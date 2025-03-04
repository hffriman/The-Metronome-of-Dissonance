using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    This script has the same purpose as DamageManager,
    but this one is dedicated to DeathZone areas of the game
*/

public class DeathZone : MonoBehaviour
{

    private float damage = 100.0f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
}
