using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : MonoBehaviour {

    //static List<GameObject> wasteFood;

    //private void Start()
    //{
    //    wasteFood = new List<GameObject>();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>() != null || other.GetComponent<Bomb>() != null)
        {
            // Debug.Log(other.name + " went into the compost. What a waste :-(");
            Destroy(other.gameObject);
        }
    }
}
