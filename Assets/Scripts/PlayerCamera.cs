using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    private float cameraX = 2.0f;
    private float cameraY = 400.0f;
    private float cameraZ = -400.0f;

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
