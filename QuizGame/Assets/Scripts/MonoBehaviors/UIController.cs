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

  [Header("Game UI")]
  [SerializeField]
  [Tooltip("Main Game Canvas")]
  private GameObject gameCanvas;

  [SerializeField]
  [Tooltip("Text box that holds the question")]
  private Text questionText;

  [SerializeField]
  [Tooltip("Each answers text box")]
  private Text[] answersText;

  [SerializeField]
  [Tooltip("Popup that shows up when players are supposed to spawp the phone")]
  private GameObject swapPlayersPopup;

  [SerializeField]
  [Tooltip("Text boxes for player 1")]
  private Text player1Score;

  [SerializeField]
  [Tooltip("Text boxes for player 2")]
  private Text player2Score;

  [SerializeField]
  [Tooltip("In order from 1 to 3 health hearts")]
  private GameObject[] player1Health = new GameObject[3], player2Health = new GameObject[3];

  [Header("End Game UI")]
  [Tooltip("Post Game Canvas")]
  [SerializeField]
  private GameObject endGameCanvas;

  [SerializeField]
  [Tooltip("Post Game Player 1 Score Text Box")]
  private Text endGamePlayer1Score;
  [SerializeField]
  [Tooltip("Post Game Player 2 Score Text Box")]
  private Text endGamePlayer2Score;

  [Header("Main Menu Canvas")]
  [SerializeField]
  [Tooltip("Main Menu Canvas")]
  private GameObject mainMenuCanvas;

  [SerializeField]
  private Text clockText;

  private int correctAnswer;

  private void Awake()
  {
    GameController gc = FindObjectOfType<GameController>();
    gc.OnNewGameStarted += ShowGameCanvas;
    gc.OnPopupSwapPlayer += PopupSwapPlayer;
    gc.OnShowCorrectAnswer += HighlightCorrectAnswer;

    ScoreController sc = FindObjectOfType<ScoreController>();
    sc.OnScoresProcessed += UpdateScoreDisplay;
    sc.OnEndGame += DisplayEndGameUI;

    FindObjectOfType<QuestionController>().OnNewQuestionGrabbed += DisplayNewQuestion;

    FindObjectOfType<TimerController>().OnTimeChanged += UpdateClockUI;
  }

  private void OnDestroy()
  {
    FindObjectOfType<GameController>().OnNewGameStarted -= ShowGameCanvas;
    FindObjectOfType<GameController>().OnPopupSwapPlayer -= PopupSwapPlayer;
    FindObjectOfType<QuestionController>().OnNewQuestionGrabbed -= DisplayNewQuestion;
    FindObjectOfType<ScoreController>().OnScoresProcessed -= UpdateScoreDisplay;
    FindObjectOfType<ScoreController>().OnEndGame -= DisplayEndGameUI;
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
  /// <param name="player2Score"></param>
  /// <param name="player1Health"></param>
  /// <param name="player2Health"></param>
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
  /// Opens the end game UI
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
  }

  /// <summary>
  /// Highlights the correct answer once both are answered
  /// </summary>
  /// <param name="correctAnswer">Correct answer number</param>
  public void HighlightCorrectAnswer(int correctAnswer)
  {
    this.correctAnswer = correctAnswer;
    answersText[this.correctAnswer].color = Color.green;
    Invoke("ResetColor", 1);
  }

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
  /// Hides all UI Canvases
  /// </summary>
  private void HideUIs()
  {
    endGameCanvas.SetActive(false);
    mainMenuCanvas.SetActive(false);
    gameCanvas.SetActive(false);
  }
}
