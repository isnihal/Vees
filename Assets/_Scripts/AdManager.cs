using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour {

    public void showAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() {resultCallback=handleAdResult});
        }
    }

    void handleAdResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:Debug.Log("Reward the user");
                break;
            case ShowResult.Skipped:Debug.Log("AD skipped");
                break;
            case ShowResult.Failed:Debug.Log("Ad failed");
                break;
        }
    }
}
