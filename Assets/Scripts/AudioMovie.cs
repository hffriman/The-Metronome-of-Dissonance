using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(0);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
