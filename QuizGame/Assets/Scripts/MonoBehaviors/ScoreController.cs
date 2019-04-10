using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Processess scores
/// By: Brandon Laing
/// </summary>
public class ScoreController : MonoBehaviour
{
  [SerializeField]
  private int player1Score, player2Score;
  [SerializeField]
  private int player1Health = 3, player2Health = 3;

  [SerializeField]
  private int answerValue;

  /// <summary>
  /// Once all scores have been checked and no health below 0. p1score, p1health, p2score, p2health
  /// </summary>
  public event System.Action<int, int, int, int> OnScoresProcessed = delegate { };
  public event System.Action<int, int> OnEndGame = delegate { };

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

  public void ResetScore()
  {
    player1Score = 0;
    player2Score = 0;
    player1Health = 3;
    player2Health = 3;
  }

  public void ProcessOutcome (int player1Answer, int player2Answer, int correctAnswers)
  {
    if (player1Answer != correctAnswers)
      player1Health--;
    else
      player1Score += answerValue;


    if (player2Answer != correctAnswers)
      player2Health--;
    else
      player2Score += answerValue;


    if (player1Health <= 0 || player2Health <= 0)
    {
      Debug.Log("Ending game");

      OnEndGame(player1Score, player2Score);
      return;
    }

    OnScoresProcessed(player1Score, player2Score, player1Health, player2Health);
  }
}
