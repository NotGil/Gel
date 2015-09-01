using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class PlayerSwitcher : MonoBehaviour {

	// Use this for initialization
    private int currentPlayerIndex;
    private GameObject[] activePlayers;

    private Transform currentPlayerTransform;

    private RigidbodyConstraints freeze,unfreeze;

	void Start () {
        currentPlayerIndex = 0;
        currentPlayerTransform = this.transform.GetChild(0);
        freeze = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        unfreeze = RigidbodyConstraints.FreezeRotation;
	}
	// Update is called once per frame
	public void Update () {
        activePlayers=GameObject.FindGameObjectsWithTag("Player");
        
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
    private void SwitchPlayer()
    {
        activePlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in activePlayers)
        {
            //Debug.Log(player.name);
            player.rigidbody.constraints = freeze;
            player.GetComponent<PlayerController>().enabled = false;
        }
        currentPlayerTransform = activePlayers[currentPlayerIndex].transform;
        currentPlayerTransform.gameObject.GetComponent<PlayerController>().enabled = true;
        currentPlayerTransform.gameObject.rigidbody.constraints = unfreeze;
        
        //Debug.Log("Sibling index= " + currentPlayerTransform.GetSiblingIndex());
    }
    private void FusePlayer()
    {
        
        float minDistance = 11;
        int index = 0;
        int selectedPlayerIndex = 0;
        
        foreach (GameObject player in activePlayers)
        {
            
            if (Regex.IsMatch(currentPlayerTransform.name, "\\d")==Regex.IsMatch(player.transform.name, "\\d") )
            {
                float distance = Vector3.Distance(currentPlayerTransform.position, player.transform.position);
                if (minDistance > distance && distance != 0)
                {
                    minDistance = distance;
                    selectedPlayerIndex = index;
                }
                
            }
            index++;

        }
        activePlayers[currentPlayerIndex].gameObject.SetActive(false);
        activePlayers[selectedPlayerIndex].gameObject.SetActive(false);

        if (Regex.IsMatch(currentPlayerTransform.name, "\\d"))
        {
            if (this.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                Debug.Log("Replace with blue cube");
                this.transform.GetChild(4).position = currentPlayerTransform.position;
                currentPlayerTransform = this.transform.GetChild(4);
                this.transform.GetChild(4).gameObject.SetActive(true);
            }
            else if (this.transform.GetChild(4).gameObject.activeInHierarchy)
            {
                Debug.Log("Replace with orange cube");
                this.transform.GetChild(1).position = currentPlayerTransform.position;
                currentPlayerTransform = this.transform.GetChild(1);
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Replace with either orange or blue cube");
                this.transform.GetChild(1).position = currentPlayerTransform.position;
                currentPlayerTransform = this.transform.GetChild(1);
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Replace with large cube");
            this.transform.GetChild(0).position = currentPlayerTransform.position;
            currentPlayerTransform = this.transform.GetChild(0);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        //Debug.Log("selectedPlayer is " + activePlayers[selectedPlayerIndex].gameObject.name);
    }
    private void Switch1()
    {
        currentPlayerIndex++;
        if (currentPlayerIndex >= activePlayers.Length)
        {
            currentPlayerIndex -= activePlayers.Length;
        }
        SwitchPlayer();
    }
    private void Switch2()
    {
        currentPlayerIndex--;
        if (currentPlayerIndex < 0)
        {
            currentPlayerIndex = activePlayers.Length - 1;
        }
        SwitchPlayer();
    }
    private void Split()
    {
        currentPlayerTransform.gameObject.GetComponent<PlayerController>().Split();
        //currentPlayerTransform = this.transform.GetChild(currentPlayerTransform.GetSiblingIndex()+1);
        SwitchPlayer();
        //currentPlayerTransform.
    }
}
