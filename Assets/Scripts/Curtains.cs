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

    Vector3 curtainEndPosition;

    bool areCurtainsOpening = false;

	// Use this for initialization
	void Start () {
        concierge = FindObjectOfType<Concierge>();
        curtainLeft = GameObject.Find("CurtainLeft");
        curtainRight = GameObject.Find("CurtainRight");

        curtainEndPosition = new Vector3(curtainsDisappear, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (areCurtainsOpening == false)
        {
            return;
        }
        curtainLeft.transform.position = Vector3.Lerp(curtainLeft.transform.position, -curtainEndPosition, curtainOpeningSpeed * Time.deltaTime);
        curtainRight.transform.position = Vector3.Lerp(curtainRight.transform.position, curtainEndPosition, curtainOpeningSpeed * Time.deltaTime);

        float dist = Vector3.Distance(curtainRight.transform.position, curtainEndPosition);

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
