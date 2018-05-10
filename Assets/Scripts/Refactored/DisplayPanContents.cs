using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CountingPanContents))]
public class DisplayPanContents : MonoBehaviour {

    [Tooltip("in seconds")] [SerializeField] int fadeSpeed = 2;

    RawImage[] panDisplayIcons;
    Outline[] panDisplayOutline;

    Color originalColor;
    bool recipeComplete = false;

    // Use this for initialization
    public void InitiliazeDisplay (CONTROLS playerSide) {
        GameObject display;
        switch (playerSide)
        {
            case CONTROLS.LEFT:
                display = GameObject.Find("PanContentsLeft");
                panDisplayIcons = display.GetComponentsInChildren<RawImage>();
                panDisplayOutline = display.GetComponentsInChildren<Outline>();
                break;
            case CONTROLS.RIGHT:
                display = GameObject.Find("PanContentsRight");
                panDisplayIcons = display.GetComponentsInChildren<RawImage>();
                panDisplayOutline = display.GetComponentsInChildren<Outline>();
                break;
        }

        originalColor = panDisplayOutline[0].effectColor;

        ClearDisplay();
        foreach (Outline outline in panDisplayOutline)
        {
            outline.effectColor = Color.clear;
        }
    }

    public void UpdateDisplay (List<GameObject> panContents)
    {
        ClearDisplay();

        if (panDisplayIcons.Length < panContents.Count)
        {
            Debug.LogWarning("The UI cannot display that much food! Be more selective idiot.");
            return;
        }

        for (int i = 0; i < panContents.Count; i++)
        {
           panDisplayIcons[i].enabled = true;
           panDisplayIcons[i].texture = panContents[i].GetComponent<Food>().GetIcon();
        }

    }

    private void Update()
    {
        if (recipeComplete == true)
        {
            DisplayGlow();
        }
    }

    public void DisplayGlow()
    {
        int runTime = (int)Time.time % fadeSpeed;
        switch (runTime)
        {
            case 0: // fade to original color
                foreach (Outline outline in panDisplayOutline)
                {
                    outline.effectColor = Color.Lerp(outline.effectColor, originalColor, fadeSpeed * Time.deltaTime);
                }
                break;
            default: // fade to transparent
                foreach(Outline outline in panDisplayOutline)
                {
                    outline.effectColor = Color.Lerp(outline.effectColor, Color.clear, fadeSpeed * Time.deltaTime);
                }
                break;
        }
    }

    void ClearDisplay()
    {
        foreach (RawImage icon in panDisplayIcons)
        {
            icon.enabled = false;
        }
    }

    public void RecipeIsComplete (bool yesOrNo)
    {
        recipeComplete = yesOrNo;
        if(recipeComplete == false)
        {
            foreach (Outline outline in panDisplayOutline)
            {
                outline.effectColor = Color.clear;
            }
        }
    }
}
