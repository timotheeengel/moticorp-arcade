using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getwins : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(name == "leftWins")
            GetComponent<Text>().text = "Left wins: " + GameObject.FindObjectOfType<Scoreboard>().leftWin;
        else
            GetComponent<Text>().text = "Right wins: " + GameObject.FindObjectOfType<Scoreboard>().rightWin;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
