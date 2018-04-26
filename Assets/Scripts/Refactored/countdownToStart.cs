using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownToStart : MonoBehaviour {

    [SerializeField] float SecondsTillStart = 3f;
    Text countDownText;
    Timer timer;
    bool launchedRound = false;

	// Use this for initialization
	void Start () {
        timer = FindObjectOfType<Timer>().GetComponent<Timer>();
        countDownText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (launchedRound == false)
        {
            SecondsTillStart -= Time.deltaTime;

            countDownText.text = "> " + ((int)SecondsTillStart).ToString() + " <";

            if (SecondsTillStart <= Mathf.Epsilon)
            {
                timer.StartRound(true);
                launchedRound = true;
                gameObject.SetActive(!launchedRound);
            }
        }
    }
}
