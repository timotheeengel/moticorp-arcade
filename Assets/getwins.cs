using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getwins : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(name == "leftWins")
            GetComponent<Text>().text = "Wins: " + GameObject.FindObjectOfType<Scoreboard>().leftWin;
        else
            GetComponent<Text>().text = "Wins: " + GameObject.FindObjectOfType<Scoreboard>().rightWin;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
