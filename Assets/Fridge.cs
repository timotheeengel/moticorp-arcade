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
        CalculateScore();
    }
    public void CalculateScore()//TODO: Presently only ingredient score*2
    {
        score = 0;
        foreach (var item in content)
        {
            score += item.ingredient.value * item.number;
        }
        score *= 2;
    }
    public int score = 0;
    public List<FoodType> content = new List<FoodType>();

    public override string ToString()
    {
        string ret = "";
        foreach (var item in content)
            ret += item.ingredient.displayName + ": " + item.number.ToString() + '\n';
        ret += "Value: " + score;
        return ret;
    }
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

        public void Init()
        {
            LoadRecipe();
        }

        public void FinishRecipe(List<Recipe.FoodType> pan)
        {
            //Evaluate Contents
            Debug.Log("Evaluating contents: ");
            foreach (var item in pan)
            {
                Debug.Log(item.ingredient.displayName);
                Debug.Log(item.number);
            }
            Debug.Log("Against: ");
            foreach (var item in recipe.content)
            {
                Debug.Log(item.ingredient.displayName);
                Debug.Log(item.number);
            }
            foreach (var item in recipe.content)
            {
                if(!pan.Exists(e => 
                    e.ingredient.id == item.ingredient.id && 
                    e.number == item.number
                    ))
                    return;
            }

            //Add score
            //Remove contents
            
            LoadRecipe();
        }

        void LoadRecipe()
        {
            if (instance.recipes.Count > 0)
            {
                recipe = instance.recipes[0];
                instance.recipes.RemoveAt(0);
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

        for (int i = 0; i < ingredients.Count; i++)
            ingredients[i].GetComponent<Food>().id = i;
    }

    void Start ()
    {
        recipes.Add(CreateRecipe());
        recipes.Add(CreateRecipe());
        leftRecipe.Init();
        rightRecipe.Init();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
