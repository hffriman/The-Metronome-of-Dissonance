using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class metronomeGoal : MonoBehaviour
{

    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
