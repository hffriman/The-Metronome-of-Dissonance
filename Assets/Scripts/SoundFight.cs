using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFight : MonoBehaviour
{

    public GameObject playerObject;

    public GameObject soundWeaponSource;

    public GameObject[] soundAttackSensors;

    public AudioClip[] soundWeapons;

    public AudioClip weaponBeam;

    private int i;

    private bool isAbleToFight;

    private bool readyToShoot;

    RaycastHit soundBeamHit;

    // Start is called before the first frame update
    void Start()
    {
        i = -1;

        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAbleToFight) {

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

            if (readyToShoot && i >= 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) 
                {
                    soundAttackSensors[0].GetComponent<AudioSource>().PlayOneShot(weaponBeam, 1.0f);
                    Shoot(Vector3.forward, i);
                } 
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    soundAttackSensors[1].GetComponent<AudioSource>().PlayOneShot(weaponBeam, 1.0f);
                    Shoot(new Vector3(-1,0,0), i);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    soundAttackSensors[2].GetComponent<AudioSource>().PlayOneShot(weaponBeam, 1.0f);
                    Shoot(new Vector3(1,0,0), i);
                }
            }
        }
    }

    void Shoot(Vector3 shootingDirection, int soundWeaponNumber)
    {
        RaycastHit soundBeamHit;
        

        if (Physics.Raycast(transform.position, transform.TransformDirection(shootingDirection), out soundBeamHit, Mathf.Infinity))
        {
            if (soundBeamHit.collider.tag == "Enemy" && soundBeamHit.collider.GetComponent<EnemyManager>().currentEnemyIdleSound.name.ToString().Contains(soundWeaponNumber.ToString()))
            {
                soundBeamHit.collider.GetComponent<EnemyManager>().PrepareForEnemyDisappearance();
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


   public void CommenceSoundFight(bool currentStatus)
   {
      isAbleToFight = currentStatus;
   }

   public void EndSoundFight()
   {
      isAbleToFight = false;
      playerObject.GetComponent<PlayerController>().ContinueMoving();
   }
}
