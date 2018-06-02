using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour {

    [Tooltip ("The position from which the curtains do not show on Screen anymore")]
    [SerializeField] float curtainsDisappear = 3.0f;
    [SerializeField] float curtainOpeningSpeed = 10.0f;
    Concierge concierge;
    GameObject curtainLeft;
    GameObject curtainRight;

    Vector3 curtainEndPositionLeft;
    Vector3 curtainEndPositionRight;

    bool areCurtainsOpening = false;

	// Use this for initialization
	void Start () {
        concierge = FindObjectOfType<Concierge>();
        curtainLeft = GameObject.Find("CurtainLeft");
        curtainRight = GameObject.Find("CurtainRight");

        curtainEndPositionLeft = new Vector3(-curtainsDisappear, curtainLeft.transform.position.y);
        curtainEndPositionRight = new Vector3(curtainsDisappear, curtainRight.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        if (areCurtainsOpening == false)
        {
            return;
        }
        curtainLeft.transform.position = Vector3.Lerp(curtainLeft.transform.position, curtainEndPositionLeft, curtainOpeningSpeed * Time.deltaTime);
        curtainRight.transform.position = Vector3.Lerp(curtainRight.transform.position, curtainEndPositionRight, curtainOpeningSpeed * Time.deltaTime);

        float dist = Vector3.Distance(curtainRight.transform.position, curtainEndPositionRight);

        // TODO: Replace magic number below. There to compensate for the 
        if (dist <= 0.3f)
        {
            concierge.BringNextCourse("Stage_GameShow");
        }
	}

    public void OpenCurtains()
    {
        areCurtainsOpening = true;
    }
}
