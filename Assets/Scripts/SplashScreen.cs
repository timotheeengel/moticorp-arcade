using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    Concierge concierge;
    ActivatePan[] activePans; 

	// Use this for initialization
	void Start () {
        concierge = FindObjectOfType<Concierge>().GetComponent<Concierge>();
        if (concierge == null)
        {
            Debug.LogWarning("No Concierge found in your Restaurant");
        }
        activePans = FindObjectsOfType<ActivatePan>();
        if (activePans.Length < 1)
        {
            Debug.LogError("No Pans detected!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit") || ReadyToPlay())
        {
            concierge.BringNextCourse("Main");
        }
	}

    bool ReadyToPlay ()
    {
        // TODO: Make recursive
        if (activePans[0].IsPanActive() && activePans[1].IsPanActive())
        {
            return true;
        }
        return false;
    }
}
