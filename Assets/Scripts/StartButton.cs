using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    Curtains curtains;
    bool leftPanOnStart = false;
    bool rightPanOnStart = false;

    // Use this for initialization
    void Start () {
        curtains = FindObjectOfType<Curtains>();
	}

    private void Update()
    {
        if (leftPanOnStart && rightPanOnStart)
        {
            curtains.OpenCurtains();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPan"))
        {
            leftPanOnStart = true;
        }
        else if (other.CompareTag("RightPan"))
        {
            rightPanOnStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftPan"))
        {
            leftPanOnStart = false;
        }
        else if (other.CompareTag("RightPan"))
        {
            rightPanOnStart = false;
        }
    }

}
