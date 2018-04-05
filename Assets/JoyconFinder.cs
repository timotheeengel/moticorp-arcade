using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JoyCon
{
    A = KeyCode.Joystick2Button0,
    B = KeyCode.Joystick2Button2,
    X = KeyCode.Joystick2Button1,
    Y = KeyCode.Joystick2Button3,
    Up = KeyCode.Joystick1Button2,
    Left = KeyCode.Joystick1Button0,
    Down = KeyCode.Joystick1Button1,
    Right = KeyCode.Joystick1Button3,
    Plus = KeyCode.Joystick2Button9,
    Minus = KeyCode.Joystick1Button8,
    ZR = KeyCode.Joystick2Button15,
    ZL = KeyCode.Joystick1Button15,
    Home = KeyCode.Joystick2Button12,
    Circle = KeyCode.Joystick1Button13,
    R = KeyCode.Joystick2Button14,
    L = KeyCode.Joystick1Button14,
    RStick = KeyCode.Joystick2Button11,
    LStick = KeyCode.Joystick1Button10,
    RSL = KeyCode.Joystick2Button4,
    RSR = KeyCode.Joystick2Button5,
    LSL = KeyCode.Joystick1Button4,
    LSR = KeyCode.Joystick1Button5,
}

public class JoyconFinder : MonoBehaviour {

    public Transform obj;
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
