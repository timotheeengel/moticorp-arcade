using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Concierge : MonoBehaviour {

    public string[] nonScoringStages;
    public static GAMESTATE currentState = GAMESTATE.IDLE;
    Scoreboard scoreboard;

    public enum GAMESTATE
    {
        SPLASHSCREEN,
        STARTSCREEN,
        GAMESCREEN,
        RESULTSCREEN,
        IDLE,
        ERROR
    }

    private void Awake()
    {
        if (FindObjectsOfType<Concierge>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        DontDestroyOnLoad(gameObject);
    }

    // TODO: Rename to something more befitting of our theme
    public void BringNextCourse (string CourseName)
    {
        SceneManager.LoadScene(CourseName);
    }

    private void Update()
    {
        if (Input.GetButtonDown("NewGame") == true)
        {
            BringNextCourse("StartScreen");
        }
        if (currentState == GAMESTATE.SPLASHSCREEN)
        {
            //if (Input.GetButton("StoveTopLeft") == false && Input.GetButton("StoveTopRight") == false)
            //{
            //    BringNextCourse("Stage_GameShow");
            //}
        } else if (currentState == GAMESTATE.GAMESCREEN)
        {
            
        } else if (currentState == GAMESTATE.RESULTSCREEN)
        {
            if (Input.GetButton("StoveTopLeft") == true && Input.GetButton("StoveTopRight") == true)
            {
                BringNextCourse("SplashScreen");
            }
        } else if (currentState == GAMESTATE.IDLE)
        {
            // TODO: Do something
        }
    }

    void ActivateScoreboard(Scene stage, LoadSceneMode modecene)
    {
        Debug.Log(stage.name);
        bool isScoreUIAvailable = CheckForScoreUI(stage);
        if (scoreboard == null && isScoreUIAvailable == false)
        {
            return;
        }
        scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        scoreboard.SetActive(isScoreUIAvailable);
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
        if (currentScene == "SplashScreen")
        {
            currentState = GAMESTATE.SPLASHSCREEN;

        } else if (currentScene == "StartScreen") {
            currentState = GAMESTATE.STARTSCREEN;
            if (scoreboard != null) { scoreboard.ResetScores(); }
        } else if (currentScene == "Stage_GameShow") {
            currentState = GAMESTATE.GAMESCREEN;
            scoreboard.ResetScores();
        } else if (currentScene == "ResultScreen")
        {
            currentState = GAMESTATE.RESULTSCREEN;
            scoreboard.WhoWon();
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

