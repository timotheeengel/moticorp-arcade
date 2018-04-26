using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : MonoBehaviour {

    static List<GameObject> wasteFood;

    private void Start()
    {
        wasteFood = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Food>())
        {
            wasteFood.Add(other.gameObject);
            // Debug.Log(other.name + " went into the compost. What a waste :-(");
            Destroy(gameObject);
        }
    }
}
