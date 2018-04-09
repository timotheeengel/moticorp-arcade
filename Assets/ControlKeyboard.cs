using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeyboard : MonoBehaviour {

    Rigidbody rigidbody;
    [SerializeField] float movementSpeed = 10.0f;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}
    
    void HandleInput()
    {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 movement = new Vector3 (
            Time.deltaTime * movementSpeed * Input.GetAxis("Horizontal"),
            Time.deltaTime * movementSpeed * Input.GetAxis("Vertical"),
            0);

        rigidbody.MovePosition(currentPos + movement);
    }
}
