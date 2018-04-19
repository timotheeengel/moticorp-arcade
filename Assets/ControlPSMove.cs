using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPSMove : MonoBehaviour {

    [SerializeField] GameObject Controller;
    Rigidbody rg;

    [SerializeField] int JitterFilterSampleCount;
    List<Vector3> posSamples;
    List<Vector2> rollSample;
    
    Vector3 ghostTarget;

	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody>();
        posSamples = new List<Vector3>();
        rollSample = new List<Vector2>();
        while(posSamples.Count<JitterFilterSampleCount)
        {
            posSamples.Add(Vector3.zero);
            rollSample.Add(Vector2.zero);
        }
        MoveSetup.instance.onStart += StartMovement;
	}

    public float CalculateScale()
    {
        return transform.position.x / Controller.transform.position.x;
    }

    IEnumerator Movement()
    {
        Vector3 pos;
        while(true)
        {
            ghostTarget = JitterFilterPos() * MoveSetup.scale;
            Vector3 dest = ghostTarget - transform.position;
            dest /= 2;
            pos = transform.position + dest;
            rg.MovePosition(new Vector3(pos.x, pos.y, 0));
            
            rg.rotation = Quaternion.Euler(0, 0, JitterFilterRoll());

            yield return null;
        }
    }

    float JitterFilterRoll()
    {
        float roll = Mathf.Deg2Rad * Controller.transform.rotation.eulerAngles.z;
        Vector2 sample = new Vector2(Mathf.Sin(roll), Mathf.Cos(roll));
        
        rollSample.Add(sample);
        rollSample.RemoveAt(0);

        Vector2 ret = new Vector2();
        foreach (var item in rollSample)
            ret += item;
        ret /= rollSample.Count;

        return Mathf.Rad2Deg * Mathf.Atan2(ret.x,ret.y);
    }

    Vector3 JitterFilterPos()
    {
        posSamples.Add(Controller.transform.position);
        posSamples.RemoveAt(0);

        Vector3 ret = new Vector3(0, 0, 0);
        foreach (var item in posSamples)
            ret += item;
        ret /= posSamples.Count;

        return ret;
    }

    void StartMovement()
    {
        StartCoroutine(Movement());
    }
}
