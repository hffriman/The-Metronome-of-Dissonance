using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{

    private bool ableToShield;
    private GameObject shadow;
    private GameObject shieldSpot;
    
    public AudioClip shieldSound;

    private AudioSource playerAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        
        shieldSpot = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;
        playerAudioSource = GameObject.Find("Player").gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        ableToShield = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToShield && Input.GetKeyDown(KeyCode.Space))
        {
            shadow.GetComponent<BoxCollider>().enabled = false;
            playerAudioSource.PlayOneShot(shieldSound, 0.5f);
            StartCoroutine(ShadowReset());
        }
    }

    public IEnumerator ShadowReset() {

        yield return new WaitForSeconds(1.75f);
        playerAudioSource.Stop();
        shadow.GetComponent<BoxCollider>().enabled = true;
        deactivateShield();
        Debug.Log(shadow.name + " activated");
    }


    public void prepareShield()
    {
        ableToShield = true;
    }

    public void deactivateShield()
    {
        ableToShield = false;
    }
}
