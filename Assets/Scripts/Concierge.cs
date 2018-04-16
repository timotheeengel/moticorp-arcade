using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Concierge : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (FindObjectsOfType<Concierge>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public void BringNextCourse (string CourseName)
    {
        SceneManager.LoadScene(CourseName);
        Debug.Log("Serving " + CourseName + " course");
    }
}
