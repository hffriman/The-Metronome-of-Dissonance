using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    - This script is used for the Arrow objects in Stage 1
    - When the player touches this Arrow object, the player
      will change its moving direction to where the Arrow points at
*/
public class DetectPlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The Arrow's direction will be given as a parameter to the PlayerController script's ChangeDirection function
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ChangeDirection(this.transform.rotation.z);
        }
    }
}
