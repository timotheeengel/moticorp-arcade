using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetup : MonoBehaviour {

    public static MoveSetup instance;
    
    public delegate void OnStart();
    public event OnStart onStart;

    public static float scale;
    public float scaleX;
    public float offsetY;
    [SerializeField] GameObject masterController;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        instance = this;
        scale = 2;
    }

    // Update is called once per frame
    void Update()
    {
        scaleX = -0.5f / masterController.transform.position.x;
        offsetY = 1.2f - (masterController.transform.position.y * scaleX);
    }
}
