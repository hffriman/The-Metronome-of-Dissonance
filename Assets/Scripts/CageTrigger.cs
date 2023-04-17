using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    - This script is used for te Cage object in Stage 2

    - The player must press the correct key inside this trigger
      in order to escape from the trap of two bouncing cages
      (see CageManager for more context)

    - This script also contains audio sources and audio clips
      for the cages' locking and opening sounds
*/

public class CageTrigger : MonoBehaviour
{
    // Each cageTrigger contains two cages
    public GameObject[] cages;
    public AudioClip[] cageSounds;

    // This is used to make sure that the player
    // can escape the cage trap but also be trapped
    // to it again later...
    private int timesHit;

    // Start is called before the first frame update
    void Start()
    {
        // In the beginning, the cageTrigger's cages are deactivated,
        // and the timesHit counter is 0

        timesHit = 0;

        foreach (GameObject cage in cages)
        {
            cage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    // This activates the cage trap and plays the cages' locking sound
    // (timesHit counter is also incremented in order to control the times
    //  when the player can be locked inside the same trap)
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player" && !Input.GetKey(KeyCode.Space) && timesHit <= 0)
        {
            this.GetComponent<AudioSource>().PlayOneShot(cageSounds[0]);
            timesHit++;

            foreach (GameObject cage in cages)
            {
                cage.SetActive(true);
            }
        }
    }

    // - This gives the player a chance to get free from the cage trap
    //   if they press the correct key at the correct time
    // - The cage opening sound will also be played if the player gets free
    //   (timesHit counter is also returned back to 0, meaning that the player
    //    can be trapped inside the same cage trap again if they come across it later)
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space) && timesHit > 0)
        {
            this.GetComponent<AudioSource>().PlayOneShot(cageSounds[1]);
            timesHit = 0;

            foreach (GameObject cage in cages)
            {
                cage.SetActive(false);
            }
        }
    }
}
