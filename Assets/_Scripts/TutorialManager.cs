using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    enum States {Basic,Basic_Enemy,One_Direction,Arcade,Fast_Escape,Equals,Time_Lapse}
    static States currentState;
    Animator animator;
    static bool veeSpawned;

	// Use this for initialization
	void Start () {
        currentState = States.Basic;
        animator = FindObjectOfType<Animator>();
        if(animator)
        {
            Debug.Log("Animator found");
        }
        veeSpawned = false;
	}

    void Update()
    {
        if (currentState == States.Basic)
        {
            if (veeSpawned)
            {
                veeSpawned = false;
                if (PlayerSpawner.getVeesSpawned() == 1)
                {
                    currentState = States.Basic_Enemy;
                }
            }
        }

        else if(currentState == States.Basic_Enemy)
        {

        }
    }
	
    public static void veeHasSpawned()
    {
        veeSpawned = true;
    }

    public static int getSpawningPosition()
    {
        switch(currentState)
        {
            case States.Basic:return 1;
                break;

            case States.Basic_Enemy:return 2;
                break;
            default:return 1;
        }
    }
}
