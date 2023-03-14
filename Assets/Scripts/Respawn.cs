using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
