using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTop : MonoBehaviour {

    CONTROLS side;
    [SerializeField] float validationTime = 0.5f;
    [SerializeField] AudioClip validationSound;
    AudioSource audioSource;
    ParticleSystem flame;
    string stoveTopButton;

    float timeOnStove = 0f;
    bool onCorrectStove = false;
    bool countedPanContents = false;
    CountingPanContents pan;

	// Use this for initialization
	void Start () {
		if (transform.position.x < 0)
        {
            side = CONTROLS.LEFT;
            stoveTopButton = "StoveTopLeft";
        } else
        {
            side = CONTROLS.RIGHT;
            stoveTopButton = "StoveTopRight";
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = validationSound;
        flame = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan && side == validPan.GetPlayerSide() && validPan.isPanEmpty() == false)
        {
            pan = validPan;
            onCorrectStove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CountingPanContents validPan = other.gameObject.GetComponentInChildren<CountingPanContents>();
        if (validPan && Input.GetButton(stoveTopButton))
        {
            timeOnStove = 0f;
            onCorrectStove = false;
            countedPanContents = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!countedPanContents && timeOnStove >= validationTime)
        {
            flame.Play();
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(validationSound);
            pan.CountPanContents();
            countedPanContents = true;
        }
        if (onCorrectStove)
        {
            timeOnStove += Time.deltaTime;
        }
    }
}
