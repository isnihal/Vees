using UnityEngine;
using UnityEngine.Advertisements;//Remove for IOS


public class AdManager : MonoBehaviour {

    static ToastManager toastManager;

    void Start()
    {
        if (PlatformManager.platform == "ANDROID")
        {
            toastManager = FindObjectOfType<ToastManager>();
        }
    }


    public static void showRewardedAd()
    {
        if (PlatformManager.platform == "ANDROID")
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = handleAdResult });
            }
            else
            {
                toastManager.showToastOnUiThread("Check Your Internet Connection");
            }
        }
    }

    public void rewardAds()//Use this for onclick to buttons
    {
        if (PlatformManager.platform == "ANDROID")
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = handleAdResult });
            }
            else
            {
                toastManager.showToastOnUiThread("Check Your Internet Connection");
            }
        }
    }


    public static void showNoRewardedAd()
    {
        if (PlatformManager.platform == "ANDROID")
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }
            else
            {
                toastManager.showToastOnUiThread("Check Your Internet Connection");
            }
        }
    }

    public void noRewardAds()//Use this for onclick to buttons
    {
        if (PlatformManager.platform == "ANDROID")
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }
            else
            {
                toastManager.showToastOnUiThread("Check Your Internet Connection");
            }
        }
    }

    static void handleAdResult(ShowResult result)
    {
        if (PlatformManager.platform == "ANDROID")
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("Reward the user");

                    break;
                case ShowResult.Skipped:
                    Debug.Log("AD skipped");
                    break;
                case ShowResult.Failed:
                    Debug.Log("Ad failed");
                    break;
            }
        }
    }
}
