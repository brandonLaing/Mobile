using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls UI and event for the main menu canvas
/// By: Natilie
/// </summary>
public class MainMenuController : MonoBehaviour
{
  public event System.Action<QuestionCategories> OnNewCategorySet = delegate { };
  public event System.Action OnNewGameStarted = delegate { };

  public void StartGame()
  {
    OnNewGameStarted();
  }

  /// <summary>
  /// Sets call for new category
  /// </summary>
  /// <param name="categoryValue">Value of category enum</param>
  public void SetCategory(int categoryValue)
  {
    OnNewCategorySet((QuestionCategories)categoryValue);
  }
}
