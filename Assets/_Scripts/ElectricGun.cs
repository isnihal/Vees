using UnityEngine;
using System.Collections;

public class ElectricGun : MonoBehaviour {

    //Electric gun effect code
    static int charge;
    float currentTime, lastIncrementedTime, timeToCharge;

	// Use this for initialization
	void Start () {
        charge = 10;
        currentTime = 0;
        lastIncrementedTime = 0;
        timeToCharge = 5;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        if((currentTime-lastIncrementedTime) >=timeToCharge)
        {
            //Increase charge
            incrementCharge();
            lastIncrementedTime = currentTime;
            Debug.Log("Incrementing charge at "+currentTime);
        }

	}

    public static int getCharge()
    {
        return charge;
    }

    public static void setCharge(int lifeToBeSet)
    {
        charge = lifeToBeSet;
    }

    public static void incrementCharge()
    {
        charge++;
    }

    public static void decrementCharge()
    {
        charge--;
    }
}
