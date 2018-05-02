using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    Concierge concierge;
    Text PlayerScoreLeft;
    Text PlayerScoreRight;
    Scoreboard scoreboard;

    // Use this for initialization
    void Start () {
        concierge = FindObjectOfType<Concierge>().GetComponent<Concierge>();
        if (concierge == null)
        {
            Debug.LogWarning("No Concierge found in your Restaurant");
        }
        DisplayFinalResults();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit"))
        {
            concierge.BringNextCourse("SplashScreen");
        }
	}

    void DisplayFinalResults()
    {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        if (scoreboard == null)
        {
            Debug.LogError("Scoreboard missing. Cannot display Scores!");
        }

        PlayerScoreLeft = GameObject.Find("ScoreLeft").GetComponent<Text>();
        PlayerScoreRight = GameObject.Find("ScoreRight").GetComponent<Text>();

        PlayerScoreLeft.text = scoreboard.roundLeftScore.ToString();
        PlayerScoreRight.text = scoreboard.roundRightScore.ToString();
    }
}

