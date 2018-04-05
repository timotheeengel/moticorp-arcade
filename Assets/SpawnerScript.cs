using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public float rate;
    public float width;
    public float depth;
    public float height;

    public List<GameObject> foodRefs;
    public GameObject cube;

	// Use this for initialization
	void Start () {
        Instantiate(cube).transform.localScale = new Vector3(width*2,height*2,depth*2);
        InvokeRepeating("SpawnFood", 0, rate);
	}

    public void SpawnFood()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-width, width),
            height,
            Random.Range(-depth, depth));
        
            Instantiate(foodRefs[Random.Range(0, foodRefs.Count)], spawnPosition, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
