using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
    - This script is only used when the OptionsMenu is opened for the first time in a certain scene (inside MainMenu or PauseMenu)
    - The Slider's position will automatically represent the SavedVolume attribute's value in the AudioSaver script
    - The button sound is resetted immediately when the Slider is activated: otherwise its button sound would activate at the wrong time
*/
public class SliderStartConfigurator : MonoBehaviour
{
    private GameObject audioSaver;

    // Start is called before the first frame update
    void Start()
    {
        audioSaver = GameObject.Find("AudioSaver");
        this.GetComponent<Slider>().value = audioSaver.GetComponent<AudioSaver>().savedVolume;
        this.GetComponent<ButtonSound>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
