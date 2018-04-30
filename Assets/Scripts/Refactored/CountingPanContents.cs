using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingPanContents : MonoBehaviour {

    Scoreboard scoreboard;
    Scoreboard.SCORESIDE playerSide;
    List<GameObject> panContents = new List<GameObject>();

    FridgeNew fridge;

    int completedRecipes = 0;

	// Use this for initialization
	void Start () {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        fridge = FindObjectOfType<FridgeNew>().GetComponent<FridgeNew>();
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
        int points = 0;
        foreach (GameObject ingredient in panContents)
        {
            points += ingredient.GetComponent<Food>().GetPointValue();
        }
        int bonus = fridge.EvaluatePanContent(panContents);
        if (bonus > 0)
        {
            completedRecipes++;
            points += bonus;
        }

        scoreboard.AddScore(playerSide, points);
    }
}

