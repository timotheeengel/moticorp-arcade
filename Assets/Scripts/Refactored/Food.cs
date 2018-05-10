using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] string displayName = "testname";
    [SerializeField] int points;
    [SerializeField] int id;
    [SerializeField] Texture icon;

    public int GetPointValue()
    {
        return points;
    }

    public string GetName()
    {
        return displayName;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int newID)
    {
        id = newID;
    }

    public Texture GetIcon()
    {
        return icon;
    }
}
