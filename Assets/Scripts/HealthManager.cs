using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    public AudioSource innerSoundSystem;

    public AudioClip damageSound;
    public AudioClip deathSound;

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
        }
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth > 0) {
            innerSoundSystem.PlayOneShot(damageSound);
        } else {
            innerSoundSystem.PlayOneShot(deathSound);
        }

        Debug.Log("Has taken damage");


    }
}
