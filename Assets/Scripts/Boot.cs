using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Trap {

    // Use this for initialization

    [SerializeField] GameObject stink;

    private void OnCollisionEnter(Collision collision)
    {
        Food food = collision.gameObject.GetComponent<Food>();
        if (food == null || food.HasGoneBad() == true)
        {
            return;
        }
        food.RotAway();
        GameObject temp = Instantiate(stink);
        temp.transform.parent = collision.gameObject.transform;

        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }

}
