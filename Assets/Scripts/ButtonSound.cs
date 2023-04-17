using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    This script is used for UI buttons:
    When the button is highlighted, its
    audio source will activate, thus making
    the "screen reader" say the button's title aloud
*/
public class ButtonSound : MonoBehaviour
{
    // counter is used in to control the activation times of a button
    // because this script's functions are activated from another script's Update()
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // If the counter is 0, the button's audio source will be played
    // (the counter is incremented afterwards so that it won't be activated constantly)
    public void ActivateSound()
    {
        if (counter == 0)
        {
            this.GetComponent<AudioSource>().Play(0);
            counter++;
        }
    }

    // This will stop the button's audio source and reset its counter,
    // thus making it possible to be activated again
    public void Reset()
    {
        GetComponent<AudioSource>().Stop();
        counter = 0;
    }
}
