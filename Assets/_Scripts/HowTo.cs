using UnityEngine;
using UnityEngine.UI;


public class HowTo : MonoBehaviour {

    public Text instructionText;

	// Use this for initialization
	void Start () {
	    switch(LevelManager.getFromLevel())
        {
            //One Direction
            case 3:
                instructionText.text = "1)Player Vee goes from Left to Right\n" +
                    "2)Enemy Vees goes from Top to Bottom\n" +
                    "3)Dont let enemy vees reach Bottom\n" +
                    "4)Each enemy Vee reaching Bottom costs a life";
                break;

            //Escape
            case 5:instructionText.text = "1)Dont hit any enemy Vees\n" +
                    "2)Player Vees goes from Bottom to Top\n" +
                    "3)Vees reaching top awards an escape";
                break;

            //Equals
            case 6:
                instructionText.text = "1)One Player Vee for One Enemy Vee\n" +
                    "2)Missing one enemy Vee leads to Game over\n" +
                    "3)Player Vee that doesnt strike Enemy Vee also leads to Game Over\n" +
                    "4)'N'th Wave will 'N' number of Vees\n"+
                    "5)Complete 'N' hits to unlock a new wave\n"+
                    "6)Wave number is displayed on top center";
                break;

            //Lapse
            case 7:
                instructionText.text = "1)60 second marathon\n" +
                    "2)Hit as many vees as possible\n" +
                    "3)Look out for bombs,They reduce your score by 25";
                break;


        }
	}
}
