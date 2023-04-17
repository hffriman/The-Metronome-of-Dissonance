using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is only used for those enemy objects
    that take all the damage from the player
    (Example: Shadow areas in Stage 1)
 */

public class DamageManager : MonoBehaviour
{

    public float damage = 100.0f;

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
