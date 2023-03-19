using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFight : MonoBehaviour
{

    public GameObject playerObject;

    public GameObject soundWeaponSource;

    public GameObject[] soundAttackSensors;

    public AudioClip[] soundWeapons;

    private int i;

    private bool isOnFightStop;

    RaycastHit soundBeamHit;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFightStop) {

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
                soundWeaponSource.GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
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
                soundWeaponSource.GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                soundAttackSensors[0].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                Shoot(Vector3.forward);
            } 
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                soundAttackSensors[1].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                Shoot(new Vector3(-1,0,0));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                soundAttackSensors[2].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                Shoot(new Vector3(1,0,0));
            }
        }
    }

    void Shoot(Vector3 shootingDirection)
    {
        RaycastHit soundBeamHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(shootingDirection), out soundBeamHit, Mathf.Infinity))
        {
            if (soundBeamHit.collider.tag == "Enemy")
            {
                Debug.Log("Success");
                soundBeamHit.collider.GetComponent<EnemyManager>().EnemyDisappearance();
            }
            else {
                Debug.Log("Not success");
            }
        }
    }

   public void CommenceSoundFight()
   {
      isOnFightStop = true;
   }

   public void EndSoundFight()
   {
      isOnFightStop = false;
      playerObject.GetComponent<PlayerController>().ContinueMoving();
   }


}
