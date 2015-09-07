using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

    public GameObject connectedDevice;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        //connectedDevice.GetComponentInChildren
        connectedDevice.GetComponentInChildren<ReceiverScript>().SendMessage("Activate",true);
    }
    void OnTriggerExit()
    {
        //connectedDevice.GetComponentInChildren
        connectedDevice.GetComponentInChildren<ReceiverScript>().SendMessage("Activate", false);
    }
}
