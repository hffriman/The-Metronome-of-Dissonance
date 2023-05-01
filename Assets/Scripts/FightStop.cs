using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - This script is attached to the FightStop component (in Stage 1),
      which the player must trigger in order to start the fight
    - After all the enemies of the fight stop are destroyed,
      the player object will continue walking forward
*/

public class FightStop : MonoBehaviour
{

    // The player's sound weapon selector
    public GameObject soundWeaponSelector;

    // The enemies nearby the fight stop (3 enemies)
    public GameObject[] enemies;

    // This will contain the amount of the defeated enemies
    private int defeatedEnemies;

    // This will check that all the enemies are defeated
    private bool allDefeated;

    // This will check if the player has entered the fight stop
    private bool isOnFightStop;

    // Player's game object
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        // The player object will be found
        player = GameObject.Find("Player");

        // The enemies nearby will be defined not destroyed
        allDefeated = false;

        // The player is not defined to be in the fight stop
        isOnFightStop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Checking if the player has reached the fight stop
        if (isOnFightStop)
        {
            /*  - The amount of nearby enemies are checked
                - If all the enemies are defeated, the fight stop will end,
                  and the player's health will be restored
            */
            CheckEnemyAmount();
            if (allDefeated)
            {
                soundWeaponSelector.GetComponent<SoundFight>().EndSoundFight();
                isOnFightStop = false;
                player.GetComponent<HealthManager>().RestoreHealth();
            }
        }
    }
    
    /* 
        If the player object has triggered the fight stop,
        the player's position in the fight stop will be defined true,
        and the fighting area is prepared to begin the enemy attacks
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnFightStop = true;
            checkFightingArea();
        }
    }

    /*
        When the player has left the fight stop,
        the player's position in the fight stop will be defined false,
        and the Fighting area is prepared to stop the enemy attacks
    */
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnFightStop = false;
            checkFightingArea();
        }
    }

    /* 
        - This checks the amount of the enemies in the fight stop
        - If all of the enemies in the FightStop are defeated,
          the allDefeated boolean will be defined true
    */
    private void CheckEnemyAmount()
    {
        defeatedEnemies = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeSelf)
            {
                defeatedEnemies++;
            }
        }

        if (defeatedEnemies == enemies.Length)
        {
            allDefeated = true;
        }
    }

    /* 
        - This is used to check the player's position in the fight stop
        - If the player is on the fight stop, they will stop moving, and the
          sound fight will begin (in the SoundFight script). Meanwhile, all the enemies 
          in the fight stop will prepare for their attacks
          (their attack times and other behaviours are defined in the EnemyManager script)
        - However, if the player is no longer in the fight stop, the sound fight will be cancelled
          (this part is also in the SoundFight script)
    */
    private void checkFightingArea() {
        
        if (isOnFightStop == true)
        {
            player.GetComponent<PlayerController>().StopPlayer();
            soundWeaponSelector.GetComponent<SoundFight>().CommenceSoundFight(isOnFightStop);
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyManager>().PrepareForEnemyAttack(player.GetComponent<CapsuleCollider>());
            }
        }
        else 
        {
            soundWeaponSelector.GetComponent<SoundFight>().CommenceSoundFight(isOnFightStop);
            StartCoroutine(RestoreOldFightStop());
        }
    }

    // This restores the FightStop after the player has moved forward
    // (If the player will come back to this FightStop, the fight can begin again)
    IEnumerator RestoreOldFightStop()
    {
        yield return new WaitForSeconds(10);
        this.defeatedEnemies = 0;
        allDefeated = false;

        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyManager>().hasTakenShot = false;
        }
    }
}
