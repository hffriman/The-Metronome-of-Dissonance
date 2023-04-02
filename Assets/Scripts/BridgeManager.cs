using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{

    public GameObject bridgeEndPoint;

    public string direction;

    private bool pressingCorrectKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((direction == "A" && Input.GetKey("a")) || (direction == "D" && Input.GetKey("d")) || (direction == "W" && Input.GetKey("w")))
        {
            pressingCorrectKey = true;
        }

        else 
        {
            pressingCorrectKey = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log(transform.rotation.z);

        if (other.gameObject.tag == "Player" && pressingCorrectKey)
        {
            other.gameObject.GetComponent<SpecialControls>().MoveCloser(bridgeEndPoint.transform.position);
        }
    }
}
