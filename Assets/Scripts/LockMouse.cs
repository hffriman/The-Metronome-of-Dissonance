using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is just used to lock and hide the mouse cursor
    in the movie and tutorial sequences
*/
public class LockMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
