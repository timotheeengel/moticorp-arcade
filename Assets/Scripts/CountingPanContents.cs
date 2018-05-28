using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingPanContents : MonoBehaviour {

    Scoreboard scoreboard;
    CONTROLS playerSide;
    List<GameObject> panContents = new List<GameObject>();
    List<GameObject> panContentsTraps = new List<GameObject>();
    DisplayPanContents contentDisplay;

    FridgeNew fridge;

    int layerFood;
    int layerFoodCaught;

    int completedRecipes = 0;

    PhysicMaterial inPanPhysics;
    PhysicMaterial outsidePanPhysics;

    ParticleSystem leftOverFX;

    // Use this for initialization
    void Start () {
        scoreboard = FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>();
        fridge = FindObjectOfType<FridgeNew>().GetComponent<FridgeNew>();
        ScriptInitialization();
        contentDisplay = GetComponent<DisplayPanContents>();
        contentDisplay.InitiliazeDisplay(playerSide);

        layerFood = LayerMask.NameToLayer("Food");
        if (layerFood == -1)
        {
            Debug.LogError("The Food Collision Layer does not exist!");
            layerFood = LayerMask.NameToLayer("Default");
        }
        switch(playerSide)
        {
            case CONTROLS.RIGHT:
                layerFoodCaught = LayerMask.NameToLayer("FoodCaughtRight");
                break;
            case CONTROLS.LEFT:
                layerFoodCaught = LayerMask.NameToLayer("FoodCaughtLeft");
                break;
            default:
                layerFood = LayerMask.NameToLayer("Default");
                Debug.LogWarning("Could not identify playerSide! All food will be moved to Default Layer when caught.");
                break;
        }

        inPanPhysics = (PhysicMaterial)Resources.Load("Food Pan Physics");
        outsidePanPhysics = (PhysicMaterial)Resources.Load("Food Physics");

        leftOverFX = GetComponentInChildren<ParticleSystem>();
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
            other.gameObject.layer = layerFoodCaught;
            other.material = inPanPhysics;
            panContents.Add(other.gameObject);
        } else if(other.GetComponent<Boot>() != null)
        {
            panContentsTraps.Add(other.gameObject);
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
            other.gameObject.layer = layerFood;
            other.material = outsidePanPhysics;
            food.HasExitedPan();
            panContents.Remove(other.gameObject);
        }
        else if (other.GetComponent<Boot>() != null)
        {
            panContentsTraps.Remove(other.gameObject);
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


    public void CountLeftOvers()
    {
        leftOverFX.Play();
        CountPanContents();
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

        if (panContentsTraps.Count > 0)
        {
            foreach(GameObject trap in panContentsTraps)
            {
                points -= trap.GetComponent<Trap>().GetPenaltyValue();
            }
        }

        scoreboard.AddScore(playerSide, points + bonus);

        // Deleting everything in pan
        GameObject temp;
        for (int i = 0; i < panContentsTraps.Count; i++)
        {
            temp = panContentsTraps[0];
            panContentsTraps.RemoveAt(0);
            Destroy(temp);
        }

        for (int i = 0; i < amountOfFood; i++)
        {
            temp = panContents[0];
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

