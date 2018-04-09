using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public float killPlane;
    public List<GameObject> foods;

	// Use this for initialization
	//void Start () {
	//	if(Random.value < 0.5f)
    //
	//}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < killPlane)
            Destroy(gameObject);
	}
}
