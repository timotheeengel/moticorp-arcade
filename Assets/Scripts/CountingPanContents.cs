using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingPanContents : MonoBehaviour {

    Scoreboard scoreboard;
    CONTROLS playerSide;
    List<GameObject> panContents = new List<GameObject>();
    DisplayPanContents contentDisplay;

    FridgeNew fridge;

    int completedRecipes = 0;

    PhysicMaterial inPanPhysics;
    PhysicMaterial outsidePanPhysics;

    // Use this for initialization
    void Start () {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        fridge = FindObjectOfType<FridgeNew>().GetComponent<FridgeNew>();
        ScriptInitialization();
        contentDisplay = GetComponent<DisplayPanContents>();
        contentDisplay.InitiliazeDisplay(playerSide);

        inPanPhysics = (PhysicMaterial)Resources.Load("Food Pan Physics");
        outsidePanPhysics = (PhysicMaterial)Resources.Load("Food Physics");

    }

    void ScriptInitialization ()
    {
        if (transform.parent.position.x > 0)
        {
            playerSide = CONTROLS.RIGHT;
        }
        else
        {
            playerSide = CONTROLS.LEFT;
        }
    }

    void ToGlowOrNotToGlow ()
    {
        if (fridge.GlowFactor(panContents) == true)
        {
            contentDisplay.RecipeIsComplete(true);
        }
        else
        {
            contentDisplay.RecipeIsComplete(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            other.material = inPanPhysics;

            panContents.Add(other.gameObject);
        }
        SanitizePanContents();
        contentDisplay.UpdateDisplay(panContents);
        ToGlowOrNotToGlow();
    }

    private void OnTriggerExit(Collider other)
    {
        
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            other.material = outsidePanPhysics;

            panContents.Remove(other.gameObject);
            food.HasExitedPan();
        }
        SanitizePanContents();
        contentDisplay.UpdateDisplay(panContents);
        ToGlowOrNotToGlow();
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
                panContents.RemoveAt(i);
                Debug.Log("Removed null item");
                i--;
            }
        }
    }

    public void CountPanContents()
    {
        SanitizePanContents();
        int amountOfFood = panContents.Count;

        int points = 0;
        foreach (GameObject food in panContents)
        {
            points += food.GetComponent<Food>().GetPointValue();
        }
        
        int bonus = fridge.EvaluatePanContent(panContents, playerSide);
        if (bonus > 0)
        {
            completedRecipes++;
        }

        scoreboard.AddScore(playerSide, points + bonus);

        for (int i = 0; i < amountOfFood; i++)
        {
            GameObject temp = panContents[0];
            panContents.RemoveAt(0);
            Destroy(temp);
        }
        contentDisplay.UpdateDisplay(panContents);
        contentDisplay.RecipeIsComplete(false);
    }

    public CONTROLS GetPlayerSide()
    {
        return playerSide;
    }

    public bool isPanEmpty()
    {
        if (panContents.Count == 0)
        {
            return true;
        }
        return false;
    }
}

