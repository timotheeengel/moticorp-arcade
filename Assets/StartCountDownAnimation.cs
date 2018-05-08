using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDownAnimation : MonoBehaviour {

    Timer timer;

	// Use this for initialization
	void Start () {
        timer = FindObjectOfType<Timer>();
	}

    public void StartGame()
    {
        timer.StartRound();
        gameObject.SetActive(false);
    }
}
