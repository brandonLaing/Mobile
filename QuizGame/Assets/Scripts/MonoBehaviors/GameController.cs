using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Collects player answers and sends calls to deal with that information
/// By: Natalie Eidt
/// </summary>
public class GameController : MonoBehaviour
{
  public event Action OnNewGameStarted = delegate { };
  public event Action OnNewRoundStart = delegate { };
  public event Action<int, int, int> OnBothAnswersReceived = delegate { };
  public event Action OnPopupSwapPlayer = delegate { };
  public event Action<int> OnShowCorrectAnswer = delegate { };

  /// <summary>
  /// Player 1s anwswer
  /// </summary>
  private int player1Answer = -1;

  /// <summary>
  /// Correct answer to current question
  /// </summary>
  private int correctAnswer = -1;

  /// <summary>
  /// Subscribes delegates
  /// </summary>
  private void Awake()
  {
    FindObjectOfType<MainMenuController>().OnNewGameStarted += StartNewGame;
    FindObjectOfType<QuestionController>().OnCorrectAnswerGrabbed += SaveCorrectAnswer;
    FindObjectOfType<TimerController>().OnTimerFinished += NewAnswerReceived;
  }

  /// <summary>
  /// Unsubscribes delegates
  /// </summary>
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
    // if there is no previous answer save the answer and popup the swap player
    if (player1Answer == -1)
    {
      player1Answer = playerAnswer;
      OnPopupSwapPlayer();
    }
    else
    {
      // Send call to process both answers recived on show correct answer
      OnBothAnswersReceived(player1Answer, playerAnswer, correctAnswer);
      OnShowCorrectAnswer(correctAnswer);
      // reset player 1 answer
      player1Answer = -1;
      // Wait then call start new round
      Invoke("StartNewRound", 1);
    }
  }

  /// <summary>
  /// If new answer is recived with no answer attached it sets answer to 4 which will never be the answer
  /// </summary>
  private void NewAnswerReceived()
  {
    NewAnswerReceived(4);
  }
}