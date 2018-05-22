using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Tooltip("Length of Round in seconds")] [SerializeField] float countdown = 30f;
    [SerializeField] AudioClip tickingSound;
    [SerializeField] AudioClip ringingSound;
    AudioSource audioSource;
    Animator animator;
    Jukebox jukebox;

    float roundLength;
    Concierge concierge;
    public bool roundHasStarted = false;

    float rotationSpeed;
    [SerializeField] Transform eggTimerTop;
    Transform eggTimerOriginalPos;
    [SerializeField] Transform eggTimerFinalPos;

	// Use this for initialization
	void Start () {
        eggTimerOriginalPos = eggTimerTop.transform;
        //Debug.Log("Final Rot " + eggTimerFinalPos.transform.localRotation.eulerAngles.y);
        //Debug.Log("Start Rot " + eggTimerOriginalPos.localRotation.eulerAngles.y);
        //Debug.Log(eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalPos.localRotation.eulerAngles.y);
        // TODO: Find out why we are rotating around Z but the correct values seem to be stored in Y ... !!!
        rotationSpeed = (eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalPos.localRotation.eulerAngles.y) / countdown;

        roundLength = countdown;
        concierge = FindObjectOfType<Concierge>();
        if (concierge == null)
        {
            Debug.LogError("Your restaurant does not have a Concierge!");
        }

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        jukebox = FindObjectOfType<Jukebox>();
    }
	
    public void StartRound ()
    {
        roundHasStarted = true;
        audioSource.clip = tickingSound;
        audioSource.loop = true;
        audioSource.Play();
    }

	// Update is called once per frame
	void Update () {
        if (roundHasStarted)
        {
            EggTimerTurns();
        }
    }

   void EggTimerTurns()
    {
        countdown -= Time.deltaTime;
        eggTimerTop.rotation = Quaternion.RotateTowards(eggTimerTop.rotation, eggTimerFinalPos.rotation, Time.deltaTime * rotationSpeed);

        audioSource.volume = Mathf.Lerp(1f, 0.5f, countdown / 10f);
    
        if (countdown <= Mathf.Epsilon)
        {
            EndRound();
            audioSource.loop = false;
            jukebox.GetComponent<AudioSource>().Stop();
            audioSource.PlayOneShot(ringingSound);
            animator.SetBool("Ringing", true);
            enabled = false;
        }
    }

    void EndRound()
    {
        Invoke("LoadNextRound", ringingSound.length);
        FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>().SaveOverallScores();
    }

    void LoadNextRound()
    {
        concierge.BringNextCourse("ResultScreen");
    }
}
