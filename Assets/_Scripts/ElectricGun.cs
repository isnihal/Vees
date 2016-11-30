using UnityEngine;
using System.Collections;

public class ElectricGun : MonoBehaviour {

    //Electric gun effect code
    static int charge;
    float currentTime, lastIncrementedTime, timeToCharge ,resetCoolDownTime;
    bool isReseting;

	// Use this for initialization
	void Start () {
        charge = 10;
        currentTime = 0;
        lastIncrementedTime = 0;
        timeToCharge = 1.5f;
        isReseting = false;
        resetCoolDownTime = 3f;
    }

    // Update is called once per frame
    void Update() {
        if (!isReseting)
        {
            currentTime = Time.time;
            if ((currentTime - lastIncrementedTime) >= timeToCharge)
            {
                //Increase charge
                incrementCharge();
                lastIncrementedTime = currentTime;
                Debug.Log("Incrementing charge at " + currentTime);
            }
        }

        if(charge==0)
        {
            isReseting = true;
            Invoke("resetCharge", 3f);
        }
        else
        {
            isReseting = false;
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

    //Do not make it static
    public void resetCharge()
    {
        charge = 10;
    }

    public static void decrementCharge()
    {
        charge--;
    }
}
