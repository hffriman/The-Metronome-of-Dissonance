using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
