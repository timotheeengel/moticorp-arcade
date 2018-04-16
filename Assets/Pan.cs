using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    public delegate void OnPanPutDown(Recipe content);
    public event OnPanPutDown onPanPutDown;

    public List<Recipe.FoodType> content;

    [SerializeField] MeshCollider panContentsVolume;

    IEnumerator EvaluateContents()
    {
        panContentsVolume.enabled = true;
        yield return null;
        onPanPutDown(content);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(EvaluateContents());
        if (Input.GetKeyUp(KeyCode.Space))
            panContentsVolume.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Food collision = other.GetComponent<Food>();
        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].ingredient.id == collision.id)
            {
                content[i] = new Recipe.FoodType(other.gameObject, content[i].number + 1);
                return;
            }
        }
        content.Add(new Recipe.FoodType(other.gameObject, 1));
    }
}
