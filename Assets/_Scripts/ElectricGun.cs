using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElectricGun : MonoBehaviour {

    //Electric gun effect code
    static int charge,maximumCharge;
    float currentTime, lastIncrementedTime, timeToCharge ,resetCoolDownTime;
    bool isReseting;
    public Text chargeText;

	// Use this for initialization
	void Start () {
        maximumCharge = 10;
        charge = maximumCharge;
        currentTime = 0;
        lastIncrementedTime = 0;
        timeToCharge = 1.5f;
        isReseting = false;
        resetCoolDownTime = 3.75f;
    }

    // Update is called once per frame
    void Update() {
        chargeText.text = charge.ToString();
        currentTime = Time.time;
        if (!isReseting)
        {
            if ((currentTime - lastIncrementedTime) >= timeToCharge)
            {
                //Increase charge
                incrementCharge();
                lastIncrementedTime = currentTime;
            }
        }

        else
        {
            //To prevent increment soon after reset
            lastIncrementedTime = currentTime;
        }

        if(charge==0)
        {
            isReseting = true;
            //Reset after a cooldown time
            resetCoolDownTime -= Time.deltaTime;
            if(resetCoolDownTime<=0)
            {
                resetCharge();
                resetCoolDownTime = 3.75f;
                isReseting = false;
            }
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
        if(charge<maximumCharge)
        {
            charge++;
        }
    }

    float getTime()
    {
        return currentTime;
    }

    //Do not make it static
    public void resetCharge()
    {
        charge = maximumCharge;
    }

    public static void decrementCharge()
    {
        charge--;
    }
}
