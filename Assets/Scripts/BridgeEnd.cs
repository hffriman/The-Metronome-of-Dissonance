using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - This script is a part of the Bridge object,
      which the player must use to move closer to the center.
      (in Stage 2 only)
      
    - After the player has moved to the end of the bridge,
      they continue their spinning around the center
*/

public class BridgeEnd : MonoBehaviour
{
    // collisionCounter is used to make sure that
    // the player can use each bridge only once
    private int collisionCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // In update, the collisionCounter is checked
    // --> If the player has already used this certain bridge,
    //     its sphere collider will be disabled
    void Update()
    {
        if (this.collisionCounter > 0)
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            this.GetComponent<SphereCollider>().enabled = true;
        }
    }

    // When the player has reached the end of the bridge,
    // they continue spinning around the center of the stage
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisionCounter++;
            other.gameObject.GetComponent<SpecialControls>().ContinueSpinning();
        }
    }
}
