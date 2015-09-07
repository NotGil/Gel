using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpHeight = 5;
    public float splitDistance;
    public Transform leftCube;
    public Transform forwardCube;
    private string taggedCollision;
    public Transform cubeA;
    public Transform cubeB;

    Vector3 moveDirection;

    public void Update()
    {
        Vector3 left = this.transform.position - leftCube.position;
        left = left.normalized;
        Vector3 forward = forwardCube.position - this.transform.position;
        forward = forward.normalized;

        if (Input.GetButtonDown("Jump")&&IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        
        GetComponent<Rigidbody>().AddForce(left* 0.5f * moveSpeed * Input.GetAxis("Horizontal"));
        GetComponent<Rigidbody>().AddForce(forward * 0.5f * moveSpeed * Input.GetAxis("Vertical"));
        
        

    }
    public bool IsGrounded()    
    {
        RaycastHit groundHit=new RaycastHit();
        //Sends a raycast down and meausures the distance to the ground/any object below it
        if (Physics.Raycast(this.transform.position, -Vector3.up,out groundHit))
        {
            if (groundHit.distance < transform.lossyScale.x / 2 || taggedCollision.Equals("Top"))
            {
                //Debug.Log("isGrounded");
                return true;
            }
        }
        
        return false;
    }
    void OnCollisionEnter(Collision collision)
    {
        taggedCollision = collision.gameObject.tag;
        //Debug.Log(collision.gameObject.tag);

    }
}