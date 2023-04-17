using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    - This script is used for Crossroads object
    - This script will only activate the sound effects of the crossroads
      and play it from all three directions (left, forward and right)
*/
public class CrossroadsSound : MonoBehaviour
{
    // hitCounter is used to play the soundeffect only once during the collision
    private int hitCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The sound effects of the crossroads object
    // will be played once in three directions
    // when the player has entered the crossroads
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && hitCounter == 0)
        {
            hitCounter++;
            this.GetComponent<AudioSource>().Play(0);
            StartCoroutine(changeSoundDirection());
        }
    }

    // When the player has exited the crossroads object's trigger,
    // the crossroads object's audio source is stopped
    // and the hitCounter is resetted back to 0
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && hitCounter > 0)
        {
            hitCounter = 0;
            this.GetComponent<AudioSource>().Stop();
        }
    }

    // This plays the crossroads object's sound from left, front and right
    // in order to give the player a sense of the directions where they can go to
    IEnumerator changeSoundDirection()
    {
        this.GetComponent<AudioSource>().panStereo = 0;
        yield return new WaitForSeconds(1.35f);
        this.GetComponent<AudioSource>().panStereo = -1;
        yield return new WaitForSeconds(1.35f);
        this.GetComponent<AudioSource>().panStereo = 1;
    }
}
