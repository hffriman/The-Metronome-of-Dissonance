using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is used in the "ShieldSpot & Shadow" prefabs Shadow object (in Stage 1)
    - Whenever the player activates/exits this Shadow object's trigger, the ShieldManager's
      ableToShield boolean will become true/false
    - Also, the ShieldSpot's alarm sound (beepSound) will play once the player triggers the Shadow object
*/
public class ShieldSpot : MonoBehaviour
{

    public AudioClip beepSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<ShieldManager>().prepareShield();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(beepSound);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<ShieldManager>().deactivateShield();
        }
    }
}
