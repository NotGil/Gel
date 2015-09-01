using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 3;
    public float jumpHeight = 5;
    public float splitDistance;

    public Transform cubeA;
    public Transform cubeB;

    Vector3 moveDirection;

    public void FixedUpdate()
    {
        //Velocity is used to move the player around
        //vs addForce() because it is more responsive
        Vector3 moveDirection = rigidbody.velocity;
        
        //Handles input
        if (Input.GetButtonDown("Jump")&&IsGrounded())
        {
            //rigidbody.velocity = new Vector3(0, jumpHeight, 0);
            moveDirection.y = jumpHeight;
        }
        moveDirection.x = MoveSpeed * Input.GetAxis("Horizontal");
        moveDirection.z = MoveSpeed * Input.GetAxis("Vertical");
        rigidbody.velocity = moveDirection;
        

    }
    public bool IsGrounded()    
    {
        RaycastHit groundHit;
        //Sends a raycast down and meausures the distance to the ground/any object below it
        if (Physics.Raycast(this.transform.position, -Vector3.up,out groundHit))
        {
            if (groundHit.distance < transform.lossyScale.x/2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void Split()
    {
        if ((cubeA != null || cubeB != null))
        {
            //Play split animation here
            this.gameObject.SetActive(false); //Disables/hides the current cube
            cubeA.position = new Vector3(this.transform.position.x + splitDistance, this.transform.position.y, this.transform.position.z);
            cubeB.position = new Vector3(this.transform.position.x - splitDistance, this.transform.position.y, this.transform.position.z);
            cubeA.gameObject.SetActive(true);
            cubeB.gameObject.SetActive(true);
            //The above two lines creates two medium cubes side by side
        }
        
    }
}