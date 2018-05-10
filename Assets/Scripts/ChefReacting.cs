using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefReacting : MonoBehaviour {

    public static ChefReacting instance;

    Animator animator;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}

    enum Reaction
    {
        Ok, Great, Excellent, Clean, NotGood, Bad, Awful
    }

    public void React(bool success, int positives, int negatives, CONTROLS pan)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;
        if (!success && positives == 0 && negatives == 0)
            return;
        Reaction reaction = Reaction.Ok;
        
        if (success)
        {
            if (positives > 0 && negatives == 0)
                reaction = Reaction.Great;
            if (positives == 0 && negatives == 0)
                reaction = Reaction.Clean;
            //Excellent makes no sense
        }
        else
        {
            if (positives < 4)
                reaction = Reaction.NotGood;
            if (negatives > 0)
                reaction = Reaction.Bad;
            if (negatives > 1 || positives == 0)
                reaction = Reaction.Awful;
        }

        if (pan == CONTROLS.LEFT)
            animator.SetBool("Left", true);
        else
            animator.SetBool("Left", false);

        switch (reaction)
        {
            case Reaction.Ok:
                animator.SetTrigger("OK");
                break;
            case Reaction.Great:
                animator.SetTrigger("Yum");
                break;
            case Reaction.Excellent:
                animator.SetTrigger("Delicious");
                break;
            case Reaction.Clean:
                //Excellent is inactive and there is no react for clean
                animator.SetTrigger("Delicious");
                break;
            case Reaction.NotGood:
                animator.SetTrigger("Bland");
                break;
            case Reaction.Bad:
                animator.SetTrigger("Tasteless");
                break;
            case Reaction.Awful:
                animator.SetTrigger("Disgraceful");
                break;
            default:
                Debug.LogWarning("Something went horrendously wrong in ChefReacting.cs");
                break;
        }
        animator.SetTrigger("React");
    }
}
