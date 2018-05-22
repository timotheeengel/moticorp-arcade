using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointCounter : MonoBehaviour {

    [SerializeField] float percentOfTotalPerSecond;
    [SerializeField] float percentOfRemainingPerSecond;

    float PointsInFloat = 0;
    float PointsInInt  = 0;

    // Use this for initialization
    void Start ()
    {
        percentOfTotalPerSecond /= 100;
        percentOfRemainingPerSecond /= 100;
    }

    public void resetScore()
    {
        PointsInFloat = 0;
        PointsInInt = 0;
    }

    public void AddScore(float points)
    {
        StartCoroutine(IncreaseScore(points));
    }

    IEnumerator IncreaseScore(float points)
    {
        float pointsLeft = points;
        float time = 0;
        while (pointsLeft > Mathf.Epsilon)
        {
            time += Time.deltaTime;

            float toAdd = points * Time.deltaTime * percentOfTotalPerSecond;
            toAdd += pointsLeft * Time.deltaTime * percentOfRemainingPerSecond;

            if (toAdd > pointsLeft)
                toAdd = pointsLeft;

            PointsInFloat += toAdd;
            pointsLeft -= toAdd;

            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
        PointsInInt = (int)(PointsInFloat + 0.5f);
        GetComponent<Text>().text = PointsInInt.ToString();
	}
}
