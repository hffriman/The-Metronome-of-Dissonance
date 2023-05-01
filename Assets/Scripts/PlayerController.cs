using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a part of the Player object (in Stage 1) //
public class PlayerController : MonoBehaviour
{
    // The Player's parent object
    public GameObject playerObject;

    // The RespawnController
    public GameObject respawnController;

    // The Player's Character Controller
    private CharacterController controller;
        
    // The data of the direction input
    private Vector3 move;
    
    // Checks if the player has entered the crossroads
    private bool isOnCrossroads;

    // Checks if the player is not moving
    private bool notMoving;

    // The default value of the player's speed
    private float playerSpeed = 1900.0f;

    // Used to check the player's interaction with the crossroads
    public Transform interactionCheck;

    // The crossroad object's layer, which is recognized by interactionCheck
    public LayerMask CrossRoadsLayer;
    
    // Stores the position data of the collected checkpoint 
    public Vector3 checkpoint;

    // Stores the last direction where the player was heading to
    private Vector3 savedDirection;

    // Contains the audio sources of the player:
    // --> used to produce the walking sound in the correct direction
    public GameObject[] playerWalkSensors;

    // The walking sound clip
    public AudioClip walkingSound;

    // The Metronome object
    private GameObject metronome;

    // Collects all the audio sources in the stage
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        // Defining the Character Controller and Metronome
        controller = gameObject.GetComponent<CharacterController>();
        metronome = GameObject.Find("Metronome");

        // Finding all the audio sources in the stage
        audioSources = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks that the controller is enabled, and the Pause Menu is closed
        if (controller.enabled == true && PauseControl.isPaused == false) 
        {

            // Checks if the player has touched the Crossroads object
            isOnCrossroads = Physics.CheckSphere(interactionCheck.position, 0.15f, CrossRoadsLayer);

            // Checks if the player is not moving
            if (move == Vector3.zero) {
                notMoving = true;
            } else {
                notMoving = false;
            }

            // If the player is at the crossroads, they can:
            //      1. Listen to the sound of Metronome by holding Left/Right Control key
            //         (also mutes all the other audio sources)
            //      2. Walk to left, right, or forward by pressing A, D or W key
            //         (the character will be automatically moved in the assigned direction)
            if (isOnCrossroads) {
                
                if (notMoving && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {

                    foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.mute = true;
                    }

                    metronome.GetComponent<AudioSource>().mute = false; 
                }
                else if (!notMoving || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
                    
                    foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.mute = false;
                    }

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

            
            // - Checks the player's walking direction and plays the walking sound
            //   from the correct audio source (forward, left, right)
            //  - The walking sound will be stopped when the player stops moving
            
            // If moving forward:
            if (move != Vector3.zero && move.z > 0)
            {
                PlayWalkingSound(playerWalkSensors[0]);
            }
            else
            {
                StopWalkingSound(playerWalkSensors[0]);
            }

            // If moving to the left:
            if (move != Vector3.zero && move.x < 0)
            {
                PlayWalkingSound(playerWalkSensors[1]);
            }
            else 
            {
                StopWalkingSound(playerWalkSensors[1]);
            }

            // If moving to the right:
            if (move != Vector3.zero && move.x > 0)
            {
                PlayWalkingSound(playerWalkSensors[2]);
            }
            else
            {
                StopWalkingSound(playerWalkSensors[2]);
            }

            // Updates the movement
            // <-- This is where the actual movement happens -->
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
    }

    // This is activated when the player touches the ArrowSign object
    // --> The character's moving direction will change based on the
    //     ArrowSign's direction (the rotation value of z-axis)
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

    // Collects the position data of the checkpoint
    // and also stops the player's current movement
    public void CollectCheckpoint()
    {
        move = new Vector3(0, 0, 0);
        checkpoint = playerObject.transform.position;
    }

    // Prepares the player for disappearance after their health reaches 0
    // (uses the checkpoint's position as a parameter, which is necessary for respawning)
    public void PrepareForDisappearance()
    {
        StopPlayer();
        respawnController.GetComponent<Respawn>().Disappear(checkpoint);
        
    }

    // Used to stop the player's current movement
    public void StopPlayer() 
    {
        move = new Vector3(0, 0, 0);
    }

    // Moves the player up in the air (activated in PrepareForDisappearance function)
    public void FlyAway()
    {
        move = new Vector3(0, 0.09f, 0);
    }

    // Used to make the player continue their previous movement
    // (used after the player has defeated enemies in FightStop)
    public void ContinueMoving()
    {
        move = savedDirection;
    }

    // This plays the walking sound from the correct playerWalkSensor
    // (based on the player's walking direction)
    public void PlayWalkingSound(GameObject playerWalkSensor) 
    {
        playerWalkSensor.GetComponent<AudioSource>().clip = walkingSound;
        playerWalkSensor.GetComponent<AudioSource>().loop = true;
        playerWalkSensor.GetComponent<AudioSource>().enabled = true;
    }

    // This stops the walking sound of the active playerWalkSensor
    public void StopWalkingSound(GameObject playerWalkSensor)
    {
        playerWalkSensor.GetComponent<AudioSource>().enabled = false;
    }

}
