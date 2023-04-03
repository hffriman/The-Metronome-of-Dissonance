using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int waitSeconds = 0;

    public int coolDownSeconds = 9;

    private float damage = 40.0f;

    private LayerMask playerLayer;

    private Transform playerDetector;

    private bool playerInSight;

    public AudioClip enemyAudio;



    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.GetMask("PlayerLayer");
        playerDetector = this.transform.GetChild(0);
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
            yield return new WaitForSeconds(waitSeconds);
            if (playerInSight == true) 
            {
                playerCollider.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
                yield return new WaitForSeconds(coolDownSeconds);
            }
        }
    }
     
    public void EnemyDisappearance() 
    {
        StartCoroutine(SoundBeforeDisappearance());
    }

    IEnumerator SoundBeforeDisappearance() {
        
        yield return new WaitForSeconds(0);
        gameObject.SetActive(false);
    }

}
