using UnityEngine;
using System.Collections;

public class FrictionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().material.frictionDirection2 = Vector3.left;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
