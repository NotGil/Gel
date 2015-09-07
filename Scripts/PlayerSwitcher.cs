using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class PlayerSwitcher : MonoBehaviour {

    // This class depends on the order of the objects
    // 0,1,2,3,4,5,6
    // Cube, Cube A, Cube B, Cube A1, Cube A2, Cube B1, Cube B2
    public Camera mainCamera;
    public PhysicMaterial slipperyMaterial;
    //public PhysicMaterial defaultMaterial;
    private RigidbodyConstraints freeze,unfreeze;
    public int currentPlayerIndex;
    private Transform[] allCubes;
    // Sets current players on and other variables
	void Start () {
        if (mainCamera == null)
        {
            Debug.LogError("You need to attach the camera to player switcher");
        }
        //currentPlayerIndex = 0;
        //freeze = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        unfreeze = RigidbodyConstraints.FreezeRotation;
        allCubes = new Transform[transform.childCount];
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            allCubes[i] = transform.GetChild(i);
            /*Debug.Log("For loop: " + transform.GetChild(i));*/
        }
        allCubes[currentPlayerIndex].GetComponent<Collider>().material = slipperyMaterial;
	}
    // Update is called once per frame and primarily handles input
	public void Update () {
        
        if (Input.GetButtonDown("Switch1"))
        {
            Switch1();
        }
        
        if (Input.GetButtonDown("Switch2"))
        {
            Switch2();
        }
        if (Input.GetButtonDown("Split"))
        {
            Split();
        }
        if (Input.GetButtonDown("Fuse"))
        {
            FusePlayer();
        }
        
	}
    // SwitchPlayer Freezes and disables PlayerController for each player object 
    // and Unfreezes and enables PlayerController for currentplayer
    private void SwitchPlayer()
    {
        /*Debug.Log("Starting process");*/
        foreach (Transform player in allCubes)
        {
            /*Debug.Log(player.name);*/
            //player.gameObject.rigidbody.constraints = freeze;
            player.gameObject.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Collider>().material = null;
        }
        mainCamera.GetComponentInChildren<ReceiverScript>().SendMessage("ChangeCamera", allCubes[currentPlayerIndex]);
        allCubes[currentPlayerIndex].GetComponent<Collider>().material = slipperyMaterial;
        allCubes[currentPlayerIndex].gameObject.GetComponent<Rigidbody>().constraints = unfreeze;
        allCubes[currentPlayerIndex].gameObject.GetComponent<PlayerController>().enabled = true;

    }
    // FusePlayer joins two cubes together
    private void FusePlayer()
    {
        int selectedPlayerIndex = CanFuseWith();
        /*Debug.Log(selectedPlayerIndex);*/
        // If the selectedPlayerIndex is -1, there is no cube to fuse with
        if (selectedPlayerIndex != -1)
        {
            /*Debug.Log(currentPlayerIndex + " " + selectedPlayerIndex);*/
            // Disables the two cubes
            allCubes[currentPlayerIndex].gameObject.SetActive(false);
            allCubes[selectedPlayerIndex].gameObject.SetActive(false);
            
            // Determines what cube to replace them with

            // If the cube is a small
            if (Regex.IsMatch(allCubes[selectedPlayerIndex].name, "\\d"))
            {
                // If Medium Cube 1 is on the field
                if (allCubes[1].gameObject.activeSelf)
                {
                    Debug.Log("Replace with blue cube");
                    allCubes[2].position = allCubes[currentPlayerIndex].position;
                    allCubes[2].gameObject.SetActive(true);
                    StuffThatCube(2, currentPlayerIndex, selectedPlayerIndex);
                    currentPlayerIndex = 2;
                    SwitchPlayer();
                }
                // Else if Medium Cube 2 is on the field
                else if (allCubes[2].gameObject.activeSelf)
                {
                    Debug.Log("Replace with orange cube");
                    allCubes[1].position = allCubes[currentPlayerIndex].position;
                    allCubes[1].gameObject.SetActive(true);
                    StuffThatCube(1, currentPlayerIndex, selectedPlayerIndex);
                    currentPlayerIndex = 1;
                    SwitchPlayer();
                }
                // Else if no Medium Cubes are on the field
                else
                {
                    Debug.Log("Replace with either orange or blue cube");
                    allCubes[1].position = allCubes[currentPlayerIndex].position;
                    allCubes[1].gameObject.SetActive(true);
                    StuffThatCube(1, currentPlayerIndex, selectedPlayerIndex);
                    currentPlayerIndex = 1;
                    SwitchPlayer();
                }
            }
            // If the cube is Medium
            else
            {
                Debug.Log("Replace with large cube");
                allCubes[0].position = allCubes[currentPlayerIndex].position;
                
                allCubes[0].gameObject.SetActive(true);
                currentPlayerIndex = 0;
                SwitchPlayer();
            }
        }
    }
    private void Switch1()
    {
        do{
            currentPlayerIndex--;
            if (currentPlayerIndex < 0)
            {
                currentPlayerIndex = allCubes.Length - 1;
            }
        } while (!allCubes[currentPlayerIndex].gameObject.activeSelf);
        SwitchPlayer();
    }
    private void Switch2()
    {
        do
        {
            currentPlayerIndex++;
            if (currentPlayerIndex >= allCubes.Length)
            {
                currentPlayerIndex -= allCubes.Length;
            }
        } while (!allCubes[currentPlayerIndex].gameObject.activeSelf);
        SwitchPlayer();
    }
    private void Split()
    {
        if (currentPlayerIndex == 0)
        {
            //Play split animation here
            currentPlayerIndex = 1;
            allCubes[0].gameObject.SetActive(false); //Disables/hides the current cube
            allCubes[1].position = new Vector3(allCubes[0].transform.position.x + 2, allCubes[0].transform.position.y, allCubes[0].transform.position.z);
            allCubes[2].position = new Vector3(allCubes[0].transform.position.x - 2, allCubes[0].transform.position.y, allCubes[0].transform.position.z);
            allCubes[1].gameObject.SetActive(true);
            allCubes[2].gameObject.SetActive(true);
            //The above two lines creates two medium cubes side by side
        }
        else if(currentPlayerIndex == 1)
        {
            currentPlayerIndex = 3;
            allCubes[1].gameObject.SetActive(false); //Disables/hides the current cube
            allCubes[3].position = new Vector3(allCubes[1].transform.position.x + 1, allCubes[1].transform.position.y, allCubes[1].transform.position.z);
            allCubes[4].position = new Vector3(allCubes[1].transform.position.x - 1, allCubes[1].transform.position.y, allCubes[1].transform.position.z);
            allCubes[3].gameObject.SetActive(true);
            allCubes[4].gameObject.SetActive(true);
        }
        else if (currentPlayerIndex == 2)
        {
            currentPlayerIndex = 5;
            allCubes[2].gameObject.SetActive(false); //Disables/hides the current cube
            /*Debug.Log("position");*/
            allCubes[5].position = new Vector3(allCubes[2].transform.position.x + 1, allCubes[2].transform.position.y, allCubes[2].transform.position.z);
            allCubes[6].position = new Vector3(allCubes[2].transform.position.x - 1, allCubes[2].transform.position.y, allCubes[2].transform.position.z);
            allCubes[5].gameObject.SetActive(true);
            allCubes[6].gameObject.SetActive(true);
        }

        SwitchPlayer();
    }
    // Helper method that swaps two indexes in the allCubes array
    private void CubeSwap(int a,int b)
    {
        if (a != b)
        {
            Transform temp = allCubes[a];
            allCubes[a] = allCubes[b];
            allCubes[b] = temp;
        }
        
    }
    // Takes a parent index either 1 or 2 and the index of two "child" cubes,
    // then sets them in the apropriate index for the next call to Split()
    private void StuffThatCube(int parent, int childA, int childB)
    {
        
        if (parent == 1)
        {
            CubeSwap(childA, 3);
            CubeSwap(childB, 4);
        }
        else if(parent == 2){
            CubeSwap(childA, 5);
            CubeSwap(childB, 6);
        }
        

    }
    // Checks if the current cube can fuse and returns -1 or
    // if it can it returns the index the closest same size cube
    private int CanFuseWith()
    {
        // The maximum distance a cube could be to fuse
        float maxDistance = 4;
        // The index used as the index for the loop
        int index = 0;
        // The selectedPlayerIndex is the index of the closest
        // and same sized cube
        int selectedPlayerIndex = -1;

        // Loops through all activePlayers to find the
        // closest and same sized cube
        foreach (Transform player in allCubes)
        {
            // If the current Playable Cube name has a number in it then it is small cube, if not it is a medium cube
            // Regex is used to determine if the names have numbers
            if (Regex.IsMatch(allCubes[currentPlayerIndex].name, "\\d") == Regex.IsMatch(player.transform.name, "\\d") && player.gameObject.activeSelf)
            {
                // Determines the distance between the cubes
                float distance = Vector3.Distance(allCubes[currentPlayerIndex].position, player.transform.position);

                // If the distance is less than the maxDistance and not 0(Distance==0 means its the same cube)
                // set this cube as the Selected Player
                if (maxDistance > distance && distance != 0)
                {
                    maxDistance = distance;
                    selectedPlayerIndex = index;
                }

            }
            index++;

        }
        return selectedPlayerIndex;
    }
}
