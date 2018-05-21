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

    [SerializeField] GameObject[] trapList;
    [SerializeField] Texture trapBG;

    [SerializeField] int IncomingFoodIconPosY = 1040;
    [SerializeField] Texture IncomingFoodIconBG;
    [SerializeField] float IncomingFoodIconBobSpeed = 10f;
    [SerializeField] float IncomingFoodIconBobMagnitude = 10f;
    [SerializeField] float IncomingFoodIconExtraHangtime = 0f;

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
        while (inRecipe.Count == 0)
        {
            yield return null;
        }
        while (true)
        {
            bool isTrap = false;
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
                    isTrap = true;
                    toSpawn = trapList[Random.Range(0, trapList.Length)];
                }

                StartCoroutine(Trajectory(FireTarget.instance.GetPositionOnLine(Line.Target, Random.Range(0f, 1f)), Instantiate(
                    toSpawn,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                    ).transform,isTrap));

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
            uiIconbg.AddComponent<RawImage>().GetComponent<RawImage>().texture = IncomingFoodIconBG;
            uiIcon.AddComponent<RawImage>().GetComponent<RawImage>().texture = item.GetComponent<Food>().GetIcon();
        }
        else
        {
            uiIconbg.AddComponent<RawImage>().GetComponent<RawImage>().texture = trapBG;
            uiIcon.AddComponent<RawImage>().GetComponent<RawImage>().texture = item.GetComponent<Trap>().GetIcon();
        }
        uiIconbg.transform.SetParent(GameObject.Find("UI").transform,false);
        uiIcon.transform.SetParent(uiIconbg.transform,false);
        uiIconbg.transform.position = new Vector3(Camera.main.WorldToScreenPoint(item.position).x, IncomingFoodIconPosY);
        StartCoroutine(IconBob(uiIconbg.transform));

        yield return new WaitForSeconds(hangtime);
        item.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator IconBob(Transform target)
    {
        float time = 0;
        Vector3 basePos = target.position;
        while (time < (hangtime + IncomingFoodIconExtraHangtime))
        {
            time += Time.deltaTime;
            target.position = new Vector3(basePos.x, basePos.y + Mathf.Sin(time * IncomingFoodIconBobSpeed) * IncomingFoodIconBobMagnitude, 0);
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

    private void Update()
    {
        Time.timeScale = 1f;
        if (Input.GetKey(KeyCode.J))
            Time.timeScale = 0.1f;
    }
}
