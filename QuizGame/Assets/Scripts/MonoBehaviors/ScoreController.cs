using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Processess scores
/// By: Brandon Laing
/// </summary>
public class ScoreController : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// Scores for player
  /// </summary>
  [SerializeField]
  private int player1Score, player2Score;
  /// <summary>
  /// Health for each player
  /// </summary>
  [SerializeField]
  private int player1Health = 3, player2Health = 3;

  [Tooltip("How much each answer is worth")]
  public int answerValue;

  /// <summary>
  /// Once all scores have been checked and no health below 0. p1score, p1health, p2score, p2health
  /// </summary>
  public event Action<int, int, int, int> OnScoresProcessed = delegate { };
  public event Action<int, int> OnEndGame = delegate { };
  #endregion

  #region Unity Events
  private void Awake()
  {
    FindObjectOfType<GameController>().OnBothAnswersReceived += ProcessOutcome;
    FindObjectOfType<GameController>().OnNewGameStarted += ResetScore;
  }

  private void OnDestroy()
  {
    FindObjectOfType<GameController>().OnBothAnswersReceived -= ProcessOutcome;
    FindObjectOfType<GameController>().OnNewGameStarted -= ResetScore;
  }
  #endregion

  #region Functions
  /// <summary>
  /// Resets scores and health of each player
  /// </summary>
  public void ResetScore()
  {
    player1Score = 0;
    player2Score = 0;
    player1Health = 3;
    player2Health = 3;
  }

  /// <summary>
  /// Calculates what to do once both answers are entered
  /// </summary>
  /// <param name="player1Answer">First players answer</param>
  /// <param name="player2Answer">second players answer</param>
  /// <param name="correctAnswers">Correct answer for current question</param>
  public void ProcessOutcome (int player1Answer, int player2Answer, int correctAnswers)
  {
    // If player 1 is wrong take health way or if not add score
    if (player1Answer != correctAnswers)
      player1Health--;
    else
      player1Score += answerValue;

    // If player 2 is wrong take health way or if not add score
    if (player2Answer != correctAnswers)
      player2Health--;
    else
      player2Score += answerValue;

    // if either player is at 0 health send the game into end game
    if (player1Health <= 0 || player2Health <= 0)
    {
      Debug.Log("Ending game");

      OnEndGame(player1Score, player2Score);
      return;
    }

    OnScoresProcessed(player1Score, player2Score, player1Health, player2Health);
  }
  #endregion
}
