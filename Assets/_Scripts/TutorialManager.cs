using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    enum States {Basic,Tap_To_Continue,Basic_Enemy,Score,Battery,END}
    static States currentState;
    Animator animator;
    static bool veeSpawned,hasFinishedColliding;

    public Text instructionBoard;

	// Use this for initialization
	void Start () {
        currentState = States.Basic;
        animator = FindObjectOfType<Animator>();
        veeSpawned = false;
        hasFinishedColliding = false;
	}

    void Update()
    {
        if (currentState == States.Basic)
        {
            instructionBoard.text = "SWIPE TO SPAWN A VEE";
            if (veeSpawned)
            {
                veeSpawned = false;
                if (PlayerSpawner.getVeesSpawned() == 1)
                {
                    currentState = States.Tap_To_Continue;
                }
            }
        }

        else if(currentState==States.Tap_To_Continue)
        {
            instructionBoard.text = "GOOD!";
        }

        else if(currentState == States.Basic_Enemy)
        {
            instructionBoard.text = "HIT THE ENEMY VEE";
            if(hasFinishedColliding)
            {
                hasFinishedColliding = false;
                currentState = States.Score;
            }
        }

        else if(currentState==States.Score)
        {
            instructionBoard.text = "EACH HIT WILL INCREASE YOUR SCORE";
        }

        else if (currentState == States.Battery)
        {
            instructionBoard.text = "KEEP AN EYE ON THE BATTERY,VEES COST BATTERY AND REFILS WITH TIME";
        }

        else if(currentState==States.END)
        {
            instructionBoard.text = "YOU ARE READY TO GO!\nCHECK OUT THE VARIOUS GAME MODES";
        }
    }

    public static void setCurrentState(string state)
    {
        switch(state)
        {
            case "TAP_TO_CONTINUE":
                currentState = States.Tap_To_Continue;
                break;
            case "BASIC_ENEMY":
                currentState = States.Basic_Enemy;
                break;
            case "BATTERY":
                currentState = States.Battery;
                break;
            case "END":
                currentState = States.END;
                break;
        }
    }
	
    public static void veeHasSpawned()
    {
        veeSpawned = true;
    }

    public static void veeHasCollided()
    {
        hasFinishedColliding = true;
    }

    public static int getCurentState()
    {
        switch(currentState)
        {
            case States.Basic:return 1;
                break;

            case States.Basic_Enemy:return 2;
                break;
            case States.Tap_To_Continue:
                return 3;
            case States.Score:
                return 4;
            case States.Battery:
                return 5;
            case States.END:
                return 6;
            default:return 1;
        }
    }
}
