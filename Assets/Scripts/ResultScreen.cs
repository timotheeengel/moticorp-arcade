using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    Concierge concierge;

    [SerializeField] SocreTower PlayerScoreLeft;
    [SerializeField] SocreTower PlayerScoreRight;

    [SerializeField] GameObject webcamSystem;

    Text RoundScoreLeft;
    Text RoundScoreRight;
    Text TotalScoreLeft;
    Text TotalScoreRight;

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
        if (Input.GetKeyDown(KeyCode.J))
            TakeAPicture();
	}

    void PlayAgain()
    {
        concierge.BringNextCourse("Stage_GameShow");
    }

    void TakeAPicture()
    {
        Instantiate(webcamSystem, GameObject.Find("Canvas").transform);
    }

    void EndSession()
    {
        concierge.BringNextCourse("SplashScreen");
    }

    void DisplayFinalResults()
    {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        if (scoreboard == null)
        {
            Debug.LogError("Scoreboard missing. Cannot display Scores!");
        }

        RoundScoreLeft = GameObject.Find("RoundScoreLeft").GetComponent<Text>();
        RoundScoreRight = GameObject.Find("RoundScoreRight").GetComponent<Text>();

        TotalScoreLeft = GameObject.Find("TotalScoreLeft").GetComponent<Text>();
        TotalScoreRight = GameObject.Find("TotalScoreRight").GetComponent<Text>();

        // TODO: Display score in amount of recipes completed or as a bar?

        int max = Mathf.Max(scoreboard.leftScore, scoreboard.rightScore, 500);

        PlayerScoreLeft.StartTower(max, scoreboard.leftScore);
        PlayerScoreRight.StartTower(max, scoreboard.rightScore);
        RoundScoreLeft.text = "Round: " + scoreboard.roundLeftScore.ToString();
        RoundScoreRight.text = "Round: " + scoreboard.roundRightScore.ToString();
        TotalScoreLeft.text = "Total: " + scoreboard.overallLeftScore.ToString();
        TotalScoreRight.text = "Total: " + scoreboard.overallRightScore.ToString();

    }
}

