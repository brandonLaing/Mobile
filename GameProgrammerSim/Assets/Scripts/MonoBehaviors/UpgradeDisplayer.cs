using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpgradeDisplayer : MonoBehaviour
{
  #region Variables
  public GameUpgrade upgrade;
  public UpgradesTracker tracker;

  public TextMeshProUGUI upgradeName;
  public TextMeshProUGUI description;
  public TextMeshProUGUI timesUpgraded;
  public TextMeshProUGUI maxUpgradable;
  public TextMeshProUGUI cost;
  #endregion

  #region Mono Behavior Functions
  private void Update()
  {
    upgradeName.text = upgrade.name;
    description.text = upgrade.effect.ToString() + upgrade.increaseStep;
    timesUpgraded.text = upgrade.currentUpgradeCount.ToString();
    maxUpgradable.text = upgrade.maxUpgrades.ToString();
    cost.text = Math.Round((float)upgrade.CurrentCost,2).ToString();
  }
  #endregion

  #region Functions
  public void _DoUpgrade()
  {
    if (upgrade.currentUpgradeCount < upgrade.maxUpgrades)
    {
      upgrade.currentUpgradeCount++;
      tracker.UpgradeAdded();
    }
  }
  #endregion
}