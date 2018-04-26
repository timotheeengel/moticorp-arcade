using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private bool isActive = false;

    public int leftScore = 0;
    public int rightScore = 0;

    private Text displayLeftScore;
    private Text displayRightScore;

    // TODO: Save round + overall score as a scriptable object  
    public static int roundLeftScore = 0;
    public static int roundRightScore = 0;

    public static int overallLeftScore = 0;
    public static int overallRightScore = 0;

    public static Scoreboard instance;

    public enum SCORESIDE
    {
        LEFT,
        RIGHT
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void UpdateDisplay()
    {
        displayLeftScore.text = leftScore.ToString();
        displayRightScore.text = rightScore.ToString();
    }

    public void AddScore(SCORESIDE side, int points)
    {
        switch (side)
        {
            case SCORESIDE.LEFT:
                leftScore += points;
                roundLeftScore += points;
                overallLeftScore += points;
                displayLeftScore.text = leftScore.ToString();
                break;
            case SCORESIDE.RIGHT:
                rightScore += points;
                roundRightScore += points;
                overallRightScore += points;
                displayRightScore.text = rightScore.ToString();
                break;
            default:
                Debug.LogWarning("There are 2 sides to the scoreboard, left and right!");
                break;
        }
    }

    public void ResetScores()
    {
        leftScore = 0;
        rightScore = 0;
        UpdateDisplay();
    }

    public void ResetRoundScores()
    {
        roundLeftScore = 0;
        roundRightScore = 0;
    }

    public void ResetAllScores()
    {
        ResetScores();
        ResetRoundScores();
        overallLeftScore = 0;
        overallRightScore = 0;
    }
    
    public void SetActive(bool state)
    {
        isActive = state;
        if (isActive == true)
        {
            displayLeftScore = GameObject.Find("ScoreLeft").GetComponent<Text>();
            displayRightScore = GameObject.Find("ScoreRight").GetComponent<Text>();
        } else
        {
            Debug.Log("No Scoring in this scene. Remember to  reset the Scores when needed /° ! °\\");
        }
    }

    //public void AddScoreLeft(int points)
    //{
    //    leftScore += points;
    //    roundLeftScore += points;
    //    UpdateDisplay();
    //}

    //public void AddScoreRight(int points)
    //{
    //    rightScore += points;
    //    roundRightScore += points;
    //    UpdateDisplay();
    //}
}

