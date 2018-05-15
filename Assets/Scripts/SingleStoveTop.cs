using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStoveTop : MonoBehaviour {

    [SerializeField] float validationTime = 0.5f;
    float timeOnStove = 0f;
    bool onCorrectStove = false;
    bool countedPanContents = false;
    CountingPanContents pan;

	// Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan != null)
        {
            pan = validPan;
            onCorrectStove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan != null)
        {
            timeOnStove = 0f;
            onCorrectStove = false;
            countedPanContents = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!countedPanContents && timeOnStove >= validationTime)
        {
            pan.CountPanContents();
            countedPanContents = true;
        }
        if (onCorrectStove)
        {
            timeOnStove += Time.deltaTime;
        }
    }

}
