using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
    - This script is used in Timer object (in Stage 1 and 2)
    - It sets a time limit, under which the player must complete the stage
    - It works in the following way:
        1. The given time limit is assigned to the "seconds" variable
        2. The seconds will be reduced by time.DeltaTime (the value of seconds variable loses 1.0f after each second)
        3. Whenever the amount of seconds has reached a certain amount, the timeAnnouncement array will
           play the correct sound effect ("x minute left") 
        4. When the seconds has reached 0, the timeAnnouncement plays the last sound ("Time over")
        5. All the audio sources in the scene will be muted, and the player's movement is stopped
        6. After some seconds, Game Over layer appears in the screen, and its audio clip is activated ("Game Over")
        7. After some seconds, the game will start from the Notice screen (part of the title sequence)
*/
public class Timer : MonoBehaviour
{

    // The time limit of the stage
    public float seconds;

    // ReadyToPlay is used to make sure that each timeAnnouncement is played only once
    private bool readyToPlay;

    // All the audio sources of the game
    private AudioSource[] audioSources;

    // The collection of timeAnnouncement clips
    public AudioClip[] timeAnnouncements;

    // The Pause Menu canvas (locked immediately after the timer reaches 0)
    public GameObject pauseMenuCollective;

    // Appears when the time has reached 0, marking the end of the game
    public GameObject gameOverLayer;

    // Player object (the player will be stopped when the timer reaches 0)
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
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
        
        if (readyToPlay)
        {
            // Timer: when there are 360 seconds left, call the correct AudioClip!!
            if (seconds < 361f && seconds > 360f)
            {
                announceTime(timeAnnouncements[6]);
            }

            // Timer: when there are 300 seconds left, call the correct AudioClip!!
            else if (seconds < 301f && seconds > 300f)
            {
                announceTime(timeAnnouncements[5]);
            }

            // Timer: when there are 240 seconds left, call the correct AudioClip!!
            else if (seconds < 241f && seconds > 240f)
            {
                announceTime(timeAnnouncements[4]);
            }
            
            // Timer: when there are 180 seconds left, call the correct AudioClip!!
            else if (seconds < 181f && seconds > 180f)
            {
                announceTime(timeAnnouncements[3]);
            }
            
            // Timer: when there are 120 seconds left, call the correct AudioClip!!
            else if (seconds < 121f && seconds > 120f)
            {
                announceTime(timeAnnouncements[2]);
            }

            // Timer: when there are 60 seconds left, call the correct AudioClip!!
            else if (seconds < 61f && seconds > 60f)
            {
                announceTime(timeAnnouncements[1]);
            }

            // Timer: when the time has run out, call the correct AudioClip!!
            else if (seconds <= 1f)
            {
                pauseMenuCollective.SetActive(false);
                announceTime(timeAnnouncements[0]);
            }
        }
    }
    
    public void announceTime(AudioClip timeAnnouncement)
    {
        if (readyToPlay)
        {
            this.GetComponent<AudioSource>().PlayOneShot(timeAnnouncement);
            Debug.Log(timeAnnouncement.name);
        }

        if (seconds <= 1f)
        {
            StartCoroutine(timeOverScreen());
        }
        
        readyToPlay = false;
        StartCoroutine(resetCounter());
    }

    IEnumerator resetCounter()
    {
        yield return new WaitForSeconds(30);
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
