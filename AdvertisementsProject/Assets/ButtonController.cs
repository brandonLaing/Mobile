using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
  public void _ToScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);

    if (AdController.main.showAd)
    {
      AdsTesting.ShowAd();
    }
  }

  public void _OptInAd()
  {
    if (AdController.main.showAd)
    {
      if (AdsTesting.nextAdTime.HasValue & (AdsTesting.nextAdTime.Value > DateTime.Now))
      {
        TimeSpan remainingTime = AdsTesting.nextAdTime.Value - DateTime.Now;
        Debug.Log($"{remainingTime.Minutes}: {remainingTime.Seconds} Time remaining before next ad is availible");
      }
      else
        AdsTesting.ShowAd();
    }
  }
}
