using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Tooltip("Length of Round in seconds")] [SerializeField] float countdown = 30f;
    float roundLength;
    [SerializeField] float nextRoundDelay = 5f;
    Concierge concierge;
    public bool roundHasStarted = false;
    bool roundHasEnded = false;

    float rotationSpeed;
    [SerializeField] Transform eggTimerTop;
    Transform eggTimerOriginalPos;
    [SerializeField] Transform eggTimerFinalPos;

	// Use this for initialization
	void Start () {
        eggTimerOriginalPos = eggTimerTop.transform;
        Debug.Log("Final Rot " + eggTimerFinalPos.transform.localRotation.eulerAngles.y);
        Debug.Log("Start Rot " + eggTimerOriginalPos.localRotation.eulerAngles.y);
        Debug.Log(eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalPos.localRotation.eulerAngles.y);
        // TODO: Find out why we are rotating around Z but the correct values seem to be stored in Y ... !!!
        rotationSpeed = (eggTimerFinalPos.transform.localRotation.eulerAngles.y - eggTimerOriginalPos.localRotation.eulerAngles.y) / countdown;

        roundLength = countdown;
        concierge = FindObjectOfType<Concierge>();
        if (concierge == null)
        {
            Debug.LogError("Your restaurant does not have a Concierge!");
        }
	}
	
    public void StartRound ()
    {
        roundHasStarted = true;
    }

	// Update is called once per frame
	void Update () {
        if (roundHasStarted && !roundHasEnded)
        {
            EggTimerTurns();
        }
        else if (roundHasEnded)
        {
            EndRound();
        }

    }

   void EggTimerTurns()
    {
        countdown -= Time.deltaTime;
        eggTimerTop.rotation = Quaternion.RotateTowards(eggTimerTop.rotation, eggTimerFinalPos.rotation, Time.deltaTime * rotationSpeed);

        if (countdown <= Mathf.Epsilon)
        {
            roundHasEnded = true;
        }
    }

    void EndRound()
    {
        Invoke("LoadNextRound", nextRoundDelay);
        FindObjectOfType<Scoreboard>().GetComponent<Scoreboard>().SaveOverallScores();
    }

    void LoadNextRound()
    {
        concierge.BringNextCourse("ResultScreen");
    }
}
