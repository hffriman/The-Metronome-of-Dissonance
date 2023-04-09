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

        playerObject.GetComponent<CapsuleCollider>().enabled = false;
        playerObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(RespawnPlayer(checkPointCoordinates));
        playerObject.GetComponent<PlayerController>().FlyAway();
    }

     IEnumerator RespawnPlayer(Vector3 checkPointCoordinates)
    {
        yield return new WaitForSeconds(3);
        playerObject.GetComponent<CharacterController>().enabled = false;
        playerObject.transform.position = checkPointCoordinates;
        yield return new WaitForSeconds(1);

        playerObject.GetComponent<HealthManager>().RestoreHealth();
        playerObject.GetComponent<MeshRenderer>().enabled = true;
        playerObject.GetComponent<CapsuleCollider>().enabled = true;
        playerObject.GetComponent<CharacterController>().enabled = true;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf) {
                enemy.SetActive(true);
            }
        }
    }
}
