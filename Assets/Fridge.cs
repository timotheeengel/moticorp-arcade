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

    [SerializeField] List<GameObject> ingredients;
    private List<Recipe> recipes = new List<Recipe>();

    [System.Serializable] private class RecipeHolder
    {
        public Text text;
        private Recipe recipe;

        Fridge fridge;

        void FinishRecipe()
        {
            LoadRecipe();
        }

        void LoadRecipe()
        {
            if (fridge.recipes.Count > 0)
            {
                recipe = fridge.recipes[0];
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

	void Start () {
        recipes.Add(CreateRecipe());
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
