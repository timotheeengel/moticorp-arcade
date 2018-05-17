using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] string displayName = "testname";
    [SerializeField] int points;
    [SerializeField] int id;
    [SerializeField] Texture icon;
    [SerializeField] AudioClip landInPan;

    AudioSource audioSource;
    bool hasHitPan = false;
    bool badFood = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitPan == true)
        {
            return;
        }

        PanInput pan = collision.gameObject.GetComponent<PanInput>();
        if (pan != null)
        {
            // audioSource.clip = landInPan;
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(landInPan);
        }
    }

    public void HasExitedPan()
    {
        hasHitPan = false;
    }

    public int GetPointValue()
    {
        return points;
    }

    public void RotAway()
    {
        points = -points;
        badFood = true;
    }

    public bool HasGoneBad()
    {
        return badFood;
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
