using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is used in Player object (in Stage 2)
    - It will make the Player rotate around the target object (Metronome)
      and change the movement whenever certain conditions are fulfilled
*/ 
public class SpecialControls : MonoBehaviour
{

   // The target object, around which the player will rotate (Metronome)
   public GameObject target;

   // Stores the position data of the certain bridge's end point
   public Vector3 bridgeEndPoint;

   // Checks if the player touches the circle area (activates rotation in clockwise direction)
   private bool isOnCircle;

   // Checks if the player's rotation is reversed (activated when the player collides with the Cage objects)
   private bool isReversed;

   // Checks if the player is climbing the bridge (rotation is stopped)
   private bool isOnBridge;


    void Start() 
    {
        isOnCircle = true;
        isReversed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnCircle) 
        {
            transform.RotateAround(target.transform.position, Vector3.up, 30 * Time.deltaTime);
        }

        if (isReversed)
        {
            transform.RotateAround(target.transform.position, Vector3.down, 30 * Time.deltaTime);
        }

        if (isOnBridge)
        {
            isOnCircle = false;
            isReversed = false;
            transform.position = Vector3.MoveTowards(transform.position, bridgeEndPoint, 2.0f);
        }

    }

    /* 
       - Activated when the player climbs the bridge
       - The player will stop rotating and starts moving
         towards the bridge's end point
    */ 
    public void MoveCloser(Vector3 bridgeEndPointNew) 
    {                        
        bridgeEndPoint = bridgeEndPointNew;
        isOnBridge = true;
    }

    /* 
        - Activates when the player has reached the end of the bridge
        - The player will continue spinning in the clockwise direction 
    */
    public void ContinueSpinning() 
    {
        isOnBridge = false;
        isOnCircle = true;
    }

    // This is activated when the player touches the Cage object
    // --> The rotation movement is reversed
    public void MoveToReverseDirection() 
    {
        isReversed = !isReversed;
        isOnCircle = !isOnCircle;
    }

    // This stops the spinning (can be used in different scenarios)
    public void StopSpinning()
    {
        isOnCircle = false;
        isReversed = false;
        isOnBridge = false;
    }
}

