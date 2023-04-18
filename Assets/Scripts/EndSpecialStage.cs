using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
    - This script is used to end the second stage (Special stage),
      after the object this is attached to (Metronome) has been triggered
      by the player object
    - After the triggering, the next scene will begin in 3 seconds
*/
public class EndSpecialStage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SpecialControls>().StopSpinning();
            StartCoroutine(CompleteStage());
        }
    }

    IEnumerator CompleteStage()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
