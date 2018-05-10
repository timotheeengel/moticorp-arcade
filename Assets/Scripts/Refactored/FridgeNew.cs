using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FridgeNew : MonoBehaviour {

    public static FridgeNew instance = null;

    [SerializeField] List<GameObject> ingredients;
    [SerializeField] List<GameObject> traps;
    [SerializeField] int maxAmountOfSameIngredient = 1;
    [SerializeField] int maxAmountOfDifferentIngredients = 4;
    [SerializeField] int recipeBonusPoints = 100;
    private List<RecipeItem> recipe;
    private RecipeDisplay recipeDisplay;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Surplus Fridge!", gameObject);
            Destroy(gameObject);
        }
        instance = this;
    }

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
        Cannon.instance.AssembleAmmunitionList();
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

    public bool GlowFactor (List<GameObject> currentPanContents)
    {
        return IsRecipeCompleted(currentPanContents);
    }

    private bool IsRecipeCompleted(List<GameObject> panContents)
    {
        List<RecipeItem> adjustedPanContents = new List<RecipeItem>();
        List<GameObject> tempPanContents = new List<GameObject>(panContents);

        // The 2 null safe guards below are in place because of 1 single issue:
        // When the player pans overlapt, the ingredient is added to both panContents list. If however one of the players happens to be
        // above their stove at the time, this ingredient's gameobject will be destroyed and deleted from one of the lists but not the other.
        // In other words, the safeguards protect against analyzing ingredient that no longer exist.

        //int count = 0;
        while (tempPanContents.Count > 0)
        {
            GameObject foodType = tempPanContents[0];

            //Debug.Log("Current foodtype " + foodType.GetComponent<Food>().GetName());
            if (foodType == null)
            {
                tempPanContents.Remove(foodType);
                continue;
            }

            Food food = foodType.GetComponent<Food>();
            int foodQuantity = 0;
            for (int i = 0; i < tempPanContents.Count; i++)
            {
                if (tempPanContents == null)
                {
                    tempPanContents.Remove(tempPanContents[i]);
                    i--;
                    continue;
                } else if (tempPanContents[i].GetComponent<Food>().GetID() == food.GetID())
                {
                    foodQuantity++;
                }
            }
            adjustedPanContents.Add(new RecipeItem(tempPanContents[0], foodQuantity));

            //Debug.Log("Found " + foodQuantity.Count + " " + foodType.GetComponent<Food>().GetName());
            //Debug.Log("Added " + adjustedPanContents[count].quantity + " " + adjustedPanContents[count].ingredient.GetComponent<Food>().GetName());
            //Debug.Log(adjustedPanContents.Count);
            //count++;

            for (int i = 0; i < tempPanContents.Count; i++)
            {
                if (tempPanContents[i].GetComponent<Food>().GetID() == food.GetID())
                {
                    tempPanContents.RemoveAt(i);
                    i--;
                }
            }
        }

        // Suggested by Tommi - Not working either
        //int count = 0;
        //foreach (RecipeItem required in recipe)
        //{
        //    Food outer = required.ingredient.GetComponent<Food>();
        //    foreach (RecipeItem have in adjustedPanContents)
        //    {
        //        Food inner = have.ingredient.GetComponent<Food>();
        //        if (outer.GetID() == inner.GetID() && have.quantity >= required.quantity)
        //        {
        //            count++;
        //            continue;
        //        }
        //    }
        //}

        //Debug.Log(count == recipe.Count);

        //if (count == recipe.Count)
        //    return true;

        //return false;

        for (int i = 0; i < recipe.Count; i++)
        {
            bool foundItemInPan = false;
            int foodQ = 0;
            for(int j = 0; j < adjustedPanContents.Count; j++)
            {
                if (adjustedPanContents[j].ingredient == null)
                {
                    continue;
                }
                if (adjustedPanContents[j].ingredient.GetComponent<Food>().GetID() == recipe[i].ingredient.GetComponent<Food>().GetID())
                {
                    foundItemInPan = true;
                    foodQ = adjustedPanContents[j].quantity;
                    break;
                }
            }
            if (foundItemInPan && recipe[i].EnoughQ(foodQ))
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
        panContents.Add(panContents[0]);

        foreach (GameObject content in panContents)
        {
            Debug.Log(content.GetComponent<Food>().GetName());
        }

        foreach (RecipeItem item in recipe)
        {
            Debug.Log(item.ingredient.GetComponent<Food>().GetName() + " " + item.quantity);
        }

        bool checkingListAgainstOneAnother = false;
        checkingListAgainstOneAnother = IsRecipeCompleted(panContents);
        Debug.Log(checkingListAgainstOneAnother);
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

    public void GetIngredientStatus(List<GameObject> InRecipe, List<GameObject> NotInRecipe, float duplication)
    {
        float total = 0;
        float unique = 0;
        foreach (var item in recipe)
        {
            unique++;
            for (int i = 0; i < item.quantity; i++)
            {
                total++;
                InRecipe.Add(item.ingredient);
            }
        }
        NotInRecipe.AddRange(ingredients.FindAll(f => recipe.Exists(e => e.ingredient.GetComponent<Food>().GetID() == f.GetComponent<Food>().GetID()) == false));//:^)
        duplication = total / unique;
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
