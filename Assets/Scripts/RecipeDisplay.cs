using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour {

    [SerializeField] Texture[] recipeBackground;
    RawImage[] iconDisplay1;
    RawImage[] iconDisplay2;
    RawImage[] iconDisplay3;
    RawImage[] iconDisplay4;
    RawImage[] iconDisplay5;
    List<RawImage[]> iconDisplays = new List<RawImage[]>();

    RawImage[] currentDisplay;
    RawImage recipeBG;
    List<RecipeItem> foodIcons;

    AudioSource audioSource;
    [SerializeField] AudioClip noteOn;
    [SerializeField] AudioClip noteOff;

    [SerializeField] float recipeBGrotation = 3f;

    int currentRecipeBG;

    private void Awake()
    {
        iconDisplay1 = GameObject.Find("RecipeD1").GetComponentsInChildren<RawImage>();
        iconDisplay2 = GameObject.Find("RecipeD2").GetComponentsInChildren<RawImage>();
        iconDisplay3 = GameObject.Find("RecipeD3").GetComponentsInChildren<RawImage>();
        iconDisplay4 = GameObject.Find("RecipeD4").GetComponentsInChildren<RawImage>();
        iconDisplay5 = GameObject.Find("RecipeD5").GetComponentsInChildren<RawImage>();
        iconDisplays.Add(iconDisplay1);
        iconDisplays.Add(iconDisplay2);
        iconDisplays.Add(iconDisplay3);
        iconDisplays.Add(iconDisplay4);
        iconDisplays.Add(iconDisplay5);

        // TODO: Add sort by size safeguard.

        foreach (RawImage[] display in iconDisplays)
        {
            disableIcons(display);
        }

        audioSource = GetComponent<AudioSource>();
        recipeBG = GameObject.Find("RecipeBG").GetComponent<RawImage>();
        // iconDisplays.OrderBy(iconName => iconName.name);
    }

    void disableIcons (RawImage[] iconDisplays)
    {
        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }
    }

    void UpdateDisplay(List <RecipeItem> recipeItems)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(noteOff);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.clip = noteOn;
        audioSource.PlayDelayed(noteOff.length);

        if(currentDisplay != null)
        {
            RawImage[] previousDisplay = currentDisplay;
            disableIcons(previousDisplay);
        }

        // TODO: Possibly add check for right sized list safeguard
        int displayLength = 0;
        foreach (RecipeItem item in recipeItems)
        {
            displayLength += item.quantity;
        }

        currentDisplay = iconDisplays[displayLength - 1];

        int newRecipeBG;
        do{
            newRecipeBG = Random.Range(0, recipeBackground.Length);
        } while (newRecipeBG != currentRecipeBG);
        currentRecipeBG = newRecipeBG;
        recipeBG.texture = recipeBackground[newRecipeBG];
        Vector3 newRot = new Vector3 (0f, 0f, Random.Range(-recipeBGrotation, recipeBGrotation));
        recipeBG.rectTransform.rotation = Quaternion.Euler(newRot);

        int offset = 0;

        for (int i = 0; i < foodIcons.Count; i++)
        {
            for (int j = 0; j < foodIcons[i].quantity; j++)
            {
                currentDisplay[offset].enabled = true;
                currentDisplay[offset].texture = foodIcons[i].ingredient.GetComponent<Food>().GetIcon();
                offset++;
            }
        }
    }

    public void DisplayRecipe(List<RecipeItem> currentRecipe)
    {
        foodIcons = currentRecipe;
        UpdateDisplay(foodIcons);
        if (foodIcons.Count > currentDisplay.Length)
        {
            Debug.LogError("Recipe is too long for the UI!");
            return;
        }
    } 
}
