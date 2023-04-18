using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - This script is used in the SoundWeaponSelector object of the player object (in Stage 1)
    - It enables the Sound Weapon inventory, which the player can use by pressing Q or E keys
    - It also manages the fighting conditions, as well as the damage given to enemies
*/
public class SoundFight : MonoBehaviour
{

    // Player object
    public GameObject playerObject;

    // The center sound source of the Player (used to play the selected weapon's sound)
    public GameObject soundWeaponSource;

    // The sound sources in the forward, left or right side of the player (used to play the shooting sound)
    public GameObject[] soundAttackSensors;

    // The inventory of the sound weapons' sound clips (played in soundWeaponSource)
    public AudioClip[] soundWeapons;

    // The shooting sound clip
    public AudioClip weaponBeam;

    // Used to select the sound weapons inside the soundWeapons array
    private int i;

    // Checks if the player is able to fight (whether they are in the FightStop or not)
    private bool isAbleToFight;

    // Checks if the player is ready to shoot (whether their cool-down moment is over or not)
    private bool readyToShoot;

    // Checks the player's shooting direction, and is used to detect the enemies
    RaycastHit soundBeamHit;

    // Start is called before the first frame update
    void Start()
    {
        // Index is first defined as -1, so that the player will always select 
        // the first/last soundWeapon when they press E/Q
        i = -1;

        // The player is defined to be ready to shoot when they first enter the FightStop
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // When the player is able to fight, they can press Q/E keys to select the correct sound weapong
        // The sound of the weapon will be played in the soundWeapon source
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

            // - When the player is ready to shoot, and they have selected their weapon (index bigger than -1),
            //   the correct soundAttackSensor will play the weaponBeam sound based on the arrow key the player has pressed
            // - Also, the Shoot function will activate immediately after that
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

    // This function activates the shooting and uses RayCastHit to detect the enemies
    void Shoot(Vector3 shootingDirection, int soundWeaponNumber)
    {
        RaycastHit soundBeamHit;
    
        // - This section checks if the player has shot the enemy with the correct sound weapon
        // - The comparison is done by the following way:
        //    1. Each enemy has their own idlesound, which has either the name "enemyidlesound0", "enemyidlesound1" or "enemyidlesound2"
        //    2. The player's sound weapons also have three names: "soundweapon0", "soundweapon1" or "soundweapon2"
        //    3. If the player has hit the enemy, and the active sound weapon's number matches the enemy's idlesound's number,
        //       the enemy will disappear. If the numbers are not the same, then the enemy will not disappear
        if (Physics.Raycast(transform.position, transform.TransformDirection(shootingDirection), out soundBeamHit, Mathf.Infinity))
        {
            if (soundBeamHit.collider.tag == "Enemy" && soundBeamHit.collider.GetComponent<EnemyManager>().currentEnemyIdleSound.name.ToString().Contains(soundWeaponNumber.ToString()))
            {
                soundBeamHit.collider.GetComponent<EnemyManager>().PrepareForEnemyDisappearance();
            }
        }

        // After the shooting, whether successful or not, the cooldown will begin
        StartCoroutine(WaitAfterShooting());
    }

    // The player's cooldown lasts for a couple of seconds
    // --> after that, the player is able to shoot again
    public IEnumerator WaitAfterShooting()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(2);
        readyToShoot = true;
    }

   // This will activate when the player has entered the FightStop
   // (this is called by FightStop script)
   public void CommenceSoundFight(bool currentStatus)
   {
      isAbleToFight = currentStatus;
   }

   // This will activate when the player has defeated all the enemies in the FightStop
   // (this is called by the FightStop script)
   public void EndSoundFight()
   {
      isAbleToFight = false;
      playerObject.GetComponent<PlayerController>().ContinueMoving();
   }
}
