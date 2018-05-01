using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour {

    RawImage[] iconDisplays;
    List<RecipeItem> foodIcons;

    private void Start()
    {
        iconDisplays = GetComponentsInChildren<RawImage>();
        // TODO: Add sorting to the icon display to ensure it shown in the right order!
        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }
    }

    void UpdateDisplay(List <RecipeItem> recipeItems)
    {
        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }
        for (int i = 0; i < foodIcons.Count; i++)
        {
            for (int j = 0; j < foodIcons[i].quantity; j++)
            {
                iconDisplays[i + j].enabled = true;
                iconDisplays[i + j].texture = foodIcons[i].ingredient.GetComponent<Food>().GetIcon();
            }
        }
    }

    public void DisplayRecipe(List<RecipeItem> currentRecipe)
    {
        foodIcons = currentRecipe;
        if (foodIcons.Count > iconDisplays.Length)
        {
            Debug.LogError("Recipe is too long for the UI!");
            return;
        }
        UpdateDisplay(foodIcons);
    } 
}
