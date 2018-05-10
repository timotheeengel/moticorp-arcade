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
    Transform eggTimerTop;
    Quaternion eggTimerOriginalPos;
    [SerializeField] Transform eggTimerFinalPos;

	// Use this for initialization
	void Start () {
        eggTimerTop = GameObject.Find("EggTimerTop").transform;
        eggTimerOriginalPos = eggTimerTop.rotation;

        rotationSpeed = (eggTimerFinalPos.transform.rotation.z - eggTimerOriginalPos.z) * countdown;

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
        // TODO: Remove magic number and find why it ain't working!
        eggTimerTop.rotation = Quaternion.RotateTowards(eggTimerTop.rotation, eggTimerFinalPos.rotation, Time.deltaTime * rotationSpeed / 4);

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
