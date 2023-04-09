using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTrigger : MonoBehaviour
{

    public GameObject[] cages;

    public AudioClip[] cageSounds;

    private int timesHit;

    // Start is called before the first frame update
    void Start()
    {

        timesHit = 0;

        foreach (GameObject cage in cages)
        {
            cage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player" && !Input.GetKey(KeyCode.Space) && timesHit <= 0)
        {
            this.GetComponent<AudioSource>().PlayOneShot(cageSounds[0]);
            timesHit++;

            foreach (GameObject cage in cages)
            {
                cage.SetActive(true);
            }
        }

        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
        {

            this.GetComponent<AudioSource>().PlayOneShot(cageSounds[1]);
            timesHit = 0;
            
            foreach (GameObject cage in cages)
            {
                cage.SetActive(false);       
            }
        }
    }
}
