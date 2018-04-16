using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

    [SerializeField] AudioClip backgroundMusic;

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        if (FindObjectsOfType<Jukebox>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Jukebox Out of Order - No AudioSource found");
        }

        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
