using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CountingPanContents))]
public class DisplayPanContents : MonoBehaviour {

    RawImage[] panDisplay;

    // Use this for initialization
    public void InitiliazeDisplay (Scoreboard.SCORESIDE playerSide) {

        switch(playerSide)
        {
            case Scoreboard.SCORESIDE.LEFT:
                panDisplay = GameObject.Find("PanContentsLeft").GetComponentsInChildren<RawImage>();
                break;
            case Scoreboard.SCORESIDE.RIGHT:
                panDisplay = GameObject.Find("PanContentsRight").GetComponentsInChildren<RawImage>();
                break;
        }
        foreach (RawImage icon in panDisplay)
        {
            icon.enabled = false;
        }
    }

    public void UpdateDisplay (List<GameObject> panContents)
    {
        foreach (RawImage icon in panDisplay)
        {
            icon.enabled = false;
        }

        if (panDisplay.Length < panContents.Count)
        {
            Debug.LogWarning("The UI cannot display that much food! Be more selective idiot.");
            return;
        }

        for (int i = 0; i < panContents.Count; i++)
        {
           panDisplay[i].enabled = true;
           panDisplay[i].texture = panContents[i].GetComponent<Food>().GetIcon();
        }
    }
}
