using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Concierge : MonoBehaviour {

    public string[] nonScoringStages;

	// Use this for initialization
	void Start () {
        if (FindObjectsOfType<Concierge>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    // TODO: Rename to something more befitting of our theme
    public void BringNextCourse (string CourseName)
    {
        SceneManager.LoadScene(CourseName);
        Debug.Log("Serving " + CourseName + " course");
    }

    void ActivateScoreboard(Scene stage, LoadSceneMode modecene)
    {
        bool isScoreUIAvailable = CheckForScoreUI(stage);
        Debug.Log(isScoreUIAvailable);
        Scoreboard scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        scoreboard.SetActive(isScoreUIAvailable);
        if (isScoreUIAvailable == true)
        {
            Debug.Log("reseting scores");
            scoreboard.ResetScores();
        }
    }

    bool CheckForScoreUI(Scene stage)
    {
        for (int i = 0; i < nonScoringStages.Length; i++)
        {
            if (stage.name == nonScoringStages[i])
            {
                return false;
            }
        }
        return true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ActivateScoreboard;
    }

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= ActivateScoreboard;
    //}

}

