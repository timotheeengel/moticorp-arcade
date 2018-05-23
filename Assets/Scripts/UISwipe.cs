using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwipe : MonoBehaviour {

    float UIStartPosY;
    float swipeSpeed = 10f;
    float timer;
    float movementPercentage = 0f;
    bool uiMoveIn = false;
    RectTransform pos;

    // Use this for initialization
	void Start () {
        pos = GetComponent<RectTransform>();
        timer = FindObjectOfType<StartCountDownAnimation>().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        uiMoveIn = true;
        UIStartPosY = pos.localPosition.y;
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
        if (pos.localPosition.y > 0)
        {

            Debug.Log(gameObject.name + Mathf.Lerp(UIStartPosY, 0f, movementPercentage));
            pos.localPosition = new Vector3(0f, Mathf.Lerp(UIStartPosY, 0f, movementPercentage));
            movementPercentage += Time.deltaTime;
        }
        else
            uiMoveIn = false;
    }
}
