using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject playerObject;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disappear(Vector3 checkPointCoordinates)
    {
        playerObject.SetActive(false);
        StartCoroutine(RespawnPlayer(checkPointCoordinates));
    }

     IEnumerator RespawnPlayer(Vector3 checkPointCoordinates)
    {
        yield return new WaitForSeconds(2);
        playerObject.transform.position = checkPointCoordinates;
        playerObject.SetActive(true);

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf) {
                enemy.SetActive(true);
            }
        }
    }
}
