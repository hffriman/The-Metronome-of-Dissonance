using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStop : MonoBehaviour
{

    public GameObject soundWeaponSelector;

    public GameObject[] enemies;

    private int defeatedEnemies;

    private bool allDefeated;

    private bool isOnFightStop;

    // Start is called before the first frame update
    void Start()
    {
        allDefeated = false;
        isOnFightStop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOnFightStop)
        {
            CheckEnemyAmount();
            if (allDefeated)
            {
                soundWeaponSelector.GetComponent<SoundFight>().EndSoundFight();
                isOnFightStop = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            isOnFightStop = true;
            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<PlayerController>().StopPlayer();
            soundWeaponSelector.GetComponent<SoundFight>().CommenceSoundFight();
        }
    }

    private void CheckEnemyAmount()
    {
        defeatedEnemies = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                defeatedEnemies++;
            }
        }

        if (defeatedEnemies == enemies.Length)
        {
            allDefeated = true;
        }
    }
}
