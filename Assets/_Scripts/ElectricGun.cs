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
        resetCoolDownTime = 4.5f;
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
            lastIncrementedTime = currentTime;
        }

        if(charge==0)
        {
            isReseting = true;
            resetCoolDownTime -= Time.deltaTime;
            if(resetCoolDownTime<=0)
            {
                resetCharge();
                resetCoolDownTime = 4.5f;
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
