using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    float killPlane;
    [SerializeField] string displayName = "testname";
    [SerializeField] int points;
    [SerializeField] int id;

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

    public int GetPointValue()
    {
        return points;
    }

    public string GetName()
    {
        return displayName;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int newID)
    {
        id = newID;
    }
}
