using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
   - This script is used for the enemy object (in Stage 1)
     that will progress in the following way:

     1. When it has detected the player (by CheckSphere), it will play an idle sound selected randomly from the array
     2. After the idle sound, the enemy will play the attack sound
     3. After the attack sound, if the player is detected by the, and the enemy is alive,
        their attack will be complete, and the player takes damage
     4. After the attack, the enemy will be still for 10 seconds (coolDownSeconds)
     5. The enemy repeats phases 1 to 4 if it is still alive
*/
public class EnemyManager : MonoBehaviour
{

    // Time before playing the idle sound
    public int waitSeconds = 0;

    // The time after the idle sound
    private int idleSeconds = 2;

    // Time after the successful attack
    private int coolDownSeconds = 10;

    // The damage that the enemy gives to the player
    private float damage = 40.0f;

    // Player's layer mask (used in CheckSphere function)
    private LayerMask playerLayer;

    // The transform of the enemy (also used in CheckSphere function)
    private Transform playerDetector;

    // Boolean that returns true, if the player was found by the CheckSphere function
    private bool playerInSight;

    // An array of different idle sounds (chosen randomly every time before the attack)
    public AudioClip[] enemyIdleSounds;
 
    // The current idle sound that was chosen by random from the array
    public AudioClip currentEnemyIdleSound;

    // The enemy's attack sound
    public AudioClip enemyAttackSound;

    // The sound of enemy's destruction
    public AudioClip enemyDestroySound;

    // The index number created by the randomizing function
    private int randomizedNumber;

    // Checks if the enemy is still alive (not destroyed)
    public bool hasTakenShot;



    // Start is called before the first frame update
    void Start()
    {
        // Getting the player's layer mask
        playerLayer = LayerMask.GetMask("PlayerLayer");

        // Getting the enemy's transform object
        playerDetector = this.transform.GetChild(0);

        // Defining the enemy's status as false (not destroyed)
        hasTakenShot = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if the player is detected by the enemy
        playerInSight = Physics.CheckSphere(playerDetector.position, 100.0f, playerLayer);
    }

  // Activates when the player has collided with the Fight Stop object
   public void PrepareForEnemyAttack(Collider playerCollider)
   {
        StartCoroutine(StartEnemyAttack(playerCollider));
   }

    /* 
        Starts the enemy's attack by following these steps:
         1. Randomizes a number from 0 to 2
         2. Selects a new idle sound from the idle sound array (with the randomized number)
         3. Waits for a short time (waitSeconds)
         4. Plays the idle sound
         5. Waits for a short time (idleSeconds)
         6. Starts playing the attack sound
         7. Waits for a 0.5 seconds
         8. Stops the audio source
         9. If the player was detected, and the enemy is still alive, the attack is completed and the player takes damage
         10. After some time (coolDownSeconds), the steps 1 to 9 will happen again
    */ 
    IEnumerator StartEnemyAttack(Collider playerCollider)
    {
        if (playerInSight == true)
        {
            while (playerCollider.gameObject.GetComponent<HealthManager>().currentHealth > 0.0f && playerInSight == true)
            {
                this.randomizedNumber = Random.Range(0, 3);
                
                this.currentEnemyIdleSound = enemyIdleSounds[randomizedNumber];

                yield return new WaitForSeconds(waitSeconds);

                GetComponent<AudioSource>().PlayOneShot(currentEnemyIdleSound);

                yield return new WaitForSeconds(idleSeconds);

                GetComponent<AudioSource>().PlayOneShot(enemyAttackSound);

                yield return new WaitForSeconds(0.5f);
                
                GetComponent<AudioSource>().Stop();

                if (playerInSight == true && !hasTakenShot) 
                {
                    playerCollider.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
                    yield return new WaitForSeconds(coolDownSeconds);
                }
            }
        }
    }
    
    // This happens when the enemy is being destroyed (called from the SoundFight script)
    public void PrepareForEnemyDisappearance() 
    {
        // Enemy's status is changed (has taken shot from the player)
        hasTakenShot = true;

        // The enemy's disappearance will start
        StartCoroutine(CompleteEnemyDisappearance());
    }

    // This finalizes the enemy's disappearance
    IEnumerator CompleteEnemyDisappearance() {
        
        // Waits 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // The enemy's mesh renderer will become false
        gameObject.GetComponent<MeshRenderer>().enabled=false;

        // The enemy's destruction sound will play
        GetComponent<AudioSource>().PlayOneShot(enemyDestroySound);

        // After the length of the enemy's destruction sound clip,
        // the enemy will be deactivated
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
