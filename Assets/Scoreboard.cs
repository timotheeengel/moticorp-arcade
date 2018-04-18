using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    public int leftScore;
    public int rightScore;

    public static Scoreboard instance;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        if (instance)
            Destroy(gameObject);
        instance = this;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
