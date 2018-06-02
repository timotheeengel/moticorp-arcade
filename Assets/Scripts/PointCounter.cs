using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointCounter : MonoBehaviour {

    [SerializeField] float percentOfTotalPerSecond;
    [SerializeField] float percentOfRemainingPerSecond;

    float PointsInFloat = 0;
    float PointsInInt  = 0;

    Text text;

    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
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
        float sign = 1;
        if(points < 0)
        {
            sign = -1;
            points *= sign;
        }
        float pointsLeft = points;
        float time = 0;
        while (pointsLeft > Mathf.Epsilon)
        {
            time += Time.deltaTime;

            float toAdd = points * Time.deltaTime * percentOfTotalPerSecond;
            toAdd += pointsLeft * Time.deltaTime * percentOfRemainingPerSecond;
            
            if (toAdd > pointsLeft)
                toAdd = pointsLeft;

            pointsLeft -= toAdd;
            PointsInFloat += toAdd * sign;

            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
        PointsInInt = (int)(PointsInFloat + 0.5f);
        text.text = PointsInInt.ToString();
	}
}
