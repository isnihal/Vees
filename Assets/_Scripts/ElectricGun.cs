﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElectricGun : MonoBehaviour {

    //Electric gun effect code
    static int charge,maximumCharge,chargeToDisplay;
    float currentTime, lastIncrementedTime, timeToCharge ,resetCoolDownTime;
    bool isReseting,oneTime;
    public GameObject[] batteryBars;
    public AudioClip rechargeClip,boomClip;

	// Use this for initialization
	void Start () {
        
        maximumCharge = 10;
        charge = maximumCharge;
        currentTime = 0;
        lastIncrementedTime = 0;
        timeToCharge = 0.7f;
        isReseting = false;
        resetCoolDownTime = 4.75f;
        oneTime = true;

    }

    // Update is called once per frame
    void Update() {
        setBattery();
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
            if (GameManager.getLevelName() == "TIME_LAPSE")
            {
                isReseting = true;
                if (oneTime)
                {
                    if (!VolumeManager.getIsMuted())
                    {
                        AudioSource.PlayClipAtPoint(rechargeClip, Vector3.zero);
                    }
                    oneTime = false;
                }
                //Reset after a cooldown time
                resetCoolDownTime -= Time.deltaTime;
                if (resetCoolDownTime <= 0)
                {
                    resetCharge();
                    resetCoolDownTime = 3.75f;
                    isReseting = false;
                }
            }
            else
            {
                isReseting = true;
                if (oneTime)
                {
                    if (!VolumeManager.getIsMuted())
                    {
                        AudioSource.PlayClipAtPoint(boomClip, Vector3.zero);
                    }
                    oneTime = false;
                }
                GameManager.decrementLife();
                resetCharge();
                isReseting = false;
            }
        }
	}

    void setBattery()
    {
        chargeToDisplay= Mathf.CeilToInt((charge/2));
        for(int i=0;i<batteryBars.Length;i++)
        {
            if(i<chargeToDisplay)
            {
                batteryBars[i].active = true;
            }
            else
            {
                batteryBars[i].active = false;
            }
        }
    }

    public static int getCharge()
    {
        return charge;
    }

    public static void incrementCharge()
    {
        if(charge<maximumCharge)
        {
            charge++;
        }
    }

    //Do not make it static
    public void resetCharge()
    {
        oneTime = true;
        charge = maximumCharge;
    }

    public static void decrementCharge()
    {
        charge--;
    }

    public static void setMaximumCharge()
    {
        charge = maximumCharge;
    }
}
