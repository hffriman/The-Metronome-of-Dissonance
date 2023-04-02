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

    private bool readyToShoot;

    RaycastHit soundBeamHit;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;

        readyToShoot = true;
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

            if (readyToShoot)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) 
                {
                    soundAttackSensors[0].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                    Shoot(Vector3.forward, i);
                } 
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    soundAttackSensors[1].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                    Shoot(new Vector3(-1,0,0), i);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    soundAttackSensors[2].GetComponent<AudioSource>().PlayOneShot(soundWeapons[i], 1.0f);
                    Shoot(new Vector3(1,0,0), i);
                }
            }
        }

        Debug.Log(readyToShoot);
    }

    void Shoot(Vector3 shootingDirection, int soundWeaponNumber)
    {
        RaycastHit soundBeamHit;
        

        if (Physics.Raycast(transform.position, transform.TransformDirection(shootingDirection), out soundBeamHit, Mathf.Infinity))
        {
            if (soundBeamHit.collider.tag == "Enemy" && soundBeamHit.collider.GetComponent<EnemyManager>().enemyAudio.name.ToString().Contains(soundWeaponNumber.ToString()))
            {
                soundBeamHit.collider.GetComponent<EnemyManager>().EnemyDisappearance();
            }
            else 
            {
                Debug.Log("Not success");
            }
        }

        StartCoroutine(WaitAfterShooting());
    }



    public IEnumerator WaitAfterShooting()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(2);
        readyToShoot = true;
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
