using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FridgeNew : MonoBehaviour {

    [SerializeField] List<GameObject> ingredients;
    [SerializeField] int maxAmountOfSameIngredient = 1;
    [SerializeField] int recipeBonusPoints = 100;
    private List<GameObject> recipe = new List<GameObject>();

    // Use this for initialization
    void Start () {
        recipe = new List<GameObject>();
        RecipeComparisonTest();
    }
	
    public List<GameObject> GetAvailableIngredients()
    {
        return ingredients;
    }

    private List<GameObject> GenerateRecipe()
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
            for (int j = 0; j < amountOfCurrentIngredient; j++)
            {
                recipe.Add(currentIngredient);
            }
            unusedIngredients.Remove(currentIngredient);
        }

        return recipe;
    }

    private bool IsRecipeCompleted(List<GameObject> panContents)
    {
        // TODO: Only checks whether all the recipe ingredients are in the pan ONCE! 
        // If several are needed, but only 1 is available in the pan, the value returned is still true
        // Adjust to check whether the right amount of ingredients is in the pan!
        return recipe.All(panContents.Contains);
    }

    private void RecipeComparisonTest()
    {
        GenerateRecipe();
        List<GameObject> panContents = new List<GameObject>();
        for (int i = 0; i < recipe.Count; i++)
        {
            panContents.Add(recipe[i]);
        }

        foreach(GameObject ingredient in recipe)
        {
            Debug.Log("The recipe contains " + ingredient.GetComponent<Food>().GetName());
        }

        foreach (GameObject ingredient in panContents)
        {
            Debug.Log("The pan contains " + ingredient.GetComponent<Food>().GetName());
        }

        bool checkingListAgainstOneAnother = false;
        checkingListAgainstOneAnother = recipe.All(panContents.Contains);
    }

    public int EvaluatePanContent(List<GameObject> panContent)
    {
        int basicScore = 0;
        foreach(GameObject ingredient in panContent)
        {
            basicScore += ingredient.GetComponent<Food>().GetPointValue();
        }

        int recipeBonus = 0;
        if (IsRecipeCompleted(panContent))
        {
            recipeBonus += recipeBonusPoints;
        }

        return basicScore + recipeBonus;
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
}
