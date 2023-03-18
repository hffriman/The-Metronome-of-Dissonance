using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 shootingDirection) {

        GameObject soundBeam = Instantiate(projectile, transform.position, transform.rotation);

        soundBeam.GetComponent<Rigidbody>().AddRelativeForce(shootingDirection);

    }
}
