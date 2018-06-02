using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    [SerializeField] Texture icon;
    [SerializeField] int penalty = 40;

    public Texture GetIcon()
    {
        return icon;
    }

    public int GetPenaltyValue()
    {
        return penalty;
    }

}
