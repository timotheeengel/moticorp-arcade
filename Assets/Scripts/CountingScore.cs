using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingScore : MonoBehaviour {

    Scoreboard scoreboard;
    CapsuleCollider capsuleCollider;
    List<GameObject> panContents = new List<GameObject>();
    Scoreboard.SCORE playerSide;

	// Use this for initialization
	void Start () {
        scoreboard = FindObjectOfType<Scoreboard>();
        scoreboard.ResetRoundScores();
        capsuleCollider = GetComponent<CapsuleCollider>();
        ScriptInitialization();
	}

    void ScriptInitialization ()
    {
        if (transform.parent.position.x > 0)
        {
            playerSide = Scoreboard.SCORE.RIGHT;
        }
        else
        {
            playerSide = Scoreboard.SCORE.LEFT;
        }
        print(playerSide);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Add(other.gameObject);
            Debug.Log(other.gameObject.name + " added to the pan");
        }
        if (gameObject.transform.parent.CompareTag("LeftPan") == true )
        {
            scoreboard.AddScoreLeft(other.GetComponent<Food>().GetPointValue());
        } else {
            scoreboard.AddScoreRight(other.GetComponent<Food>().GetPointValue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " removed from the pan");
        }
        if (gameObject.transform.parent.CompareTag("LeftPan") == true)
        {
            scoreboard.AddScoreLeft(-other.GetComponent<Food>().GetPointValue());
        }
        else
        {
            scoreboard.AddScoreRight(-other.GetComponent<Food>().GetPointValue());
        }
    }

    public void CountPanContents()
    {
        int score = 0;
        foreach (GameObject ingredient in panContents)
        {
            score += ingredient.GetComponent<Food>().GetPointValue();
        }
        if (gameObject.transform.parent.CompareTag("LeftPan") == true) {
            scoreboard.AddScoreLeft(score);
        } else {
            scoreboard.AddScoreRight(score);
        }
    }
    

}
