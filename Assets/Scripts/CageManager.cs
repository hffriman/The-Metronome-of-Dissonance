using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    - This script is used for the Cage object in Stage 2
    
    - When the player touches the cage object,
      the bouncing sound will be activated,
      and the player's moving direction is reversed

    - Idea: the player will be trapped and bounces
      between two cages until they press the certain key
*/

public class CageManager : MonoBehaviour
{

    public AudioClip bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GetComponent<AudioSource>().PlayOneShot(bounceSound);
            other.gameObject.GetComponent<SpecialControls>().MoveToReverseDirection();
        }
    }
}
