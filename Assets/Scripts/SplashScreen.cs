using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    Concierge concierge;

	// Use this for initialization
	void Start () {
        concierge = FindObjectOfType<Concierge>().GetComponent<Concierge>();
        if (concierge == null)
        {
            Debug.LogWarning("No Concierge found in your Restaurant");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit"))
        {
            concierge.BringNextCourse("Main");
        }
	}
}
