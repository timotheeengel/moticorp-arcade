using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

    [Tooltip ("in seconds")][SerializeField] int fadeSpeed = 2;

    Text text;
    Concierge concierge;    
    
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        concierge = FindObjectOfType<Concierge>();
        if (concierge == null)
        {
            Debug.LogError("No Concierge found in your Restaurant");
        }
	}
	
	// Update is called once per frame
	void Update () {
        Fading();
	}

    void Fading()
    {
        int runTime = (int) Time.time % fadeSpeed;
        switch (runTime)
        {
            case 0: // fade to black
                text.color = Color.Lerp(text.color, Color.black, fadeSpeed * Time.deltaTime);
                break;
            default: // fade to transparent
                text.color = Color.Lerp(text.color, Color.clear, fadeSpeed * Time.deltaTime);
                break;
        }

    }
}
