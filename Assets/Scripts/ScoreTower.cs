using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTower : MonoBehaviour {

    [SerializeField] float secondsToFullScore;
    Slider slider;
    Text text;
    
    [SerializeField] float targetValue;
    [SerializeField] float currentValue;

    public void StartTower(float max, float target)
    {
        slider.maxValue = max;
        targetValue = target;
        StartCoroutine(GrowTower());
    }

    IEnumerator GrowTower()
    {
        currentValue = 0;
        while (currentValue < targetValue)
        {
            currentValue += slider.maxValue * Time.deltaTime / secondsToFullScore;
            text.text = ((int)currentValue).ToString();
            slider.value = currentValue;
            yield return null;
        }
        text.text = ((int)targetValue).ToString();
        slider.value = targetValue;
    }

	// Use this for initialization
	void Awake () {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
	}
}
