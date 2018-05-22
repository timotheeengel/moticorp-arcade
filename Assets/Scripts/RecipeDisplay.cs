﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour {

    [SerializeField] Texture[] recipeBackground;
    RawImage[] iconDisplays;
    RawImage recipeBG;
    List<RecipeItem> foodIcons;

    AudioSource audioSource;
    [SerializeField] AudioClip noteOn;
    [SerializeField] AudioClip noteOff;

    [SerializeField] float recipeBGrotation = 3f;

    int currentRecipeBG;

    private void Awake()
    {
        iconDisplays = GetComponentsInChildren<RawImage>();
        audioSource = GetComponent<AudioSource>();
        recipeBG = GameObject.Find("RecipeBG").GetComponent<RawImage>();
        // iconDisplays.OrderBy(iconName => iconName.name);
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

        int newRecipeBG;
        do{
            newRecipeBG = Random.Range(0, recipeBackground.Length);
        } while (newRecipeBG != currentRecipeBG);
        currentRecipeBG = newRecipeBG;
        recipeBG.texture = recipeBackground[newRecipeBG];
        Vector3 newRot = new Vector3 (0f, 0f, Random.Range(-recipeBGrotation, recipeBGrotation));
        recipeBG.rectTransform.rotation = Quaternion.Euler(newRot);

        foreach (RawImage icon in iconDisplays)
        {
            icon.enabled = false;
        }

        int offset = 0;
        for (int i = 0; i < foodIcons.Count; i++)
        {
            for (int j = 0; j < foodIcons[i].quantity; j++)
            {
                iconDisplays[offset].enabled = true;
                iconDisplays[offset].texture = foodIcons[i].ingredient.GetComponent<Food>().GetIcon();
                offset++;
            }
        }
    }

    public void DisplayRecipe(List<RecipeItem> currentRecipe)
    {
        foodIcons = currentRecipe;
        UpdateDisplay(foodIcons);
        if (foodIcons.Count > iconDisplays.Length)
        {
            Debug.LogError("Recipe is too long for the UI!");
            return;
        }
    } 
}
