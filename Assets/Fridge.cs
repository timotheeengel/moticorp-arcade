using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    [System.Serializable]
    public struct Content
    {
        public Content(GameObject _ingredient, int _count){
            ingredient = _ingredient.GetComponent<Food>();
            count = _count;
        }

        public Food ingredient;
        public int count;
    }

    public Recipe() { contents = new List<Content>(); }
    public void Add(GameObject _ingredient, int _count){
        contents.Add(new Content(_ingredient, _count));
    }
    public override string ToString()
    {
        string ret = "";
        foreach (var item in contents)
            ret += item.ingredient.displayName + ": " + item.count.ToString() + '\n';
        return ret;
    }

    public List<Content> contents;
}

public class Fridge : MonoBehaviour {

    public static Fridge instance = null;

    [SerializeField] List<GameObject> ingredients;
    private List<Recipe> recipes = new List<Recipe>();

    [System.Serializable] private class RecipeHolder
    {
        public Text text;
        private Recipe recipe;
        
        void FinishRecipe()
        {
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
    }

    void Start () {
        recipes.Add(CreateRecipe());
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
