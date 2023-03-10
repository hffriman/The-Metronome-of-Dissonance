using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  GameObject playerObject;

    public GameObject respawnController;


    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 move;
    
    private bool isOnCrossroads;
    private float playerSpeed = 1900.0f;

    public Transform interactionCheck;
    public LayerMask CrossRoadsLayer;

    public Vector3 checkpoint;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player has touched the Crossroads object
        isOnCrossroads = Physics.CheckSphere(interactionCheck.position, 0.15f, CrossRoadsLayer);

        // If the player has touched the crossroads, they can move left, right, or forward
        if (isOnCrossroads) {

            if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
            {
                move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            }
            else if (Input.GetKeyDown("w")) {
                move = new Vector3(0, 0, Input.GetAxis("Vertical"));
            }
        }
         controller.Move(move * Time.deltaTime * playerSpeed);
    }

    public void ChangeDirection(float arrowRotation)
    {
        if (arrowRotation == 0.00f) {
            move = new Vector3(-0.05f, 0, 0);
        }
        else if (arrowRotation >= 0.70f)
        {
            move = new Vector3(0.05f, 0, 0);
        }
        else if (arrowRotation > 0.00f && arrowRotation < 0.70f) {
            move = new Vector3(0, 0, 0.05f);
        }
    }

    public void CollectCheckpoint()
    {
        move = new Vector3(0, 0, 0);
        checkpoint = playerObject.transform.position;
    }

    public void PrepareForDisappearance()
    {
        move = new Vector3(0, 0, 0);
        respawnController.GetComponent<Respawn>().Disappear(checkpoint);
        
    }
}
