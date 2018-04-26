using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingPanContents : MonoBehaviour {

    Scoreboard scoreboard;
    List<GameObject> panContents = new List<GameObject>();
    Scoreboard.SCORESIDE playerSide;

	// Use this for initialization
	void Start () {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        // TODO: Factor out ResetRoundScores and adapt to our new Flowchart
        scoreboard.ResetRoundScores();
        ScriptInitialization();
	}

    void ScriptInitialization ()
    {
        if (transform.parent.position.x > 0)
        {
            playerSide = Scoreboard.SCORESIDE.RIGHT;
        }
        else
        {
            playerSide = Scoreboard.SCORESIDE.LEFT;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Add(other.gameObject);
            scoreboard.AddScore(playerSide, other.GetComponent<Food>().GetPointValue());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Remove(other.gameObject);
            scoreboard.AddScore(playerSide, -other.GetComponent<Food>().GetPointValue());
        }
        
    }

    public void AddRecipeBonusScore(int recipePoints)
    {
        scoreboard.AddScore(playerSide, recipePoints);
    }

    // TODO: Check for bugs - possibly the reason we get double the points because we do not differenciate between existing score and new score.
    public void CountPanContents()
    {
        int score = 0;
        foreach (GameObject ingredient in panContents)
        {
            score += ingredient.GetComponent<Food>().GetPointValue();
        }
        scoreboard.AddScore(playerSide, score);
    }
}

