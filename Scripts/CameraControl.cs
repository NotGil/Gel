using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    public Transform currentPlayer;
    private Transform spherePosition;
    public float smooth = 2.5f;
    // Use this for initialization
    void Start()
    {
        spherePosition = currentPlayer.GetChild(2).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, spherePosition.position, smooth * Time.deltaTime);
    }
    void ChangeCamera(Transform newPlayer)
    {
        currentPlayer = newPlayer;
        spherePosition = currentPlayer.GetChild(2).transform;
    }
}