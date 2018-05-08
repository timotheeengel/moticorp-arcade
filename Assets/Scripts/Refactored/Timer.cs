using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Tooltip("Length of Round in seconds")] [SerializeField] float countdown = 30f;
    [SerializeField] float nextRoundDelay = 5f;
    Text text;
    Concierge concierge;
    public bool roundHasStarted = false;
    bool roundHasEnded = false;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        concierge = FindObjectOfType<Concierge>();
        if (concierge == null)
        {
            Debug.LogError("Your restaurant does not have a Concierge!");
        }
        if (text == null)
        {
            Debug.LogWarning("Could not find the Egg timer!");
        }
        text.text = countdown.ToString();
	}
	
    public void StartRound ()
    {
        roundHasStarted = true;
    }

	// Update is called once per frame
	void Update () {
        if (!roundHasStarted)
        {
            // TODO: Rewrite in a more elegant manner
            text.text = "00:" + countdown.ToString();
            return;
        }
        if (!roundHasEnded)
        {
            CurrentRoundTime();
        }
        else
        {
            EndRound();
        }

    }

    void CurrentRoundTime()
    {
        countdown -= Time.deltaTime;
        if (countdown >= Mathf.Epsilon)
        {
            int seconds = (int)countdown;
            if (seconds < 10) // Find a cleaner solution
            {
                text.text = "00:0" + seconds.ToString();
            }
            else
            {
                text.text = "00:" + seconds.ToString();
            }
            return;
        }
        roundHasEnded = true;
        text.text = "00:00";
    }

    void EndRound()
    {
        Invoke("LoadNextRound", nextRoundDelay);
        FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>().SaveOverallScores();
    }

    void LoadNextRound()
    {
        concierge.BringNextCourse("ResultScreen");
    }
}
