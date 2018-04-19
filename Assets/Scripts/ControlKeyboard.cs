using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeyboard : MonoBehaviour {

    Rigidbody rb;
    [SerializeField] float movementSpeed = 10.0f;
    float limitY;
    float limitX;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        limitY = -SpawnerScript.instance.height;
        limitX = SpawnerScript.instance.width;
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

        movement.y = Mathf.Clamp(movement.y, limitY - currentPos.y, -limitY - currentPos.y);
        movement.x = Mathf.Clamp(movement.x, -limitX - currentPos.x, limitX - currentPos.x);
        rb.MovePosition(currentPos + movement);
    }
}
