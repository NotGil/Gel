using UnityEngine;
using System.Collections;

public class ColorPicker : MonoBehaviour {

    public Color color;
	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().material.color = color;
	}
	
}
