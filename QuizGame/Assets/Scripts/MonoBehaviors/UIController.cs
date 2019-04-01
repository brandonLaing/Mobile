using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  [Header("Game UI")]
  [SerializeField]
  private GameObject gameCanvas;

  [SerializeField]
  private Text questionText;
  [SerializeField]
  private Text[] answersText;
  [SerializeField]
  private GameObject swapPlayersPopup;


  [Header("End Game UI")]
  [SerializeField]
  private GameObject endGameCanvas;

  [SerializeField]
  private Text endGamePlayer1Score;
  [SerializeField]
  private Text endGamePlayer2Score;

  [Header("Main Menu Canvas")]
  [SerializeField]
  private GameObject mainMenuCanvas;

  /// <summary>
  /// Show a question to the screen
  /// </summary>
  /// <param name="question">Question that is to be displayed</param>
  /// <param name="answers">Set of answers</param>
  public void DisplayNewQuestion(string question, string[] answers)
  {
    questionText.text = question;

    for (int i = 0; i < answersText.Length; i++)
    {
      answersText[i].text = answers[i];
    }
  }

  /// <summary>
  /// Shows the swap player UI
  /// </summary>
  public void PopupSwapPlayer()
  {
    swapPlayersPopup.SetActive(true);
  }

  /// <summary>
  /// Hides the swap player UI
  /// </summary>
  public void HideSwapPlayer()
  {
    swapPlayersPopup.SetActive(false);
  }

  /// <summary>
  /// Updates the UI to show the new score and Health
  /// </summary>
  /// <param name="player1Score">Player 1s score</param>
  /// <param name="player2Score"></param>
  /// <param name="player1Health"></param>
  /// <param name="player2Health"></param>
  public void UpdateScoreDisplay(int player1Score, int player2Score, 
    int player1Health, int player2Health)
  {

  }

  public void DisplayEndGameUI(int player1Score, int player2Score)
  {
    HideUIs();
    endGameCanvas.SetActive(true);

  }

  public void HighlightCorrectAnswer(int correctAnswer)
  {

  }

  public void UpdateClockUI(float time)
  {
    
  }

  public void ShowGameCanvas()
  {

  }

  private void HideUIs()
  {
    endGameCanvas.SetActive(false);
    mainMenuCanvas.SetActive(false);
    gameCanvas.SetActive(false);
  }
}
