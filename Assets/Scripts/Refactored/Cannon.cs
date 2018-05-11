using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{

    public static Cannon instance;

    [SerializeField] float muzzleVelocity;
    [SerializeField] float hangtime;

    [SerializeField] int RecipeIngredientWeight;
    [SerializeField] int NonRecipeIngredientWeight;
    [SerializeField] int TrashWeight;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject bomb;
    [SerializeField] Texture bombIcon;
    [SerializeField] Texture bombBG;

    List<GameObject> inRecipe = new List<GameObject>();
    List<GameObject> notInRecipe = new List<GameObject>();

    float duplication = new float();

    [SerializeField] float cooldown = 0;
    float cooldownTimer = 0;

    public void StartRound()
    {
        StartCoroutine(FireFood());
    }

    public void AssembleAmmunitionList()
    {
        inRecipe.Clear();
        notInRecipe.Clear();

        FridgeNew.instance.GetIngredientStatus(inRecipe, notInRecipe, duplication);
    }

    IEnumerator FireFood()
    {
        if (inRecipe.Count == 0)
        {
            yield return null;
        }
        while (true)
        {
            bool isBomb = false;
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
                    isBomb = true;
                    toSpawn = bomb;
                }

                StartCoroutine(Trajectory(FireTarget.instance.GetPositionOnLine(Line.Target, Random.Range(0f, 1f)), Instantiate(
                    toSpawn,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                    ).transform,isBomb));

                GetComponentInChildren<ParticleSystem>().Play();
            }
            yield return null;
        }
    }

    IEnumerator Trajectory(Vector3 target, Transform item, bool isBomb)
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        Vector3 movement = item.position - target;
        Vector3 upwardsMovement = new Vector3(0, 3, 0);//TODO magic number
        while (true)
        {
            movement = target - item.position;
            if (movement.magnitude < 0.1)
            {
                item.Translate(FireTarget.instance.GetOffset(), Space.World);
                break;
            }
            item.Translate((movement.normalized * muzzleVelocity + upwardsMovement) * Time.deltaTime, Space.World);

            yield return null;
        }
        GameObject uiIconbg = new GameObject();
        GameObject uiIcon = new GameObject();
        if(!isBomb)
        {
            uiIconbg.AddComponent<RawImage>().GetComponent<RawImage>().texture = UIGlobals.incomingFoodIconBG;
            uiIcon.AddComponent<RawImage>().GetComponent<RawImage>().texture = item.GetComponent<Food>().GetIcon();
        }
        else
        {
            uiIconbg.AddComponent<RawImage>().GetComponent<RawImage>().texture = bombBG;
            uiIcon.AddComponent<RawImage>().GetComponent<RawImage>().texture = bombIcon;
        }
        uiIconbg.transform.parent = GameObject.Find("UI").transform;
        uiIcon.transform.SetParent(uiIconbg.transform,false);
        uiIconbg.transform.position = new Vector3(Camera.main.WorldToScreenPoint(item.position).x, UIGlobals.incomingFoodIconPosY);
        StartCoroutine(IconBob(uiIconbg.transform, hangtime));

        yield return new WaitForSeconds(hangtime);
        item.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator IconBob(Transform target, float hangtime)
    {
        float time = 0;
        Vector3 basePos = target.position;
        while (time < (hangtime + 0.5f))//TODO magic number
        {
            time += Time.deltaTime;
            target.position = new Vector3(basePos.x, basePos.y + Mathf.Sin(time * UIGlobals.incomingFoodIconBobSpeed) * UIGlobals.incomingFoodIconBobMagnitude, 0);
            yield return null;
        }
        Destroy(target.gameObject);
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
    void Start()
    {
        SetWeights(RecipeIngredientWeight, NonRecipeIngredientWeight, TrashWeight);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
