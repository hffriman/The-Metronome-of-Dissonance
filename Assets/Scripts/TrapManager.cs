using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

    private bool ableToJump;
    public GameObject trap;

    // Start is called before the first frame update
    void Start()
    {
        ableToJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToJump && Input.GetKeyDown(KeyCode.Space))
        {
            trap.GetComponent<BoxCollider>().enabled = false;
            Debug.Log(trap.name + " deactivated");
            StartCoroutine(TrapReset());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ableToJump = true;
        }
    }

    public IEnumerator TrapReset() {

        yield return new WaitForSeconds(2);
        trap.GetComponent<BoxCollider>().enabled = true;
        ableToJump = false;
        Debug.Log(trap.name + " activated");
    }
}
