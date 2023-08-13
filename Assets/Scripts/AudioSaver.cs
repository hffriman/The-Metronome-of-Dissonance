using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - This script is used to save the current volume value and increase/decrease the game's overall volume
    - When the volume is changed through the VolumeControl script, the AudioSaver's savedVolume data changes too
    - The AudioSaver uses DontDestroyOnLoad method and thus transfers its values in every scene 
*/
public class AudioSaver : MonoBehaviour
{

    public float savedVolume;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        savedVolume = 0.8f;
        AudioListener.volume = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = savedVolume;
    }
}
