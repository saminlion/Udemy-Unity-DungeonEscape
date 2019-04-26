using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Debug.Log("Show Rewarded Ad");

        //check if the ads is ready (rewardedVideo)
        //Show(rewardedVideo)

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };

            Advertisement.Show("rewardedVideo", options);
        }
    }

    void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                //Award 100 gems to player
                Debug.Log("Award gems");
                GameManager.Instance.player.AddGem(100);
                UIManager.Instance.OpenShop(GameManager.Instance.player._amountOfDiamonds);
                break;

            case ShowResult.Skipped:

                break;

            case ShowResult.Failed:

                break;
        }
    }



}
