using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    bool hasBounce = false;
    Rigidbody rb;
    Vector3 bounceForce;
    Vector3 bounceAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void IntializeBounce(float maxBounceOffForce, float maxBounceOffAngle, float destroyDelay)
    {
        if (hasBounce == true)
        {
            rb.AddForce(bounceForce, ForceMode.VelocityChange);
            return;
        }
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;

        bounceForce = new Vector3(Random.Range(-maxBounceOffForce, maxBounceOffForce), 0f, Random.Range(-maxBounceOffForce, maxBounceOffForce));
        rb.AddForce(bounceForce, ForceMode.VelocityChange);

        bounceAngle = new Vector3(Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle), Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle), Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle));
        rb.AddTorque(bounceAngle, ForceMode.VelocityChange);

        Destroy(gameObject, destroyDelay);

        hasBounce = true;
    }
}
