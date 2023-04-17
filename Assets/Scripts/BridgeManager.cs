using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is part of the Bridge object, which
      the Player must use to move closer to the center
      (in Stage 2 only)

    - This is the starting point of the bridge:
      if the player presses the correct direction button at the right time, 
      they start moving towards the Bridge's endpoint (see BridgeEnd script)
*/

public class BridgeManager : MonoBehaviour
{

    public GameObject bridgeEndPoint;

    public GameObject bridge;

    public AudioClip alertSound;

    public AudioClip successfulSound;

    // The direction is a String just for convenience
    // --> the letter shows the direction where the bridge's endpoint is at
    public string direction;

    private bool pressingCorrectKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player presses the correct key at the correct bridge
        // --> a.k.a. if the player is going to the same direction where the bridge's endpoint is at
        if ((direction == "A" && Input.GetKey("a")) || (direction == "D" && Input.GetKey("d")) || (direction == "W" && Input.GetKey("w")))
        {
            pressingCorrectKey = true;
        }

        else 
        {
            pressingCorrectKey = false;
        }
    }

    // Before the player gets their chance to press the correct key,
    // they are alarmed by a sound that goes from the direction
    // where the bridge's endpoint is
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<AudioSource>().PlayOneShot(alertSound);
        }
    }

    // When the player is close to the bridge's starting point,
    // they are given the chance to press the correct key
    // --> If the player has pressed the same direction key
    //     where the bridge's endpoint is at, the player
    //     starts moving towards that endpoint, thus becoming
    //     closer to the center of the stage (the goal)
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && pressingCorrectKey)
        {
            this.GetComponent<AudioSource>().Stop();
            bridge.GetComponent<AudioSource>().PlayOneShot(successfulSound);
            other.gameObject.GetComponent<SpecialControls>().MoveCloser(bridgeEndPoint.transform.position);
        }
    }
}
