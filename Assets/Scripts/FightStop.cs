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
            other.gameObject.GetComponent<PlayerController>().StopPlayer();
            soundWeaponSelector.GetComponent<SoundFight>().CommenceSoundFight();

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyManager>().PrepareForEnemyAttack(other);
            }

        }
    }

    private void CheckEnemyAmount()
    {
        defeatedEnemies = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeSelf)
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
