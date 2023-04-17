using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleCardFader : MonoBehaviour
{

    public float delaySeconds;
    public Image blackScreen;

    private bool fading;
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
