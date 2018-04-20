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
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        DisplayFinalResutlts();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Submit"))
        {
            concierge.BringNextCourse("SplashScreen");
        }
	}

    void DisplayFinalResutlts()
    {
        PlayerScoreLeft = GameObject.Find("ScoreLeft").GetComponent<Text>();
        PlayerScoreRight = GameObject.Find("ScoreRight").GetComponent<Text>();

        PlayerScoreLeft.text = Scoreboard.leftTotalScore.ToString();
        PlayerScoreRight.text = Scoreboard.rightTotalScore.ToString();

        scoreboard.ResetAlLScores();
    }
}

