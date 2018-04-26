using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jukebox : MonoBehaviour {

    [Tooltip ("Please arrange songs according to the scenes' build order!")]
    [SerializeField] AudioClip[] backgroundMusic;

    AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<Jukebox>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Jukebox Out of Order - No AudioSource found");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadNewSong;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadNewSong;
    }

    void LoadNewSong(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (scene.buildIndex < backgroundMusic.Length)
        {
            audioSource.clip = backgroundMusic[scene.buildIndex];
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
