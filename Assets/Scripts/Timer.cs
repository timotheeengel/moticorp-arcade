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
    Vector3 eggTimerOriginalLocalAngle;
    Quaternion eggTimerOriginalQuaternion;
    [SerializeField] Transform eggTimerFinalPos;

    float windUpSpeed;
    // TODO: Find way to access countdown animation clip length!
    float windUpTime = 4f / 2;
    bool hasWoundUp = false;
    bool isWindingUp = false;

    // Use this for initialization
    void Start () {
        eggTimerOriginalLocalAngle = eggTimerTop.transform.localEulerAngles;
        eggTimerOriginalQuaternion = eggTimerTop.rotation;
        //Debug.Log("Final Rot " + eggTimerFinalPos.transform.localRotation.eulerAngles.y);
        //Debug.Log("Start Rot " + eggTimerOriginalPos.localRotation.eulerAngles.y);
        //Debug.Log(eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalPos.localRotation.eulerAngles.y);
        // TODO: Find out why we are rotating around Z but the correct values seem to be stored in Y ... !!!
        rotationSpeed = (eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalLocalAngle.y) / countdown;
        windUpSpeed = (eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalLocalAngle.y) / windUpTime;
        Debug.Log(windUpTime);
        Debug.Log(windUpSpeed);


        roundLength = countdown;
        concierge = FindObjectOfType<Concierge>();
        if (concierge == null)
        {
            Debug.LogError("Your restaurant does not have a Concierge!");
        }

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        jukebox = FindObjectOfType<Jukebox>();

        eggTimerTop.transform.localEulerAngles = eggTimerFinalPos.transform.localEulerAngles;
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
        if (roundHasStarted && hasWoundUp)
        {
            EggTimerTurns();
        } else if (isWindingUp)
        {
            EggTimerWindsUp();
        }
    }

   void EggTimerTurns()
    {
        countdown -= Time.deltaTime;
        eggTimerTop.rotation = Quaternion.RotateTowards(eggTimerTop.rotation, eggTimerFinalPos.rotation, Time.deltaTime * rotationSpeed);

        audioSource.volume = Mathf.Lerp(1f, 0.2f, countdown / 10f);
    
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

    void EggTimerWindsUp()
    {
        
        windUpTime -= Time.deltaTime;
        eggTimerTop.rotation = Quaternion.RotateTowards(eggTimerTop.rotation, eggTimerOriginalQuaternion, Time.deltaTime * windUpSpeed);

        if (windUpTime <= Mathf.Epsilon)
        {
            hasWoundUp = true;
        }
    }

    public void WindUpEggTimer()
    {
        isWindingUp = true;
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
