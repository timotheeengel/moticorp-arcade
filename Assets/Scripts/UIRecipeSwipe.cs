using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecipeSwipe : MonoBehaviour {

    float UIStartPosY;
    float swipeSpeed = 10f;
    float timer;
    float movementPercentage = 0f;
    float UIStartScale;
    bool uiMoveIn = false;
    RectTransform pos;
    Animator anim;

    // Use this for initialization
	void Start () {
        pos = GetComponent<RectTransform>();
        timer = FindObjectOfType<StartCountDownAnimation>().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        anim = FindObjectOfType<StartCountDownAnimation>().GetComponent<Animator>();

        uiMoveIn = true;
        UIStartPosY = pos.localPosition.y;
        UIStartScale = pos.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (uiMoveIn)
        {
            SwipeDown();
        }
	}

    void SwipeDown()
    {
        if (pos.localPosition.y < 0)
        {
            pos.localPosition = new Vector3(0f, Mathf.Lerp(UIStartPosY, 0f, movementPercentage));
            float newScale = Mathf.Lerp(UIStartScale, 1f, movementPercentage);
            pos.localScale = new Vector3(newScale, newScale, 1f);
            movementPercentage += Time.deltaTime;
        }
        else
        {
            uiMoveIn = false;
            anim.SetTrigger("StartCountdown");
        }
    }
}
