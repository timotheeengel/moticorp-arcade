using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FridgeNew : MonoBehaviour {

    [SerializeField] List<GameObject> ingredients;
    [SerializeField] int maxAmountOfSameIngredient = 1;
    [SerializeField] int maxAmountOfDifferentIngredients = 4;
    [SerializeField] int recipeBonusPoints = 100;
    private List<RecipeItem> recipe;
    private RecipeDisplay recipeDisplay;

    // Use this for initialization
    void Start () {
        recipeDisplay = FindObjectOfType<RecipeDisplay>();
        recipe = new List<RecipeItem>();
        GenerateRecipe();
    }

    private void Update()
    {
        if(Debug.isDebugBuild && Input.GetButtonDown("Submit"))
        {
            GenerateRecipe();
        }
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
        
        int amountOfIngredients = Random.Range(1, maxAmountOfDifferentIngredients);
        
        for (int i = 0; i < amountOfIngredients; i++)
        {
            GameObject currentIngredient = unusedIngredients[Random.Range(0, unusedIngredients.Count)];
            int amountOfCurrentIngredient = Random.Range(1, maxAmountOfSameIngredient);

            recipe.Add(new RecipeItem(currentIngredient, amountOfCurrentIngredient));

            unusedIngredients.Remove(currentIngredient);
        }
        recipeDisplay.DisplayRecipe(recipe);
        return recipe;
    }

    public int EvaluatePanContent(List<GameObject> panContent)
    {
        if (IsRecipeCompleted(panContent))
        {
            GenerateRecipe();
            return recipeBonusPoints;
        }
        return 0;
    }


    private bool IsRecipeCompleted(List<GameObject> panContents)
    {
        List<RecipeItem> adjustedPanContents = new List<RecipeItem>();
        List<GameObject> tempPanContents = new List<GameObject>(panContents);

        // The 2 null safe guards below are in place because of 1 single issue:
        // When the player pans overlapt, the ingredient is added to both panContents list. If however one of the players happens to be
        // above their stove at the time, this ingredient's gameobject will be destroyed and deleted from one of the lists but not the other.
        // In other words, the safeguards protect against analyzing ingredient that no longer exist.

        while (tempPanContents.Count > 0)
        {
            GameObject foodType = tempPanContents[0];
            if (foodType == null)
            {
                tempPanContents.Remove(foodType);
                continue;
            }
            List<GameObject> foodQuantity = tempPanContents.FindAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
            adjustedPanContents.Add(new RecipeItem(panContents[0], foodQuantity.Count));
            tempPanContents.RemoveAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
        }
       
        foreach (RecipeItem item in recipe)
        {
            bool foundItemInPan = false;
            int foodQ = 0;
            foreach (RecipeItem food in adjustedPanContents)
            {
                if(food.ingredient == null)
                {
                    continue;
                }
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

        bool checkingListAgainstOneAnother = false;
        checkingListAgainstOneAnother = IsRecipeCompleted(panContents);
        print(checkingListAgainstOneAnother);
    }

    // Out-dated! Use V2
    private bool OLDIsRecipeCompletedV1(List<GameObject> panContents)
    {
        List<RecipeItem> adjustedPanContents = new List<RecipeItem>();
        while (panContents.Count > 0)
        {
            GameObject foodType = panContents[0];
            List<GameObject> foodQuantity = panContents.FindAll(f => f.GetComponent<Food>().GetID() == foodType.GetComponent<Food>().GetID());
            adjustedPanContents.Add(new RecipeItem(panContents[0], foodQuantity.Count));

            panContents.RemoveAll(f => foodType);
        }

        // TODO: Fix THIS!

        foreach (RecipeItem item in recipe)
        {
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
            return true;
        }
        return false;
    }
}
