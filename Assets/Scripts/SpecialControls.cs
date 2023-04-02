using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialControls : MonoBehaviour
{

   public GameObject target;

   public Transform interactionCheck;

   public Vector3 bridgeEndPoint;

   public LayerMask circleLayer;

   public LayerMask arrowSignLayer;

   private bool isOnCircle;

   private bool isReversed;

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

    public void MoveCloser(Vector3 bridgeEndPointNew) 
    {                        
        bridgeEndPoint = bridgeEndPointNew;
        isOnBridge = true;
    }

    public void ContinueSpinning() {
        isOnBridge = false;
        isOnCircle = true;
    }

    public void MoveToReverseDirection() 
    {
        isReversed = !isReversed;
        isOnCircle = !isOnCircle;
    }
}

