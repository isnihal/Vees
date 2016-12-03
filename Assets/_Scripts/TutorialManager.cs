using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    enum States {Basic,Basic_Enemy,One_Direction,Arcade,Fast_Escape,Equals,Time_LAPSE}
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
        if(currentState==States.Basic)
        {
            if(veeSpawned)
            {
                Debug.Log("Vee spawned");
                veeSpawned = false;
            }
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

            default:return 1;
        }
    }
}
