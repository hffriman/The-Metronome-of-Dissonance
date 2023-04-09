using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{

    public int waitSeconds = 0;

    private int idleSeconds = 2;

    private int coolDownSeconds = 10;

    private float damage = 40.0f;

    private LayerMask playerLayer;

    private Transform playerDetector;

    private bool playerInSight;

    public AudioClip[] enemyIdleSounds;
 
    public AudioClip currentEnemyIdleSound;

    public AudioClip enemyAttackSound;

    public AudioClip enemyDestroySound;

    private int randomizedNumber;

    private bool hasTakenShot;



    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("PlayerLayer");
        playerDetector = this.transform.GetChild(0);
        hasTakenShot = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(playerDetector.position, 100.0f, playerLayer);
    }

   public void PrepareForEnemyAttack(Collider playerCollider)
   {
        StartCoroutine(StartEnemyAttack(playerCollider));
   }

    IEnumerator StartEnemyAttack(Collider playerCollider)
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
     
    public void PrepareForEnemyDisappearance() 
    {
        hasTakenShot = true;
        this.waitSeconds = 100;
        StartCoroutine(CompleteEnemyDisappearance());
    }

    IEnumerator CompleteEnemyDisappearance() {
        
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MeshRenderer>().enabled=false;
        GetComponent<AudioSource>().PlayOneShot(enemyDestroySound);
        yield return new WaitForSeconds(enemyDestroySound.length);
        gameObject.SetActive(false);
    }
}
