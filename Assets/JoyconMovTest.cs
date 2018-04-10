using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconMovTest : MonoBehaviour
{
    Rigidbody rigidbody;
    JoyconDemo joyconDemo;
    [SerializeField] float movementSpeed = 0.0000000001f;
    [SerializeField] bool gyroMovTest = false;
    [SerializeField] bool accelMovTest = false;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        joyconDemo = GetComponent<JoyconDemo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroMovTest)
        {
            GyroMovement();
        }
        else if (accelMovTest)
        {
            AccelMovement();
        }
    }

    void AccelMovement()
    {
        Vector3 accel = joyconDemo.GetDemoAccel();
        float newPosX = 0;
        float newPosY = 0;
        if (Mathf.Abs(accel.x) > 1)
        {
            print("X accel: " + (int)accel.z);
            newPosX = accel.z * movementSpeed;
        }
        if (Mathf.Abs(accel.y) > 1.5)
        {
            print("Y accel  : " + (int)accel.y);
            newPosY = accel.y * movementSpeed;
        }

        Vector3 newPos = new Vector3(newPosX, newPosY, 0);
        rigidbody.MovePosition(transform.position + newPos);
       
    }

    void GyroMovement()
    {
        Vector3 gyro = joyconDemo.GetDemoGyro();
        float newPosX = 0;
        float newPosY = 0;
        if (Mathf.Abs(gyro.x) > 1)
        {
            print("X gyro: " + (int)gyro.z);
            newPosX = gyro.z * movementSpeed;
        }
        if (Mathf.Abs(gyro.y) > 1)
        {
            print("Y gyro: " + (int)gyro.y);
            newPosY = gyro.y * movementSpeed;
        }

        Vector3 newPos = new Vector3(newPosX, newPosY, 0);
        rigidbody.MovePosition(transform.position + newPos);
    }
}
