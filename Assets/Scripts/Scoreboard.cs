using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{

    public int leftScore = 0;
    public int rightScore = 0;

    private Text displayLeftScore;
    private Text displayRightScore;

    public static int leftTotalScore = 0;
    public static int rightTotalScore = 0;

    public static Scoreboard instance;

    public enum SCORE
    {
        LEFT,
        RIGHT
    }

    private void Awake()
    {
        //        GetComponent<Renderer>().enabled = false;
        if (instance)
            Destroy(gameObject);
        else instance = this;
    }

    // Use this for initialization
    void Start()
    {
        // TODO: Refactor ScoreBoard because DontDestroyOnLoad only works for parent objects!
        // DontDestroyOnLoad(gameObject);
        displayLeftScore = GameObject.Find("ScoreLeft").GetComponent<Text>();
        displayRightScore = GameObject.Find("ScoreRight").GetComponent<Text>();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayLeftScore.text = leftScore.ToString();
        displayRightScore.text = rightScore.ToString();
    }

    public void AddScoreLeft(int points)
    {
        leftScore += points;
        leftTotalScore += points;
        UpdateDisplay();
    }

    public void AddScoreRight(int points)
    {
        rightScore += points;
        rightTotalScore += points;
        UpdateDisplay();
    }

    public void ResetRoundScores()
    {
        leftScore = 0;
        rightScore = 0;
    }

    public void ResetAlLScores()
    {
        ResetRoundScores();
        leftTotalScore = 0;
        rightTotalScore = 0;
    }
}

