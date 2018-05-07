using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanInput : MonoBehaviour {

    [SerializeField] GameObject Controller;
    [SerializeField] float movementSpeed = 2f;
    Rigidbody rb;

    Vector3 keyboardPosition = Vector3.zero;
    Vector3 controllerPosition = Vector3.zero;

    CONTROLS player;
    enum CONTROLS { LEFT, RIGHT };

    [SerializeField] int JitterFilterSampleCount;
    List<Vector3> posSamples;
    List<Vector2> rollSample;
    
    Vector3 ghostTarget;

	// Use this for initialization
	void Start ()
    {
        posSamples = new List<Vector3>();
        rollSample = new List<Vector2>();
        while(posSamples.Count<JitterFilterSampleCount)
        {
            posSamples.Add(Vector3.zero);
            rollSample.Add(Vector2.zero);
        }
        rb = GetComponent<Rigidbody>();
        if (rb.position.x < 0)
        {
            player = CONTROLS.LEFT;
            Controller = GameObject.FindGameObjectWithTag("LeftController");
        }
        else
        {
            player = CONTROLS.RIGHT;
            Controller = GameObject.FindGameObjectWithTag("RightController");
        }
        keyboardPosition.y = transform.position.y;
        keyboardPosition.z = transform.position.z;
        //MoveSetup.instance.onStart += StartMovement;
    }

    public float CalculateScale()
    {
        return transform.position.x / Controller.transform.position.x;
    }

    void HandleInput()
    {
        Vector3 movement = new Vector3();
        switch (player)
        {
            case CONTROLS.LEFT:

                movement = new Vector3(
                    Time.deltaTime * movementSpeed * Input.GetAxis("HorizontalP1"),
                    Time.deltaTime * movementSpeed * Input.GetAxis("VerticalP1"),
                    0);
                break;

            case CONTROLS.RIGHT:
                movement = new Vector3(
                    Time.deltaTime * movementSpeed * Input.GetAxis("HorizontalP2"),
                    Time.deltaTime * movementSpeed * Input.GetAxis("VerticalP2"),
                    0);
                break;
        }
        keyboardPosition += movement;

        //movement.y = Mathf.Clamp(movement.y, limitY - currentPos.y, -limitY - currentPos.y);
        //movement.x = Mathf.Clamp(movement.x, -limitX - currentPos.x, limitX - currentPos.x);
    }

    private void FixedUpdate()
    {
        if(Controller!=null)
            HandlePSMoveInput();
        HandleInput();
        rb.MovePosition(controllerPosition + keyboardPosition);
    }

    private void HandlePSMoveInput()
    {
        ghostTarget = JitterFilterPos() * MoveSetup.scale;
        Vector3 dest = ghostTarget - transform.position;
        dest /= 2;
        controllerPosition = transform.position + dest;
        controllerPosition.z = 0;
        rb.rotation = Quaternion.Euler(0, 0, JitterFilterRoll());
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

        return Mathf.Rad2Deg * Mathf.Atan2(ret.x, ret.y);
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
}
