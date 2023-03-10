using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            playerObject.GetComponent<PlayerController>().PrepareForDisappearance();
            currentHealth = maxHealth;
        }
    }
}
