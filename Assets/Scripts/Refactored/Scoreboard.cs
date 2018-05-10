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
    public int roundLeftScore = 0;
    public int roundRightScore = 0;

    // Overall scores + PlayerPrefs keys
    private string MyPlayerPrefsKey_overallLeft = "overallLeftScore";
    public static int overallLeftScore = 0;
    private string MyPlayerPrefsKey_overallRight = "overallRightScore";
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

        overallLeftScore = PlayerPrefs.GetInt(MyPlayerPrefsKey_overallLeft, 0);
        overallRightScore = PlayerPrefs.GetInt(MyPlayerPrefsKey_overallRight, 0);
    }

    private void Update()
    {
        if(Debug.isDebugBuild)
        {
            if (Input.GetButtonDown("Reset Scores"))
            {
                ResetRoundScores();
            }
        }
    }

    void UpdateDisplay()
    {
        if(displayLeftScore != null)
        displayLeftScore.text = leftScore.ToString();
        if (displayRightScore != null)
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
        ResetAllScores();
    }
    
    public void SetActive(bool state)
    {
        isActive = state;
        if (isActive == true)
        {
            displayLeftScore = GameObject.Find("ScoreLeft").GetComponent<Text>();
            displayRightScore = GameObject.Find("ScoreRight").GetComponent<Text>();
            UpdateDisplay();
        } else
        {
            Debug.Log("No Scoring in this scene. Remember to  reset the Scores when needed /° ! °\\");
        }
    }

    void ResetOverallScores ()
    {
        PlayerPrefs.SetInt(MyPlayerPrefsKey_overallLeft, 0);
        PlayerPrefs.SetInt(MyPlayerPrefsKey_overallRight, 0);
    }

    public void SaveOverallScores()
    {
        PlayerPrefs.SetInt(MyPlayerPrefsKey_overallLeft, overallLeftScore);
        PlayerPrefs.SetInt(MyPlayerPrefsKey_overallRight, overallRightScore);
    }
}

