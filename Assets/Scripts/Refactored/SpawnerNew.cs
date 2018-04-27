using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNew : MonoBehaviour
{
    [SerializeField] Vector3 customGravity = new Vector3(0, -20, 0);
    [SerializeField] float spawnRate = 1f;

    List<GameObject> ingredientsList;


    private StageBoundaries stageBoundaries;

    private void Awake()
    {
        // TODO: Do we really need to set a custom gravity? 
        // Or should simply handle it through the regular Unity Physics interface?
        Physics.gravity = customGravity;
    }

    private void Start()
    {
        stageBoundaries = FindObjectOfType<StageBoundaries>().GetComponent<StageBoundaries>();
        ingredientsList = FindObjectOfType<FridgeNew>().GetComponent<FridgeNew>().GetAvailableIngredients();
        InvokeRepeating("SpawnFood", 0, spawnRate);
    }

    private Quaternion RandomAngle()
    {
        return Quaternion.Euler(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360));
    }

    public void SpawnFood()
    {
        GameObject toSpawnNext = ingredientsList[Random.Range(0, ingredientsList.Count)];

        Vector3 spawnPosition = new Vector3(
            Random.Range(-stageBoundaries.GetBoundaryInX(), stageBoundaries.GetBoundaryInX()),
            transform.position.y,
            0);

        Instantiate(toSpawnNext, spawnPosition, RandomAngle(), transform);
        toSpawnNext.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}