using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedras : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject granadePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();

        }
    }

    void ThrowGrenade()
    {
        GameObject pedra = Instantiate(granadePrefab, transform.position, transform.rotation);
        Rigidbody rb = pedra.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

    }

}
