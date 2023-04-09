using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerObject;

    public GameObject respawnController;

    private CharacterController controller;
        
    private Vector3 move;
    
    private bool isOnCrossroads;

    private bool notMoving;

    private bool isOnJumpSpot;

    private bool isOnGround;

    private float playerSpeed = 1900.0f;

    public Transform interactionCheck;

    public LayerMask CrossRoadsLayer;
    
    public LayerMask JumpLayer;

    public Vector3 checkpoint;

    private Vector3 savedDirection;

    public GameObject[] playerWalkSensors;

    public AudioClip walkingSound;

    private GameObject metronome;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        metronome = GameObject.Find("Metronome");
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.enabled == true) 
        {

            // Checks if the player has touched the Crossroads object
            isOnCrossroads = Physics.CheckSphere(interactionCheck.position, 0.15f, CrossRoadsLayer);

            // Checks if the player is on the ground
            isOnGround = controller.isGrounded;


            // Checks if the player is not moving
            if (move == Vector3.zero) {
                notMoving = true;
            } else {
                notMoving = false;
            }

            // If the player has touched the crossroads, they can move left, right, or forward
            if (isOnCrossroads) {

                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) {
                    metronome.GetComponent<AudioSource>().mute = false;   
                }
                else if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift)) {
                    metronome.GetComponent<AudioSource>().mute = true;
                }

                if (Input.GetKeyDown("a") && notMoving) 
                {
                    move = new Vector3(-0.05f, 0, 0);
                } 
                else if (Input.GetKeyDown("d") && notMoving)
                {
                    move = new Vector3(0.05f, 0, 0);
                }
                else if (Input.GetKeyDown("w") && notMoving) {
                    move = new Vector3(0, 0, 0.05f);
                }
            }

            // Check the player's walking direction and play the Walking sound
            // in the correct audio source

            // Forward:
            if (move != Vector3.zero && move.z > 0)
            {
                PlayWalkingSound(playerWalkSensors[0]);
            }
            else
            {
                StopWalkingSound(playerWalkSensors[0]);
            }

            // Left:
            if (move != Vector3.zero && move.x < 0)
            {
                if (controller.isGrounded)
                {
                    PlayJumpingSound(playerWalkSensors[1]);
                }
                
                else
                {
                    PlayWalkingSound(playerWalkSensors[1]);
                }
            }
            else 
            {
                StopWalkingSound(playerWalkSensors[1]);
            }

            // Right:
            if (move != Vector3.zero && move.x > 0)
            {
                PlayWalkingSound(playerWalkSensors[2]);
            }
            else
            {
                StopWalkingSound(playerWalkSensors[2]);
            }

            // Updates the movement per second
            // <-- This is where the actual movement happens -->
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
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
        savedDirection = move;
    }

    public void CollectCheckpoint()
    {
        move = new Vector3(0, 0, 0);
        checkpoint = playerObject.transform.position;
    }

    public void PrepareForDisappearance()
    {
        StopPlayer();
        respawnController.GetComponent<Respawn>().Disappear(checkpoint);
        
    }

    public void StopPlayer() 
    {
        move = new Vector3(0, 0, 0);
    }

    public void FlyAway()
    {
        move = new Vector3(0, 0.09f, 0);
    }

    public void ContinueMoving()
    {
        move = savedDirection;
    }

    public void PlayWalkingSound(GameObject playerWalkSensor) 
    {
        playerWalkSensor.GetComponent<AudioSource>().clip = walkingSound;
        playerWalkSensor.GetComponent<AudioSource>().loop = true;
        playerWalkSensor.GetComponent<AudioSource>().enabled = true;
    }

    public void StopWalkingSound(GameObject playerWalkSensor)
    {
        playerWalkSensor.GetComponent<AudioSource>().enabled = false;
    }

    public void PlayJumpingSound(GameObject playerWalkSensor)
    {

    }
}
