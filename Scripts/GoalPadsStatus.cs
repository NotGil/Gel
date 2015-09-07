using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class GoalPadsStatus : MonoBehaviour {

	// Use this for initialization
    bool spaceFilled;
    private GameObject[] goalPads;
	void Start () {
        spaceFilled = false;
        goalPads = GameObject.FindGameObjectsWithTag("Finish");
        if (goalPads.Length == 1 || goalPads.Length == 2 || goalPads.Length == 4)
        {
           
        }
        else
        {
            Debug.LogError("There can only be 1,2, or 4 goalPads");
        }
        
        
	}
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        bool allSpacesFilled = true;
        goalPads = GameObject.FindGameObjectsWithTag("Finish");

        if (goalPads.Length == 1 && other.name.Equals("Cube"))
        {
            spaceFilled = true;
            Debug.Log("goal entered");
        }
        else if (goalPads.Length == 2 && (other.name.Equals("Cube A") || other.name.Equals("Cube B"))){
        
            spaceFilled = true;
            Debug.Log("goal entered");
        }
        else if (goalPads.Length == 4 && (other.name.Equals("Cube A1") || other.name.Equals("Cube B1") || other.name.Equals("Cube A2") || other.name.Equals("Cube B2")))
        {
            spaceFilled = true;
            Debug.Log("goal entered");
        }

        foreach (GameObject test in goalPads)
        {
            if (!test.GetComponent<GoalPadsStatus>().spaceFilled)
            {
                allSpacesFilled = false;
            }
        }
        if (allSpacesFilled)
        {
            Debug.Log("Load Next Level");
            Application.LoadLevel(0);
        }
        

    }
    void OnTriggerExit(Collider other)
    {
        if (goalPads.Length == 1 && other.name.Equals("Cube"))
        {
            spaceFilled = false;
            Debug.Log("goal exited");
        }
        else if (goalPads.Length == 2 && (other.name.Equals("Cube A") || other.name.Equals("Cube B")))
        {

            spaceFilled = false;
            Debug.Log("goal exited");
        }
        else if (goalPads.Length == 4 && (other.name.Equals("Cube A1") || other.name.Equals("Cube B1") || other.name.Equals("Cube A2") || other.name.Equals("Cube B2")))
        {
            spaceFilled = false;
            Debug.Log("goal exited");
        }
    }
    
}

