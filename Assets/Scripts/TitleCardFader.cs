using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// This script is used in the title sequences of the game (Notice, HenrySFriman presents, Startscreen)
// Its main idea is to fade in/out black screen after certain seconds (delaySeconds)
// In the middle of fade in/out, the current scene's UI is shown, and its audio is activated
// Idea in a nutshell: 
//    1. The black screen disappears slowly,
//    2. The current scene's UI is shown (Notice, HenrySFriman presents or Startscreen)
//    3. The UI's audio source is activated (delaySeconds is used to measure the audio clip's length)
//    4. After the audio clip, the black screen appears again in a couple of seconds 
//    5. The scene is changed
public class TitleCardFader : MonoBehaviour
{

    // DelaySeconds should be as long as the title card's audio clip's length
    public float delaySeconds;

    // BlackScreen is used in fade in/out transitions
    public Image blackScreen;

    // Used to check if the scene should begin or end
    private bool fading;
    
    // ActivationCounter and DeactivationCounter
    // are used to make sure that the audio clips
    // will play only once
    private int activationCounter;
    private int deactivationCounter;

    // Start is called before the first frame update
    void Start()
    {
        fading = true;
        activationCounter = 0;
        deactivationCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fading == true)
        {
            StartFadingIn();
        }

        if (fading == false) 
        {
            StartFadingOut();
        }
    }

    public void StartFadingIn()
    {
        activationCounter++;
        if (activationCounter > 0 && activationCounter < 2)
        {
            blackScreen.CrossFadeAlpha(0, 1.0f, false);
            StartCoroutine(ActivateScreenReading());
        }
    }

    IEnumerator ActivateScreenReading()
    {
        if (activationCounter > 0 && activationCounter < 2) 
        {
            yield return new WaitForSeconds(1);
            this.GetComponent<AudioSource>().mute = false;
            this.GetComponent<AudioSource>().Play(0);
            yield return new WaitForSeconds(delaySeconds);
            fading = false;
        }
    }

    public void StartFadingOut()
    {
        deactivationCounter++;

        if (deactivationCounter > 0 && deactivationCounter < 2)
        {
            blackScreen.CrossFadeAlpha(1, 1.0f, false);
            StartCoroutine(EndTitleCard());
        }
    }

    IEnumerator EndTitleCard()
    {
        if (deactivationCounter > 0 && deactivationCounter < 2)
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
