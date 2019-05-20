using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
  #region Variables
  public GameTracker gameTracker;
  public TextMeshProUGUI linesText;

  public MoneyTracker moneyTracker;
  public TextMeshProUGUI moneyText;
  #endregion

  #region Mono Behavior Functions
  private void Update()
  {
    linesText.text = "Lines Coded: " + Mathf.RoundToInt((float)gameTracker.codePointsEntered);
    moneyText.text = "$" + Math.Round((float)moneyTracker.money, 2).ToString("00.00");
  }
  #endregion

  #region Functions

  #endregion
}