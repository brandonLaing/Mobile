using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UpgradesTracker : MonoBehaviour
{
  #region Variables
  public List<GameUpgrade> upgrades;
  public List<UpgradeDisplayer> displayer;

  [SerializeField] private int linesPerSecond;

  public GameObject upgradePrefab;

  public GameTracker tracker;
  #endregion

  #region Mono Behavior Functions
  private void Start()
  {
    LoadInfo((DateTime.Now - SaveLoadSystem.TimeSinceLastSave).TotalSeconds);

    foreach (GameUpgrade upgrade in upgrades)
    {
      GameObject displayer = Instantiate(upgradePrefab, this.transform);
      displayer.GetComponent<UpgradeDisplayer>().upgrade = upgrade;
    }

    UpgradeAdded();
  }

  private void OnDestroy()
  {
    SaveInfo();
  }

  private void Update()
  {
    Debug.Log((linesPerSecond / 60.0F) * Time.deltaTime);

    tracker._AddCodePoints((linesPerSecond / 60.0F) * Time.deltaTime);
  }
  #endregion

  #region Functions
  public void UpgradeAdded()
  {
    linesPerSecond = 0;
    foreach (GameUpgrade upgrade in upgrades)
    {
      switch (upgrade.effect)
      {
        case UpgradeEffect.LinesPerSecond:
          linesPerSecond += upgrade.effectValue;
          break;
      }
    }
  }

  public void SaveInfo()
  {
    List<int> counts = new List<int>();
    foreach (GameUpgrade upgrade in upgrades)
      counts.Add(upgrade.currentUpgradeCount);

    SaveLoadSystem.Save(counts, "upgrades");
  }

  public void LoadInfo(double secondsPast)
  {
    List<int> countList = SaveLoadSystem.Load<List<int>>("upgrades");
    for (int i = 0; i < upgrades.Count; i++)
      upgrades[i].currentUpgradeCount = countList[i];
  }
  #endregion
}