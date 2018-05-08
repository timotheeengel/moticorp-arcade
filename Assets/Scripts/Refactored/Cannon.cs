using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public static Cannon instance;

    [SerializeField] float muzzleVelocity;
    [SerializeField] float hangtime;

    [SerializeField] int RecipeIngredientWeight;
    [SerializeField] int NonRecipeIngredientWeight;
    [SerializeField] int TrashWeight;
    [SerializeField] Transform spawnPoint;

    List<GameObject> inRecipe = new List<GameObject>();
    List<GameObject> notInRecipe = new List<GameObject>();

    float duplication = new float();

    [SerializeField] float cooldown = 0;
    float cooldownTimer = 0;
    
    public void AssembleAmmunitionList()
    {
        inRecipe.Clear();
        notInRecipe.Clear();

        FridgeNew.instance.GetIngredientStatus(inRecipe, notInRecipe, duplication);
    }

    IEnumerator FireFood()
    {
        if(inRecipe.Count == 0)
        {
            yield return null;
        }
        while (true)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                cooldownTimer += cooldown;
                GameObject toSpawn;
                int roll = Random.Range(0, TrashWeight);

                if (roll < RecipeIngredientWeight)
                    toSpawn = inRecipe[Random.Range(0, inRecipe.Count)];
                else if (roll < NonRecipeIngredientWeight)
                    toSpawn = notInRecipe[Random.Range(0, notInRecipe.Count)];
                else
                {
                    Debug.Log("Tried to fire trash, but that hasn't been implemented");
                    toSpawn = new GameObject();
                }

                Instantiate(
                    toSpawn,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                    ).GetComponent<Food>().Flight(FireTarget.instance.GetPositionOnLine(Line.Target,Random.Range(0f,1f)), muzzleVelocity, hangtime);

                GetComponentInChildren<ParticleSystem>().Play();
            }
            yield return null;
        }
    }

    //A value of -1 keeps it at current
    public void SetWeights(int inRecipe = -1, int notInRecipe = -1, int trash = -1)
    {
        if (inRecipe != -1)
            RecipeIngredientWeight = inRecipe;
        if (notInRecipe != -1)
            NonRecipeIngredientWeight = notInRecipe + RecipeIngredientWeight;
        if (trash != -1)
            TrashWeight = trash + NonRecipeIngredientWeight;
    }

    public void SetCooldown(int seconds)
    {
        cooldown = seconds;
    }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Surplus Cannon");
            Destroy(this);
        }
        else
            instance = this;
    }

    // Use this for initialization
    void Start () {
        SetWeights(RecipeIngredientWeight,NonRecipeIngredientWeight,TrashWeight);
        StartCoroutine(FireFood());
	}
}
