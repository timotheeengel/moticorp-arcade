using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingScore : MonoBehaviour {

    Vector2 movementVector = Vector2.zero;
    Transform targetPos;
    int points;
    [SerializeField] float speed;

    public void SendOff(Transform target, int pointsIn)
    {
        GetComponent<Text>().text = pointsIn.ToString();
        targetPos = target;
        points = pointsIn;
        movementVector = (target.position - transform.position).normalized;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(movementVector * speed * Time.deltaTime);
        if((transform.position - targetPos.position).magnitude < 50)
        {
            targetPos.GetComponent<PointCounter>().AddScore(points);
            Destroy(gameObject);
        }
    }
}
