using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyTracker : MonoBehaviour
{
  #region Variables
  public static MoneyTracker main;

  public decimal money;
  [SerializeField] private List<GameDataType> games;
  [SerializeField] private readonly string saveGamesKey = "saveGames";

  [SerializeField] private readonly string saveMoneyKey = "money";


  #endregion

  #region Mono Behavior Functions
  private void Awake()
  {
    if (main == null)
      main = this;
    else
      Destroy(this.gameObject);
  }

  private void Start()
  {
    LoadInfo((DateTime.Now - SaveLoadSystem.TimeSinceLastSave).Seconds);
  }

  private void OnDestroy()
  {
    SaveInfo();
  }

  private void Update()
  {
    foreach (GameDataType game in games)
    {
      money += (((decimal)game.Quality * (decimal)game.Popularity * 55 + 5) / 60) * (decimal)Time.deltaTime;
    }
  }

  #endregion
  #region Functions
  public void LoadInfo(int secondsMissed)
  {
    games = SaveLoadSystem.Load<List<GameDataType>>(saveGamesKey);
    money = SaveLoadSystem.Load<decimal>(saveMoneyKey);

    foreach (GameDataType game in games)
      money += (((decimal)game.Quality * (decimal)game.Popularity * 55 + 5) / 60) * (decimal)secondsMissed;
  }

  public void SaveInfo()
  {
    SaveLoadSystem.Save(games, saveGamesKey);
    SaveLoadSystem.Save(money, saveMoneyKey);
  }

  public void AddGame(GameDataType newGame)
  {
    games.Add(newGame);
  }
  #endregion
}

