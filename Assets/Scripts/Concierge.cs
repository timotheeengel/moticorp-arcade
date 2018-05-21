using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Concierge : MonoBehaviour {

    public string[] nonScoringStages;
    GAMESTATE currentState = GAMESTATE.IDLE;
    enum GAMESTATE
    {
        SPLASHSCREEN,
        GAMESCREEN,
        RESULTSCREEN,
        IDLE,
        ERROR
    }

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
    }

    private void Update()
    {
        if (currentState == GAMESTATE.SPLASHSCREEN)
        {
            //if (Input.GetButton("StoveTopLeft") == false && Input.GetButton("StoveTopRight") == false)
            //{
            //    BringNextCourse("Stage_GameShow");
            //}
        } else if (currentState == GAMESTATE.GAMESCREEN)
        {
            // TODO: Anything needed here?
        } else if (currentState == GAMESTATE.RESULTSCREEN)
        {
            if (Input.GetButton("StoveTopLeft") == true && Input.GetButton("StoveTopRight") == true)
            {
                BringNextCourse("SplashScreen");
            } else if(Input.GetButtonDown("NewGame") == true)
            {
                BringNextCourse("Stage_GameShow");
            }
        } else if (currentState == GAMESTATE.IDLE)
        {
            // TODO: Do something
        }
    }

    void ActivateScoreboard(Scene stage, LoadSceneMode modecene)
    {
        bool isScoreUIAvailable = CheckForScoreUI(stage);
        Scoreboard scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        scoreboard.SetActive(isScoreUIAvailable);
        if (isScoreUIAvailable == true)
        {
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

    void GameState(Scene stage, LoadSceneMode modecene)
    {
        string currentScene = stage.name;
        if (currentScene == "SplashScreen" || currentScene == "StartScreen")
        {
            currentState = GAMESTATE.SPLASHSCREEN;
        } else if(currentScene == "Stage_GameShow")
        {
            currentState = GAMESTATE.GAMESCREEN;
        } else if (currentScene == "ResultScreen")
        {
            currentState = GAMESTATE.RESULTSCREEN;
        } else if (currentScene == "IdleScreen")
        {
            currentState = GAMESTATE.IDLE;
        } else
        {
            currentState = GAMESTATE.ERROR;
            Debug.LogError("Scene Unknown, cannot assign GAMESTATE to currentState in Concierge.cs");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ActivateScoreboard;
        SceneManager.sceneLoaded += GameState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ActivateScoreboard;
        SceneManager.sceneLoaded -= GameState;
    }

}

