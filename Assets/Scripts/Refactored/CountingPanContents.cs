using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingPanContents : MonoBehaviour {

    Scoreboard scoreboard;
    Scoreboard.SCORESIDE playerSide;
    List<GameObject> panContents = new List<GameObject>();
    DisplayPanContents contentDisplay;

    FridgeNew fridge;

    int completedRecipes = 0;

	// Use this for initialization
	void Start () {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        fridge = FindObjectOfType<FridgeNew>().GetComponent<FridgeNew>();
        ScriptInitialization();
        contentDisplay = GetComponent<DisplayPanContents>();
        contentDisplay.InitiliazeDisplay(playerSide);
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
        SanitizePanContents();
        contentDisplay.UpdateDisplay(panContents);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Remove(other.gameObject);
            scoreboard.AddScore(playerSide, -other.GetComponent<Food>().GetPointValue());
        }
        SanitizePanContents();
        contentDisplay.UpdateDisplay(panContents);
    }

    public void AddRecipeBonusScore(int recipePoints)
    {
        scoreboard.AddScore(playerSide, recipePoints);
    }

    void SanitizePanContents()
    {
        for (int i = 0; i < panContents.Count; i++)
        {
            if (panContents[i] == null)
            {
                panContents.Remove(panContents[i]);
                Debug.Log("Removed null item");
                i--;
            }
        }
    }

    public void CountPanContents()
    {
        SanitizePanContents();
        int amountOfFood = panContents.Count;

        int bonus = fridge.EvaluatePanContent(panContents);
        if (bonus > 0)
        {
            completedRecipes++;
            scoreboard.AddScore(playerSide, bonus);
        }

        for (int i = 0; i < amountOfFood; i++)
        {
            GameObject temp = panContents[0];
            panContents.RemoveAt(0);
            Destroy(temp);
        }
        contentDisplay.UpdateDisplay(panContents);
    }

    public Scoreboard.SCORESIDE GetPlayerSide()
    {
        return playerSide;
    }
}

