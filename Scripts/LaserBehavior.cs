using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {

    public GameObject connectedDevice;
    public bool deactivates=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter()
    {
        //connectedDevice.GetComponentInChildren
        connectedDevice.GetComponentInChildren<ReceiverScript>().SendMessage("Activate", !deactivates);
    }

}
