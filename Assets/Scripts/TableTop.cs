using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : MonoBehaviour {

    [SerializeField] float maxBounceOffForce = 5f;
    //[SerializeField] float maxBounceOffAngle = 50f;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;
        if (collidedObj.GetComponent<Food>() != null)
        {
            Rigidbody rb = collidedObj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;

            Vector3 bounceForce = new Vector3(Random.Range(-maxBounceOffForce, maxBounceOffForce), Random.Range(-maxBounceOffForce, maxBounceOffForce), Random.Range(-maxBounceOffForce, maxBounceOffForce));
            rb.AddForce(bounceForce, ForceMode.VelocityChange);

            //Vector3 bounceAngle = new Vector3(Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle), Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle), Random.Range(maxBounceOffAngle / 2, maxBounceOffAngle));
            //rb.AddTorque(bounceAngle, ForceMode.VelocityChange);
        }
    }

}
