using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    - This script is used in the audio movies
    - When the audio clip has ended, the next scene will be loaded
    - However, if the current active scene is the Credits movie (final scene),
      the game will start from scene 1 (the title sequence)
*/
public class AudioMovie : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(AfterAudioEnds());
    }


    IEnumerator AfterAudioEnds()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        if (SceneManager.GetActiveScene().name != "Credits")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
