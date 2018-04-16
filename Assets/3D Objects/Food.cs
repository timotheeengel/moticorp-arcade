using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    float killPlane;
    public string displayName = "testname";
    public int id;

    // Use this for initialization
    void Start()
    {
        killPlane = -SpawnerScript.instance.height;
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < killPlane)
            Destroy(gameObject);
	}
}
