using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetup : MonoBehaviour {

    public static MoveSetup instance;
    
    public delegate void OnStart();
    public event OnStart onStart;

    public static float scale;
    [SerializeField] GameObject masterController;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        instance = this;
    }

    // Use this for initialization
    void Start () {
        scale = 5;
	}

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            scale = 3;
            //onStart();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
