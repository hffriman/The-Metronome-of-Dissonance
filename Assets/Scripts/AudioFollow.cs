using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollow : MonoBehaviour
{

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 audioPosition = playerObject.transform.position;
        transform.position = audioPosition;    
    }
}
