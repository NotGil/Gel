using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {

    public bool isActive = false;

    public float smooth;
    public float doorLength;
    public Transform doorStop;
    Vector3 newPosition,oldPosition;
    void Start()
    {
        smooth = 1;
        oldPosition = transform.position;
        newPosition = doorStop.position;
        
    }
    void Update()
    {
        
        if (isActive)
        {
            transform.position=Vector3.Lerp(transform.position, newPosition, smooth * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, oldPosition, smooth * Time.deltaTime);
        }
  
    }
    void Activate(bool flag)
    {
        isActive = flag;

    }
}
