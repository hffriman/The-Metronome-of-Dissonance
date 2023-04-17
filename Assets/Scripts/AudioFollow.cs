using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Make the AudioListener always be in the same position as the Player
    --> The audio will always be heard as close to the Player as possible
*/
public class AudioFollow : MonoBehaviour
{

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Audio Listener's position will always have the same position as the PlayerObject
        Vector3 audioPosition = playerObject.transform.position;
        transform.position = audioPosition;    
    }
}
