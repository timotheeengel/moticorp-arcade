using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour {

    Transform Controller1;
    Transform Controller2;

    [SerializeField] Color defaultColor;
    [SerializeField] Color halflightColor;
    [SerializeField] Color highlightColor;

    [SerializeField] float timer;

    Image image;
    RectTransform rectTransform;

    public bool goodToGo;

    bool counting = false;

    Vector3[] worldPosition;
    //Bottom left, top left, top right, bottom right

    bool ControllerHovering(Vector3 pos)
    {
        if (pos.x < worldPosition[3].x &&
            pos.x > worldPosition[1].x &&
            pos.y > worldPosition[3].y &&
            pos.y < worldPosition[1].y)
            return true;
        return false;
    }
    
	// Use this for initialization
	void Awake () {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        worldPosition = new Vector3[4];
        rectTransform.GetWorldCorners(worldPosition);

        Controller1 = GameObject.FindGameObjectWithTag("LeftPan").transform;
        Controller2 = GameObject.FindGameObjectWithTag("RightPan").transform;
    }

    IEnumerator CountUp()
    {
        float time = 0;
        while(time < timer)
        {
            time += Time.deltaTime;
            yield return null;
        }
        goodToGo = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ControllerHovering(Controller1.position) || ControllerHovering(Controller2.position))
        {
            GetComponent<Image>().color = halflightColor;
            if (ControllerHovering(Controller2.position) && ControllerHovering(Controller1.position))
            {
                GetComponent<Image>().color = highlightColor;
                if(counting == false)
                {
                    counting = true;
                    StartCoroutine(CountUp());
                }
            }
            else
            {
                StopAllCoroutines();
                counting = false;
                goodToGo = false;
            }
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
	}
}
