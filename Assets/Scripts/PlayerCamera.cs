using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    - This script is used in Camera (Stage 1)
    - It is defined to follow the player in certain distance
    - The position of the camera gets updated along with the player's position
*/
public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    public float cameraX = 2.0f;
    public float cameraY = 400.0f;
    public float cameraZ = -400.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = player.transform.position;
        cameraPos.x += cameraX;
        cameraPos.y += cameraY;
        cameraPos.z += cameraZ;
        transform.position = cameraPos;
    }
}
