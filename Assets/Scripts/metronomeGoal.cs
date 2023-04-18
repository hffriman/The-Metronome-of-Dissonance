using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
   - This script is a part of the Metronome object (in Stage 1)
   - When the player has triggered the Metronome, the metronome's sound
     will activate, and other audio sources are stopped.
   - After the triggering, the new scene will be loaded
*/
public class metronomeGoal : MonoBehaviour
{

    // This is used to store all the audio sources in the stage
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        // All the audio sources are found and stored
        audioSources = FindObjectsOfType<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // After the player has triggered the Metronome,
    // all of the audio sources are stopped, after which
    // the Metronome's own audio source starts playing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(AudioSource audioSource in audioSources)
            {
                audioSource.mute = true;
            }

            this.GetComponent<AudioSource>().mute = false;
            other.gameObject.GetComponent<PlayerController>().StopPlayer();
            StartCoroutine(EndStage());
        }
    }

    // In a couple of seconds after the OnTriggerEnter, a new scene is loaded 
    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
