using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void moveToEnemy()
    {
        if (TutorialManager.getCurentState() == 3)
        {
            animator.SetTrigger("StrikeEnemy");
        }
    }

    public void changeState(string state)
    {
        TutorialManager.setCurrentState(state);
    }
}
