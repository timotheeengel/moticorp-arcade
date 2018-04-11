using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    float killPlane;
    MeshCollider meshCollider;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        killPlane = -SpawnerScript.instance.height;

        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();



    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < killPlane)
            Destroy(gameObject);
	}
}
