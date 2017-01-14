﻿using UnityEngine;

//Specify the platform here

/*IOS instructions
1)Turn off all services
2)Change platform to iOS
 */
public class PlatformManager : MonoBehaviour {
    public static string platform = "ANDROID"; //IOS OTHERWISE //CAPS
}

/*Scripts affecting iOS,Remove certain libraries there
#1 AdManager.cs
#2 GameManager.cs
#3 IAPManager.cs
#4 LevelManager.cs
#5 Scoreboard.cs
*/