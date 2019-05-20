using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
  #region Variables
  private string codePointsKey = "codePoints";
  public double codePointsEntered;

  private string designPointsKey = "designPoints";
  public double designPointsEntered;

  private string artPointsKey = "artPoints";
  public double artPointsEntered;

  private string currentGameKey = "currentGame";
  private GameDataType currentGame = null;
  #endregion

  #region Mono Behavior Functions
  private void Start()
  {
    LoadInfo();

    if (currentGame == null)
      currentGame = new GameDataType(100, 1, 1, Random.Range(1.0F, 100.0F));
  }

  private void OnDestroy()
  {
    SaveInfo();
  }
  #endregion

  #region Functions
  public void LoadInfo()
  {
    codePointsEntered = SaveLoadSystem.Load<int>(codePointsKey);
    designPointsEntered = SaveLoadSystem.Load<int>(designPointsKey);
    artPointsEntered = SaveLoadSystem.Load<int>(artPointsKey);

    currentGame = SaveLoadSystem.Load<GameDataType>(currentGameKey);
  }

  public void SaveInfo()
  {
    SaveLoadSystem.Save(codePointsEntered, codePointsKey);
    SaveLoadSystem.Save(designPointsEntered, designPointsKey);
    SaveLoadSystem.Save(artPointsEntered, artPointsKey);

    SaveLoadSystem.Save<GameDataType>(currentGame, currentGameKey);
  }

  public void _StartNewGame()
  {
    currentGame = new GameDataType(100, 1, 1, Random.Range(1.0F, 100.0F));
  }

  public void _ReleaseGame()
  {
    currentGame.ReleaseGame((int)codePointsEntered, (int)designPointsEntered, (int)artPointsEntered);
    MoneyTracker.main.AddGame(currentGame);
    currentGame = new GameDataType(1000, 1, 1, Random.Range(1.0F, 100.0F));
    codePointsEntered = 0;
    designPointsEntered = 0;
    artPointsEntered = 0;
  }

  public void _AddCodePoints(double points)
  {
    codePointsEntered += points;
  }

  public void _AddCodePoints(int points)
  {
    codePointsEntered += points;
  }
  public void _AddDesignPoints(double points)
  {
    designPointsEntered += points;
  }

  public void _AddArtPoints(double points)
  {
    artPointsEntered += points;
  }
  #endregion
}