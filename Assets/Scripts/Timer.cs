using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float seconds;

    private float originalSeconds;

    private bool readyToPlay;

    private AudioSource[] audioSources;

    public AudioClip[] timeAnnouncements;

    public GameObject pauseMenuCollective;

    public GameObject gameOverLayer;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        originalSeconds = seconds;
        readyToPlay = true;
        audioSources = FindObjectsOfType<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds >= 0)
        {
            seconds = seconds - Time.deltaTime;
        }
        
        // Timer: when 60 seconds has passed, call the correct AudioClip!!
        if (seconds < 241f && seconds > 240f)
        {
            announceTime(timeAnnouncements[4]);
        }
        else if (seconds < 181f && seconds > 180f)
        {
            announceTime(timeAnnouncements[3]);
        }
        else if (seconds < 121f && seconds > 120f)
        {
            announceTime(timeAnnouncements[2]);
        }
        else if (seconds < 61f && seconds > 60f)
        {
            announceTime(timeAnnouncements[1]);
        }
        else if (seconds <= 1f)
        {
            pauseMenuCollective.SetActive(false);
            announceTime(timeAnnouncements[0]);
        }
    }
    
    public void announceTime(AudioClip timeAnnouncement)
    {
        if (readyToPlay)
        {
            this.GetComponent<AudioSource>().PlayOneShot(timeAnnouncement);
        }

        if (seconds <= 1)
        {
            StartCoroutine(timeOverScreen());
        }
        
        readyToPlay = false;
        StartCoroutine(resetCounter());
    }

    IEnumerator resetCounter()
    {
        yield return new WaitForSeconds(10);
        readyToPlay = true;
    }

    IEnumerator timeOverScreen()
    {    
        yield return new WaitForSeconds(1);

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = true;
        }

        if (player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().StopPlayer();
        }
        else if (player.GetComponent<SpecialControls>())
        {
            player.GetComponent<SpecialControls>().StopSpinning();
        }

        this.GetComponent<AudioSource>().mute = false;
        yield return new WaitForSeconds(2);

        gameOverLayer.SetActive(true);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);

    }
}
