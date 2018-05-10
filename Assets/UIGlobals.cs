using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlobals : MonoBehaviour {

    [SerializeField] int IncomingFoodIconPosY = 1040;
    [SerializeField] Texture IncomingFoodIconBG;
    [SerializeField] float IncomingFoodIconBobSpeed = 10f;
    [SerializeField] float IncomingFoodIconBobMagnitude = 10f;

    public static int incomingFoodIconPosY = 0;
    public static Texture incomingFoodIconBG;
    public static float incomingFoodIconBobSpeed = 0;
    public static float incomingFoodIconBobMagnitude = 0;

    // Use this for initialization
    void Awake () {
        incomingFoodIconBG = IncomingFoodIconBG;
        incomingFoodIconBobMagnitude = IncomingFoodIconBobMagnitude;
        incomingFoodIconBobSpeed = IncomingFoodIconBobSpeed;
        incomingFoodIconPosY = IncomingFoodIconPosY;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
