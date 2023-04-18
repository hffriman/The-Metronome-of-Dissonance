using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to manage the player's health (in Stage 1) //
public class HealthManager : MonoBehaviour
{

    // Defining the maximum health and the current health
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    // Defining an audio source in order to play the damage sounds of the player
    public AudioSource innerSoundSystem;

    //Defining the damage and death sounds
    public AudioClip damageSound;
    public AudioClip deathSound;

    // Defining the player object
    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // This makes sure the current health will never be more than the maximum health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Prepares for the player's "death" whenever the health has reached 0 (or under)
        if (currentHealth <= 0)
        {
            playerObject.GetComponent<PlayerController>().PrepareForDisappearance();
        }
    }

    // Restores the player's health
    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    /* 
        Uses the damage argument taken from the enemies, traps or deathzones,
        reduces that damage from the player's currentHealth and plays the correct audio
        depending on the player's status (whether they are alive or dead after the damage)
    */
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth > 0) {
            innerSoundSystem.PlayOneShot(damageSound);
        } else {
            innerSoundSystem.PlayOneShot(deathSound);
        }
    }
}
