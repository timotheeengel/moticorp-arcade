using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour {

    [SerializeField] Texture[] recipeBackground;
    RawImage[] iconDisplays;
    RawImage recipeBG;
    List<RecipeItem> foodIcons;

    private void Awake()
    {
        iconDisplays = GetComponentsInChildren<RawImage>();
        recipeBG = GameObject.Find("RecipeBG").GetComponent<RawImage>();
        // iconDisplays.OrderBy(iconName => iconName.name);
        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }
    }

    void UpdateDisplay(List <RecipeItem> recipeItems)
    {
        // TODO: Make so the current recipeBackground cannot be chosen by the RNG
        recipeBG.texture = recipeBackground[Random.Range(0, recipeBackground.Length)];

        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }

        int offset = 0;
        for (int i = 0; i < foodIcons.Count; i++)
        {
            for (int j = 0; j < foodIcons[i].quantity; j++)
            {
                iconDisplays[offset].enabled = true;
                iconDisplays[offset].texture = foodIcons[i].ingredient.GetComponent<Food>().GetIcon();
                offset++;
            }
        }
    }

    public void DisplayRecipe(List<RecipeItem> currentRecipe)
    {
        foodIcons = currentRecipe;
        UpdateDisplay(foodIcons);
        if (foodIcons.Count > iconDisplays.Length)
        {
            Debug.LogError("Recipe is too long for the UI!");
            return;
        }
    } 
}
