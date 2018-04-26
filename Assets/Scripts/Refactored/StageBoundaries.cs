using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoundaries : MonoBehaviour {

    [SerializeField] float boundaryX;

    public float GetBoundaryInX()
    {
        return boundaryX;
    }
}
