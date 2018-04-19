using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingScore : MonoBehaviour {

    Scoreboard scoreboard;
    CapsuleCollider capsuleCollider;
    List<GameObject> panContents = new List<GameObject>();

	// Use this for initialization
	void Start () {
        scoreboard = FindObjectOfType<Scoreboard>();
        scoreboard.ResetRoundScores();
        capsuleCollider = GetComponent<CapsuleCollider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Add(other.gameObject);
            Debug.Log(other.gameObject.name + " added to the pan");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>())
        {
            panContents.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " removed from the pan");
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
