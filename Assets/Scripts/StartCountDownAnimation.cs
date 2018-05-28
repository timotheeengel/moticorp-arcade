using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDownAnimation : MonoBehaviour {

    Timer timer;
    Cannon cannon;
	// Use this for initialization
	void Start () {
        timer = FindObjectOfType<Timer>();
        cannon = FindObjectOfType<Cannon>();
	}

    public void StartGame()
    {
        cannon.StartRound();
        timer.StartRound();
        gameObject.SetActive(false);
    }

    public void WindUpTimer()
    {
        timer.WindUpEggTimer();
    }
}
