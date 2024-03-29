﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePan : MonoBehaviour {

    Text readyText;
    PSMoveSplash psMoveController;
    bool isPanActive = false;

    // Use this for initialization
    void Start () {
		readyText = GetComponent<Text>();
        psMoveController = GetComponentInChildren<PSMoveSplash>();
    }
	
	// Update is called once per frame
	void Update () {
        if (true)
        {
            isPanActive = true;
            readyText.enabled = true;
        } else
        {
            readyText.enabled = false;
            isPanActive = false;
        }
    }

    public bool IsPanActive()
    {
        return isPanActive;
    }
}
