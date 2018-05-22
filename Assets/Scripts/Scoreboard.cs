using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private bool isActive = false;

    public int leftScore = 0;
    public int rightScore = 0;

    private PointCounter displayLeftScore;
    private PointCounter displayRightScore;

    // TODO: Save round + overall score as a scriptable object  
    public int roundLeftScore = 0;
    public int roundRightScore = 0;

    // Overall scores + PlayerPrefs keys
    private string MyPlayerPrefsKey_overallLeft = "overallLeftScore";
    public int overallLeftScore = 0;
    private string MyPlayerPrefsKey_overallRight = "overallRightScore";
    public int overallRightScore = 0;

    public static Scoreboard instance;

    [SerializeField] GameObject MovingScore;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            Debug.LogWarning("Scoreboard dishonored the clan and committed harakiri. SAD!");
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
        // Reset Scores = delete key
        if (Input.GetButtonDown("Reset Scores"))
        {
            ResetRoundScores();
        }
        if (Input.GetButtonDown("Reset Overall Scores"))
        {
            ResetOverallScores();
        }
    }

    public void AddScore(CONTROLS side, int points)
    {
        switch (side)
        {
            case CONTROLS.LEFT:
                leftScore += points;
                roundLeftScore += points;
                overallLeftScore += points;
                Instantiate(MovingScore, Camera.main.WorldToScreenPoint(GameObject.Find("Player1").transform.position), Quaternion.identity, GameObject.Find("UI").transform).GetComponent<MovingScore>().SendOff(displayLeftScore.transform, points);
                break;
            case CONTROLS.RIGHT:
                rightScore += points;
                roundRightScore += points;
                overallRightScore += points;
                Instantiate(MovingScore, Camera.main.WorldToScreenPoint(GameObject.Find("Player2").transform.position), Quaternion.identity, GameObject.Find("UI").transform).GetComponent<MovingScore>().SendOff(displayRightScore.transform, points);
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
        displayRightScore.resetScore();
        displayLeftScore.resetScore();
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
            displayLeftScore = GameObject.Find("ScoreLeft").GetComponent<PointCounter>();
            displayRightScore = GameObject.Find("ScoreRight").GetComponent<PointCounter>();
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

