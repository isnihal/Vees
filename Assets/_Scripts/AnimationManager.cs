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

    public void moveToScore()
    {
        if (TutorialManager.getCurentState() == 4)
        {
            animator.SetTrigger("ShowScore");
        }
    }

    public void moveToBattery()
    {
        if (TutorialManager.getCurentState() == 5)
        {
            animator.SetTrigger("ShowBattery");
        }
    }

    public void moveToEnd()
    {
        if(TutorialManager.getCurentState()==6)
        {
            animator.SetTrigger("ShowEnd");
        }
    }

    public void loadLevel()
    {
        Application.LoadLevel(1);
    }

    public void changeState(string state)
    {
        TutorialManager.setCurrentState(state);
    }
}
