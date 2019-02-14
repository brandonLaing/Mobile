using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsTesting : MonoBehaviour
{

  public static DateTime? nextAdTime = null;

  /// <summary>
  /// Show reward ad
  /// </summary>
  public static void ShowAd()
  {
    nextAdTime = DateTime.Now.AddSeconds(5);

    if (Advertisement.IsReady())
    {
      ShowOptions options = new ShowOptions() { resultCallback = HandleShowResult };
      Advertisement.Show(options);
    }
  }

  /// <summary>
  /// called once ad is closed
  /// </summary>
  /// <param name="obj">Result given back from the ad</param>
  private static void HandleShowResult(ShowResult obj)
  {
    switch (obj)
    {
      case ShowResult.Failed:
        Debug.Log("They failed the ad");
        break;

      case ShowResult.Finished:
        Debug.Log("They finished the ad, we got de money");
        break;

      case ShowResult.Skipped:
        Debug.Log("The ad was skipped");
        break;
    }
  }

  //public static void ShowAd()
  //{
  //  ShowOptions option = new ShowOptions
  //  {
  //    resultCallback = Unpause
  //  };

  //  if (Advertisement.IsReady())
  //  {
  //    Advertisement.Show(option);

  //    // thing to pause the game
  //  }
  //}

  static void Unpause(ShowResult result)
  {
    // thing to unpause
  }
}
