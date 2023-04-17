using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    This script is used for the Checkpoint object,
    which will make the player's current position to
    be saved as a checkpoint position
*/
public class CheckpointManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the player has collided with the Checkpoint object,
    // the player's own CollectCheckpoint function will be activated
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().CollectCheckpoint();
        }
    }
}
