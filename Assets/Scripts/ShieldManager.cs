using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is used in the "ShieldSpot & Shadow" prefab (in Stage 1)
    - It is used to give the player an option to survive from the Shadow trap
      by pressing the Space key 
*/
public class ShieldManager : MonoBehaviour
{

    // Checks if the player is able to use the shield and survive the Shadow trap
    private bool ableToShield;

    // Shadow object
    private GameObject shadow;

    // ShieldSpot object
    private GameObject shieldSpot;
    
    // Audio clip for the shield sound
    public AudioClip shieldSound;


    // Start is called before the first frame update
    void Start()
    {
        // Defining the ShieldSpot and Shadow of the current prefab
        shieldSpot = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;

        // Defining the player's ability to use shield as false
        ableToShield = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* 
            - When the player is able to shield AND they press the Space key,
              the Shadow object's box collider is disabled, and the shield sound
              activates in the Shadow object's audio source
            - The Shadow object's box collider will be enabled again in the ShadowReset function
        */
        if (ableToShield && Input.GetKeyDown(KeyCode.Space))
        {
            shadow.GetComponent<BoxCollider>().enabled = false;
            shadow.GetComponent<AudioSource>().PlayOneShot(shieldSound, 0.5f);
            StartCoroutine(ShadowReset());
        }
    }

    // In a couple of seconds after the player has activated the shield,
    // the Shadow object's shield sound will stop,
    // and the Shadow object's box collider will activate
    public IEnumerator ShadowReset() {

        yield return new WaitForSeconds(1.75f);
        shadow.GetComponent<AudioSource>().Stop();
        shadow.GetComponent<BoxCollider>().enabled = true;
        deactivateShield();
    }

    // This defines the player's ability to use the shield as true
    // (this function is called in the ShieldSpot script)
    public void prepareShield()
    {
        ableToShield = true;
    }

    // This defines the player's ability to use the shield as false
    // (called both in this script and in the ShieldSpot script)
    public void deactivateShield()
    {
        ableToShield = false;
    }
}
