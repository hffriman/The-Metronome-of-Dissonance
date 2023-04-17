using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
