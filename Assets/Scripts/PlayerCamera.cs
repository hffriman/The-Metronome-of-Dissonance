using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
