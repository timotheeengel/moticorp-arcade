using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {


    [SerializeField] Vector3 customGravity = new Vector3 (0, -3, 0);
    public static SpawnerScript instance;

    public float rate;
    public float width;
    public float depth;
    public float height;

    public List<GameObject> foodRefs;
//    public GameObject cube;

    GameObject toSpawn;

    private void Awake()
    {
        Physics.gravity = customGravity;
        if (instance)
            Destroy(gameObject);
        instance = this;
    }

    // Use this for initialization
    void Start () {
//        Instantiate(cube).transform.localScale = new Vector3(width*2,height*2,depth*2);
        InvokeRepeating("SpawnFood", 0, rate);
	}

    private Quaternion RandomAngle()
    {
        return Quaternion.Euler(
            Random.Range(0, 360), 
            Random.Range(0, 360), 
            Random.Range(0, 360));
    }

    public void SpawnFood()
    {
        toSpawn = foodRefs[Random.Range(0, foodRefs.Count)];

        Vector3 spawnPosition = new Vector3(
            Random.Range(-width, width),
            height,
            Random.Range(-depth/2, depth/2));
        
        Instantiate(toSpawn, spawnPosition, RandomAngle());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
