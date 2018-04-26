using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeyboard : MonoBehaviour {

    Rigidbody rb;
    [SerializeField] float movementSpeed = 2f;
    CONTROLS player;
    enum CONTROLS { LEFT, RIGHT };

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        if (rb.position.x < 0)
        {
            player = CONTROLS.LEFT;
        } else
        {
            player = CONTROLS.RIGHT;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Debug.isDebugBuild)
        {
            HandleInput();
        }
    }
    
    void HandleInput()
    {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 movement = new Vector3() ;
        switch (player)
        {       
            case CONTROLS.LEFT:
         
                movement = new Vector3(
                    Time.deltaTime * movementSpeed * Input.GetAxis("Horizontal"),
                    Time.deltaTime * movementSpeed * Input.GetAxis("Vertical"),
                    0);
                break;

            case CONTROLS.RIGHT:
                movement = new Vector3(
                    Time.deltaTime * movementSpeed * Input.GetAxis("HorizontalP2"),
                    Time.deltaTime * movementSpeed * Input.GetAxis("VerticalP2"),
                    0);
                break;
        }
        rb.MovePosition(currentPos + movement);

        //movement.y = Mathf.Clamp(movement.y, limitY - currentPos.y, -limitY - currentPos.y);
        //movement.x = Mathf.Clamp(movement.x, -limitX - currentPos.x, limitX - currentPos.x);

    }
}
