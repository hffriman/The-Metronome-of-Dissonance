using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFight : MonoBehaviour
{

    public GameObject soundWeaponSelector;

    public GameObject[] playerAudioSensors;

    public AudioClip[] soundWeapons;

    private int i;

    RaycastHit soundBeamHit;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            if (i <= 0)
            {
                i = 2;
            }
            else
            {
                i--;
            }
            soundWeaponSelector.GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
        }
        else if (Input.GetKeyDown("e"))
        {
            if (i >= 2)
            {
                i = 0;
            }
            else 
            {
                i++;
            }       
            soundWeaponSelector.GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            playerAudioSensors[0].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
            Shoot(Vector3.forward);
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerAudioSensors[1].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
            Shoot(new Vector3(-1,0,0));
        }
         else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerAudioSensors[2].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
            Shoot(new Vector3(1,0,0));
        }
    }

    void Shoot(Vector3 shootingDirection)
    {
        RaycastHit soundBeamHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(shootingDirection), out soundBeamHit, Mathf.Infinity))
        {
            if (soundBeamHit.collider.tag == "Enemy")
            {
                Debug.Log("Hit successful");
            }
        }
    }
}
