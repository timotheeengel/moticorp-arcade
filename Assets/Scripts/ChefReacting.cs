using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefReacting : MonoBehaviour {

    [SerializeField] float reactInterval = 4f;
    float timeSinceLastReaction = 0;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        React();
	}

    void React()
    {
        timeSinceLastReaction += Time.deltaTime;
        if (timeSinceLastReaction <= reactInterval)
        {
            return;
        }

        // Todo: Make proper reaction script
        int react = Random.Range(0,3);
        switch (react)
        {
            case 0:
                animator.SetTrigger("OK");
                break;
            case 1:
                animator.SetTrigger("Delicious");
                break;
            case 2:
                animator.SetTrigger("Yum");
                break;
            default:
                Debug.LogWarning("Something went horrendously wrong in ChefReacting.cs");
                break;
        }
        timeSinceLastReaction = 0;
    }
}
