using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
    - This script is used to set the Volume in the Main Menu and Pause Menu
    - When the Slider UI element is moved, the Audio Listener's volume is defined by it
    - When the new volume is set, the Slider will activate its sound (helps to check if the new volume is good)
    - The volume will always be saved in all of the scenes in the game (in the AudioSaver script)
*/
public class VolumeControl : MonoBehaviour
{

    public GameObject audioSaver;

    public GameObject slider;
    
    private float volumeValue;

    // Start is called before the first frame update
    void Start()
    {
        audioSaver = GameObject.Find("AudioSaver");
        volumeValue = audioSaver.GetComponent<AudioSaver>().savedVolume;
        AudioListener.volume = volumeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.GetComponent<Slider>().value;
        volumeValue = AudioListener.volume;
        audioSaver.GetComponent<AudioSaver>().savedVolume = volumeValue;
        slider.GetComponent<ButtonSound>().Reset();
        slider.GetComponent<ButtonSound>().ActivateSound();
        
    }
    
}
