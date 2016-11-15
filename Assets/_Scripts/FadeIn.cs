using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

    Color panelImageColor;
    Image panelImage;
    float fadeInTime;

	// Use this for initialization
	void Start () {
        //Smaller the value,Higher the loading time :P
        fadeInTime = 0.75f;
        panelImage = GetComponent<Image>();
        panelImageColor = panelImage.color;
	}
	
	// Update is called once per frame
	void Update () {
        panelImageColor.a-=fadeInTime*Time.deltaTime;
        panelImage.color=panelImageColor;
	}
}
