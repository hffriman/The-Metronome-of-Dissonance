using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is used in the Respawn Manager object (in Stage 1)
    - Whenever the player's health reaches 0, the following things happen:
      1. The player's Capsule Collider and Mesh Renderer are disabled
      2. The player will be moved up in the air (in order to make OnTriggerExit function happen in the FightStop object)
      3. After some seconds, the player's character controller will be disabled (in preparation of moving it back to the checkpoint)
      4. The player's new position will be the same as the last checkpoint
      5. After some seconds, the player's health will be restored
      6. Also, the player's Capsule Collider, Mesh Renderer and Character controller will be enabled

    - Note: during the respawn session, all the enemies will be made active again
*/
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
