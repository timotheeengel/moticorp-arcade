using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : MonoBehaviour {

    [SerializeField] float maxBounceOffForce = 1000000000f;
    [SerializeField] float maxBounceOffAngle = 500000f;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;
        if (collidedObj.GetComponent<Food>() != null)
        {
            Rigidbody rb = collidedObj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(new Vector3(Random.Range(maxBounceOffForce / 2, maxBounceOffForce), 0f, Random.Range(maxBounceOffForce / 2, maxBounceOffForce)), ForceMode.Acceleration);
            rb.AddTorque(new Vector3 (Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle), 0f), ForceMode.Acceleration);
        }
    }

}
