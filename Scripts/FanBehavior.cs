using UnityEngine;
using System.Collections;

public class FanBehavior : MonoBehaviour {

    public float hoverForce = 12f;
    public bool isActive = true;
    // Update is called once per frame
	void Update () {
        if (isActive)
        {
            this.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
        
	}    
    void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            if (other.name.Equals("Cube A") || other.name.Equals("Cube B"))
            {
                isActive = false;
            }
            if ((other.name.Contains("1") || other.name.Contains("2")) && isActive)
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            }

        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.name.Equals("Cube A") || other.name.Equals("Cube B"))
            {
                isActive = true;
            }

        }
    }
    void Activate(bool flag)
    {
        isActive = flag;
    }

}
