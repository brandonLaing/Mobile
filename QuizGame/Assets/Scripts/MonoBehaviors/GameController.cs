using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collects player answers and sends calls to deal with that information
/// By: Natali
/// </summary>
public class GameController : MonoBehaviour
{
  public event System.Action OnNewGameStarted = delegate { };
  public event System.Action OnNewRoundStart = delegate { };
  public event System.Action<int, int, int> OnBothAnswersReceived = delegate { };
  public event System.Action OnPopupSwapPlayer = delegate { };

  private int player1Answer = -1;
  private int correctAnswer = -1;

  private void Awake()
  {
    
  }

  private void OnDestroy()
  {
    
  }

  /// <summary>
  /// Sends message to start a new game.
  /// By: Brandon Laing
  /// </summary>
  public void StartNewGame()
  {
    OnNewGameStarted();
    OnNewRoundStart();
  }

  /// <summary>
  /// Send message to get new question and display it.
  /// By: Brandon Laing
  /// </summary>
  public void StartNewRound()
  {
    OnNewRoundStart();
  }

  /// <summary>
  /// Stores a correct answer.
  /// By: Brandon Laing
  /// </summary>
  /// <param name="correctAnswer">Correct answer number</param>
  public void SaveCorrectAnswer(int correctAnswer)
  {
    this.correctAnswer = correctAnswer;
  }

  /// <summary>
  /// Processes a new player answer recived
  /// By: Brandon Laing
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
      player1Answer = -1;
      Invoke("StartNewRound", 1);
    }
  }
}