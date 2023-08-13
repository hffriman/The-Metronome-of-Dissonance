using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is used in the Audio Movies, where the player
// can skip some audio movies (tutorials and credits) by pressing the Enter key
public class SkipAudioMovie : MonoBehaviour
{
    private float seconds = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return) && seconds > 5.0f)
        {
            if (SceneManager.GetActiveScene().name != "Credits")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
