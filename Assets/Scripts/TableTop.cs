using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : MonoBehaviour {

    [SerializeField] float maxBounceOffForce = 5f;
    [SerializeField] float maxBounceOffAngle = 50f;
    [SerializeField] float destroyDelay = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;
        if (collidedObj.GetComponent<Food>() != null || collidedObj.GetComponent<Boot>() != null)
        {
            collision.gameObject.GetComponent<Bounce>().IntializeBounce(maxBounceOffForce, maxBounceOffAngle, destroyDelay);
        }
    }

}
