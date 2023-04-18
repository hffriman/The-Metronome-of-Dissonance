using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
    - This script is used to set the Volume in the Main Menu and Pause Menu
    - When the Slider UI element is moved, the Audio Listener's volume is defined by it
    - When the new volume is set, the Slider will activate its sound (helps to check if the new volume is good)
    - The changed Volume is saved in all of the scenes in the game (DontDestroyOnLoad)
*/
public class VolumeControl : MonoBehaviour
{

    public GameObject slider;
    
    private float volumeValue;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        volumeValue = slider.GetComponent<Slider>().value;
        AudioListener.volume = slider.GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.GetComponent<Slider>().value;
        slider.GetComponent<ButtonSound>().Reset();
        slider.GetComponent<ButtonSound>().ActivateSound();
        
    }
    
}
