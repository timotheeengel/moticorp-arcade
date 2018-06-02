using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDownAnimation : MonoBehaviour {

    Timer timer;
    Cannon cannon;

    Jukebox jukebox;
    AudioSource audioSource;
    [SerializeField] AudioClip countdownSound;


	// Use this for initialization
	void Start () {
        jukebox = FindObjectOfType<Jukebox>();
        jukebox.GetComponent<AudioSource>().Stop();

        timer = FindObjectOfType<Timer>();
        cannon = FindObjectOfType<Cannon>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = countdownSound;
        audioSource.loop = false;
	}

    public void StartCountdown()
    {
        GetComponent<Animator>().SetTrigger("StartCountdown");
        audioSource.Play();
    }

    public void StartGame()
    {
        jukebox.GetComponent<AudioSource>().Play();
        cannon.StartRound();
        timer.StartRound();
        gameObject.SetActive(false);
    }

    public void WindUpTimer()
    {
        timer.WindUpEggTimer();
    }
}
