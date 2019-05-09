using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameUpgrade : ScriptableObject
{
  [Header("Basic Info")]
  public string upgradeName = "DefaultUpgrade";


  // Check what cost will look like over time https://www.desmos.com/calculator
  [Header("Cost")]
  public int startingCost = 1;
  public int increaseStep = 1;
  public int CurrentCost
  {
    get
    {
      return Mathf.RoundToInt(increaseStep + Mathf.Log(currentUpgradeCount + 1) + startingCost);
    }
  }

  [Header("UpgradeCount")]
  public int currentUpgradeCount;
  public int maxUpgrades;

  [Header("Effect")]
  public UpgradeEffect effect = UpgradeEffect.LinesPerSecond;
}
