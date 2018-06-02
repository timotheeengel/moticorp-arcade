using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>() != null || other.GetComponent<Trap>() != null)
        {
            // Debug.Log(other.name + " went into the compost. What a waste :-(");
            Destroy(other.gameObject);
        }
    }
}
