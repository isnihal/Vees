using UnityEngine;
using System.Collections;

public class ElectricGun : MonoBehaviour {

    //Electric gun effect code
    static int charge;

	// Use this for initialization
	void Start () {
        charge = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
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
