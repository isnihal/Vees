using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour {

    public static void showRewardedAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() {resultCallback=handleAdResult});
        }
    }

    public void rewardAds()//Use this for onclick to buttons
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = handleAdResult });
        }
    }


    public static void showNoRewardedAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    public void noRewardAds()//Use this for onclick to buttons
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    static void handleAdResult(ShowResult result)
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
