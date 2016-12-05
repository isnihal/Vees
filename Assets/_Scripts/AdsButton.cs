using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsButton : MonoBehaviour
{
    void OnGUI()
    {
        Rect buttonRect = new Rect(10, 10, 150, 50);
        string buttonText = Advertisement.IsReady() ? "Show Ad" : "Waiting...";

        if (GUI.Button(buttonRect, buttonText))
        {
            Advertisement.Show();
        }
    }
}