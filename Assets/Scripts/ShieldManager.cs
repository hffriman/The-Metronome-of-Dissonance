using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{

    private bool ableToShield;
    private GameObject shadow;
    private GameObject shieldSpot;
    
    public AudioClip shieldSound;


    // Start is called before the first frame update
    void Start()
    {
        
        shieldSpot = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;
        ableToShield = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToShield && Input.GetKeyDown(KeyCode.Space))
        {
            shadow.GetComponent<BoxCollider>().enabled = false;
            shadow.GetComponent<AudioSource>().PlayOneShot(shieldSound, 0.5f);
            StartCoroutine(ShadowReset());
        }
    }

    public IEnumerator ShadowReset() {

        yield return new WaitForSeconds(1.75f);
        shadow.GetComponent<AudioSource>().Stop();
        shadow.GetComponent<BoxCollider>().enabled = true;
        deactivateShield();
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
