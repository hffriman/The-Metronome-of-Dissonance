using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int waitSeconds = 0;

    public int coolDownSeconds = 9;

    private float damage = 40.0f;


    public AudioClip enemyAudio;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

   public void PrepareForEnemyAttack(Collider playerCollider)
   {
        StartCoroutine(StartEnemyAttack(playerCollider));
   }

    IEnumerator StartEnemyAttack(Collider playerCollider)
    {
        while (playerCollider.gameObject.GetComponent<HealthManager>().currentHealth > 0.0f)
        {
            yield return new WaitForSeconds(waitSeconds);
            playerCollider.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            yield return new WaitForSeconds(coolDownSeconds);
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
