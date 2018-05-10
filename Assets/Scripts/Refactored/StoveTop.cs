﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTop : MonoBehaviour {

    CONTROLS side;
    [SerializeField] float validationTime = 0.5f;
    float timeOnStove = 0f;
    bool onCorrectStove = false;
    bool countedPanContents = false;
    CountingPanContents pan;

	// Use this for initialization
	void Start () {
		if (transform.position.x < 0)
        {
            side = CONTROLS.LEFT;
            
        } else
        {
            side = CONTROLS.RIGHT;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan && side == validPan.GetPlayerSide())
        {
            pan = validPan;
            onCorrectStove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan && side == validPan.GetPlayerSide())
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
