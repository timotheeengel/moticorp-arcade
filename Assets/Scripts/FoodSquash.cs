using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSquash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider pan)
    {
        if (pan.transform.tag != "Pan")
            return;

        Collider coll = GetComponent<Collider>();
        Collider[] colliders = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents);
        
        foreach (var item in colliders)
        {
            if ((item.transform.position - pan.transform.position).magnitude < 0.5f && //TODO magic value
                item.GetComponent<Food>() != null && 
                item.transform.position.y < pan.transform.position.y)
            {
                    item.GetComponent<Food>().Squash();
            }
        }
    }
}
