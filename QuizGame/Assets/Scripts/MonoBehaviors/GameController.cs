using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collects player answers and sends calls to deal with that information
/// By: Natalie Eidt
/// </summary>
public class GameController : MonoBehaviour
{
  public event System.Action OnNewGameStarted = delegate { };
  public event System.Action OnNewRoundStart = delegate { };
  public event System.Action<int, int, int> OnBothAnswersReceived = delegate { };
  public event System.Action OnPopupSwapPlayer = delegate { };
  public event System.Action<int> OnShowCorrectAnswer = delegate { };


  private int player1Answer = -1;
  private int correctAnswer = -1;

  private void Awake()
  {
    FindObjectOfType<MainMenuController>().OnNewGameStarted += StartNewGame;
    FindObjectOfType<QuestionController>().OnCorrectAnswerGrabbed += SaveCorrectAnswer;
    FindObjectOfType<TimerController>().OnTimerFinished += NewAnswerReceived;
  }

  private void OnDestroy()
  {
    FindObjectOfType<MainMenuController>().OnNewGameStarted -= StartNewGame;
    FindObjectOfType<QuestionController>().OnCorrectAnswerGrabbed -= SaveCorrectAnswer;
    FindObjectOfType<TimerController>().OnTimerFinished -= NewAnswerReceived;
  }

  /// <summary>
  /// Sends message to start a new game.
  /// </summary>
  public void StartNewGame()
  {
    player1Answer = -1;
    correctAnswer = -1;
    OnNewGameStarted();
    OnNewRoundStart();
  }

  /// <summary>
  /// Send message to get new question and display it.
  /// </summary>
  public void StartNewRound()
  {
    OnNewRoundStart();
  }

  /// <summary>
  /// Stores a correct answer.
  /// </summary>
  /// <param name="correctAnswer">Correct answer number</param>
  public void SaveCorrectAnswer(int correctAnswer)
  {
    this.correctAnswer = correctAnswer;
  }

  /// <summary>
  /// Processes a new player answer recived
  /// </summary>
  /// <param name="playerAnswer">Incoming player answer</param>
  public void NewAnswerReceived(int playerAnswer)
  {
    if (player1Answer == -1)
    {
      player1Answer = playerAnswer;
      OnPopupSwapPlayer();
    }
    else
    {
      OnBothAnswersReceived(player1Answer, playerAnswer, correctAnswer);
      OnShowCorrectAnswer(correctAnswer);
      player1Answer = -1;
      Invoke("StartNewRound", 1);
    }
  }

  private void NewAnswerReceived()
  {
    NewAnswerReceived(-1);
  }
}