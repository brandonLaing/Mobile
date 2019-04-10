using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls UI and updates in in different ways
/// By: Brandon Laing
/// </summary>
public class UIController : MonoBehaviour
{
  #region Variables
  [Header("Game UI")]

  [Tooltip("Main Game Canvas")]
  public GameObject gameCanvas;
  [Tooltip("Text box that holds the question")]
  public Text questionText;
  [Tooltip("Each answers text box")]
  public Text[] answersText;

  [Tooltip("Popup that shows up when players are supposed to spawp the phone")]
  public GameObject swapPlayersPopup;
  [Tooltip("Text boxes for player 1")]
  public Text player1Score;
  [Tooltip("Text boxes for player 2")]
  public Text player2Score;
  [Tooltip("In order from 1 to 3 health hearts")]
  public GameObject[] player1Health = new GameObject[3], player2Health = new GameObject[3];

  [Header("End Game UI")]
  [Tooltip("Post Game Canvas")]
  public GameObject endGameCanvas;
  [Tooltip("Post Game Player 1 Score Text Box")]
  public Text endGamePlayer1Score;
  [Tooltip("Post Game Player 2 Score Text Box")]
  public Text endGamePlayer2Score;

  [Header("Main Menu Canvas")]
  [Tooltip("Main Menu Canvas")]
  public GameObject mainMenuCanvas;
  [Tooltip("Test that the countdown will show on")]
  public Text clockText;

  /// <summary>
  /// Stored correct answer for unhighlighting
  /// </summary>
  private int correctAnswer;
  #endregion

  #region Unity Events
  private void Awake()
  {
    GameController gc = FindObjectOfType<GameController>();
    gc.OnNewGameStarted += ShowGameCanvas;
    gc.OnPopupSwapPlayer += PopupSwapPlayer;
    gc.OnShowCorrectAnswer += HighlightCorrectAnswer;
    gc.OnNewGameStarted += ResetUIs;

    ScoreController sc = FindObjectOfType<ScoreController>();
    sc.OnScoresProcessed += UpdateScoreDisplay;
    sc.OnEndGame += DisplayEndGameUI;

    FindObjectOfType<QuestionController>().OnNewQuestionGrabbed += DisplayNewQuestion;

    FindObjectOfType<TimerController>().OnTimeChanged += UpdateClockUI;

    FindObjectOfType<SideBarController>().OnBackToMenu += ShowMainMenuCanvas;
  }

  private void OnDestroy()
  {
    GameController gc = FindObjectOfType<GameController>();
    gc.OnNewGameStarted -= ShowGameCanvas;
    gc.OnPopupSwapPlayer -= PopupSwapPlayer;
    gc.OnShowCorrectAnswer -= HighlightCorrectAnswer;
    gc.OnNewGameStarted -= ResetUIs;

    ScoreController sc = FindObjectOfType<ScoreController>();
    sc.OnScoresProcessed -= UpdateScoreDisplay;
    sc.OnEndGame -= DisplayEndGameUI;

    FindObjectOfType<QuestionController>().OnNewQuestionGrabbed -= DisplayNewQuestion;

    FindObjectOfType<TimerController>().OnTimeChanged -= UpdateClockUI;

    FindObjectOfType<SideBarController>().OnBackToMenu -= ShowMainMenuCanvas;
  }

  #endregion

  #region Functions
  /// <summary>
  /// Resets all ui elements for the start of a game
  /// </summary>
  private void ResetUIs()
  {
    player1Score.text = 0.ToString();
    player2Score.text = 0.ToString();
    questionText.text = "";
    for (int i = 0; i < answersText.Length; i++)
      answersText[i].text = "";

    for (int i = 0; i < player1Health.Length; i++)
    {
      player1Health[i].SetActive(true);
      player2Health[i].SetActive(true);
    }
  }


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
  /// <param name="player2Score">Player 2s score</param>
  /// <param name="player1Health">player 1s health</param>
  /// <param name="player2Health">player 2s health</param>
  public void UpdateScoreDisplay(int player1Score, int player2Score, int player1Health, int player2Health)
  {
    this.player1Score.text = player1Score.ToString();
    this.player2Score.text = player2Score.ToString();

    if (player1Health < 3)
      this.player1Health[player1Health].SetActive(false);

    if (player2Health < 3)
      this.player2Health[player2Health].SetActive(false);
  }

  /// <summary>
  /// Opens the end game UI then sends game back to the main menu
  /// </summary>
  /// <param name="player1Score"></param>
  /// <param name="player2Score"></param>
  public void DisplayEndGameUI(int player1Score, int player2Score)
  {
    HideUIs();
    endGameCanvas.SetActive(true);
    if (player1Score > player2Score)
    {
      this.endGamePlayer1Score.text = player1Score.ToString();
      this.endGamePlayer2Score.text = player2Score.ToString();
    }
    else
    {
      this.endGamePlayer1Score.text = player2Score.ToString();
      this.endGamePlayer2Score.text = player1Score.ToString();
    }

    Invoke("ShowMainMenuCanvas", 5);
  }

  /// <summary>
  /// Highlights the correct answer once both are answered and then waits a second and resets the color
  /// </summary>
  /// <param name="correctAnswer">Correct answer number</param>
  public void HighlightCorrectAnswer(int correctAnswer)
  {
    this.correctAnswer = correctAnswer;
    answersText[this.correctAnswer].color = Color.green;
    Invoke("ResetColor", 1);
  }

  /// <summary>
  /// Resets the color of the text back to its original color
  /// </summary>
  private void ResetColor()
  {
    answersText[correctAnswer].color = Color.black;
  }

  /// <summary>
  /// Updates the display of the display clock
  /// </summary>
  /// <param name="time">Current time out of 60 seconds</param>
  public void UpdateClockUI(float time)
  {
    clockText.text = (Mathf.RoundToInt(time)).ToString();
  }

  /// <summary>
  /// Opens the game canvas
  /// </summary>
  public void ShowGameCanvas()
  {
    HideUIs();
    gameCanvas.SetActive(true);
  }

  /// <summary>
  /// Sets the main menu to the only display
  /// </summary>
  public void ShowMainMenuCanvas()
  {
    HideUIs();
    mainMenuCanvas.SetActive(true);
  }

  /// <summary>
  /// Hides all UI Canvases
  /// </summary>
  private void HideUIs()
  {
    endGameCanvas.SetActive(false);
    mainMenuCanvas.SetActive(false);
    gameCanvas.SetActive(false);
  }
  #endregion
}
