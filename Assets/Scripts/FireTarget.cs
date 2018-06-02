using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line {Target, Entry}

public class FireTarget : MonoBehaviour {

    public static FireTarget instance;

    Vector3 offset;
    Vector3[] targetPoints = new Vector3[2];
    Vector3[] entryPoints = new Vector3[2];

    public Vector3 GetOffset()
    {
        return offset;
    }

    public Vector3 GetPositionOnLine(Line line, float t) {
        switch (line)
        {
            case Line.Target:
                return targetPoints[0] * t + targetPoints[1] * (1 - t);
                break;
            case Line.Entry:
                return entryPoints[0] * t + entryPoints[1] * (1 - t);
                break;
            default:
                return Vector3.zero;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Surplus FireTarget");
            Destroy(this);
        }
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        GetComponent<LineRenderer>().GetPositions(targetPoints);
        transform.GetChild(0).GetComponent<LineRenderer>().GetPositions(entryPoints);

        offset = ((entryPoints[0] + entryPoints[1]) / 2 - (targetPoints[0] + targetPoints[1]) / 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
