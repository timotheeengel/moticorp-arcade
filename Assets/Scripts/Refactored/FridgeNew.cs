﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FridgeNew : MonoBehaviour {

    [SerializeField] List<GameObject> ingredients;
    [SerializeField] int maxAmountOfSameIngredient = 1;
    [SerializeField] int recipeBonusPoints = 100;
    private List<RecipeItem> recipe;

    // Use this for initialization
    void Start () {
        recipe = new List<RecipeItem>();
        RecipeComparisonTest();
    }
	
    public List<GameObject> GetAvailableIngredients()
    {
        return ingredients;
    }

    private List<RecipeItem> GenerateRecipe()
    {
        if (recipe.Count > 0)
        {
            recipe.Clear();
        }

        List<GameObject> unusedIngredients = ingredients.ToList();
        
        int amountOfIngredients = Random.Range(1, ingredients.Count);
        
        for (int i = 0; i < amountOfIngredients; i++)
        {
            GameObject currentIngredient = unusedIngredients[Random.Range(0, unusedIngredients.Count)];
            int amountOfCurrentIngredient = Random.Range(1, maxAmountOfSameIngredient);

            recipe.Add(new RecipeItem(currentIngredient, amountOfCurrentIngredient));

            unusedIngredients.Remove(currentIngredient);
        }

        return recipe;
    }

    public int EvaluatePanContent(List<GameObject> panContent)
    {
        int basicScore = 0;
        foreach (GameObject ingredient in panContent)
        {
            basicScore += ingredient.GetComponent<Food>().GetPointValue();
        }

        int recipeBonus = 0;
        if (IsRecipeCompletedV2(panContent))
        {
            recipeBonus += recipeBonusPoints;
        }

        return basicScore + recipeBonus;
    }

    // Out-dated! Use V2
    private bool OLDIsRecipeCompletedV1(List<GameObject> panContents)
    {
        List < RecipeItem > adjustedPanContents = new List<RecipeItem>();
        while (panContents.Count > 0)
        {
            GameObject foodType = panContents[0];
            List<GameObject> foodQuantity = panContents.FindAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
            adjustedPanContents.Add(new RecipeItem(panContents[0], foodQuantity.Count));

            panContents.RemoveAll(f => foodType);
        }

        // TODO: Fix THIS!

        foreach (RecipeItem item in recipe) {
            if (!adjustedPanContents.Exists
                    (e =>
                    e.ingredient.GetComponent<Food>().GetID() == item.ingredient.GetComponent<Food>().GetID()
                    && e.quantity >= item.quantity)
                )
            {
                return false;
            }
        }
        return true;
    }

    private bool IsRecipeCompletedV2(List<GameObject> panContents)
    {
        List<RecipeItem> adjustedPanContents = new List<RecipeItem>();
        while (panContents.Count > 0)
        {
            GameObject foodType = panContents[0];
            List<GameObject> foodQuantity = panContents.FindAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
            adjustedPanContents.Add(new RecipeItem(panContents[0], foodQuantity.Count));
            panContents.RemoveAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
        }

        // TODO: Fix THIS!

        foreach (RecipeItem item in recipe)
        {
            bool foundItemInPan = false;
            int foodQ = 0;
            foreach (RecipeItem food in adjustedPanContents)
            {
                if (food.ingredient.GetComponent<Food>().GetID() == item.ingredient.GetComponent<Food>().GetID())
                {
                    foundItemInPan = true;
                    foodQ = food.quantity;
                    break;
                }
            }
            if (foundItemInPan && item.EnoughQ(foodQ))
            {
                continue;
            }
            return false;
        }
        return true;
    }

    private void RecipeComparisonTest()
    {
        GenerateRecipe();
        List<GameObject> panContents = new List<GameObject>();
        for (int i = 0; i < recipe.Count; i++)
        {
            for (int j = 0; j < recipe[i].quantity; j++)
            {
                panContents.Add(recipe[i].ingredient);
            }
        }

        foreach (RecipeItem item in recipe)
        {
            Debug.Log("The recipe contains " + item.ingredient.GetComponent<Food>().GetName() + " Q: " + item.quantity);
        }

        foreach (GameObject ingredient in panContents)
        {
            Debug.Log("The pan contains " + ingredient.GetComponent<Food>().GetName());
        }

        bool checkingListAgainstOneAnother = false;
        checkingListAgainstOneAnother = IsRecipeCompletedV2(panContents);
        print(checkingListAgainstOneAnother);
    }


}

public struct RecipeItem
{
    public GameObject ingredient;
    public int quantity;

    public RecipeItem(GameObject _ingredient, int _quantity)
    {
        ingredient = _ingredient;
        quantity = _quantity;
    }

    public void IncreaseQ (int add)
    {
        quantity += add;
    }

    public bool EnoughQ (int caughtQ)
    {
        if (caughtQ >= quantity)
        {
            Debug.Log(caughtQ + " && " + quantity);
            return true;
        }
        return false;
    }
}