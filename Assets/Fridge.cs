using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    public struct FoodType
    {
        public FoodType(GameObject _ingredient, int _count){
            ingredient = _ingredient.GetComponent<Food>();
            number = _count;
        }

        public Food ingredient;
        public int number;
    }
    
    public void Add(GameObject _ingredient, int _count){
        content.Add(new FoodType(_ingredient, _count));
    }
    public override string ToString()
    {
        string ret = "";
        foreach (var item in content)
            ret += item.ingredient.displayName + ": " + item.number.ToString() + '\n';
        return ret;
    }

    public List<FoodType> content;
}

public class Fridge : MonoBehaviour {

    public static Fridge instance = null;

    public static Pan leftPan;
    public static Pan rightPan;

    [SerializeField] List<GameObject> ingredients;
    private List<Recipe> recipes = new List<Recipe>();

    [System.Serializable] private class RecipeHolder
    {
        public Text text;
        private Recipe recipe;
        
        public void FinishRecipe(Recipe pan)
        {
            //Evaluate Contents


            //Add score
            LoadRecipe();
        }

        void LoadRecipe()
        {
            if (instance.recipes.Count > 0)
            {
                recipe = instance.recipes[0];
                text.text = recipe.ToString();
            }
            else
                Debug.Log("No More Recipes");
        }
    }

    [SerializeField] RecipeHolder leftRecipe;
    [SerializeField] RecipeHolder rightRecipe;

    Recipe CreateRecipe()
    {
        Recipe ret = new Recipe();
        ret.Add(ingredients[0], 4);
        ret.Add(ingredients[1], 2);
        return ret;
    }

    private void Awake()
    {
        if (instance)
        {
            Debug.Log("Surplus Fridge!");
            Destroy(gameObject);
        }
        instance = this;
        leftPan = GameObject.FindGameObjectWithTag("LeftPan").GetComponent<Pan>();
        rightPan = GameObject.FindGameObjectWithTag("RightPan").GetComponent<Pan>();
        leftPan.onPanPutDown += leftRecipe.FinishRecipe;
        leftPan.onPanPutDown += rightRecipe.FinishRecipe;
        rightPan.onPanPutDown += leftRecipe.FinishRecipe;
        rightPan.onPanPutDown += rightRecipe.FinishRecipe;
    }

    void Start () {
        recipes.Add(CreateRecipe());
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
