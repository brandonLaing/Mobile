using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event System.Action OnNewGameStarted = delegate { };
    public event System.Action OnNewRoundStart = delegate { };
    public event System.Action<int, int> OnBothAnswersReceived = delegate { };
    public event System.Action OnPopupSwapPlayer = delegate { };

    private int Player1Answer;
    private int Player2Answer;
    private int CorrectAnswer;

    public void StartNewGame()
    {
        //OnNewGameStarted
        //OnNewRoundStarted
    }

    public void SaveCorrectAnswer(int correctAnswer)
    {
        CorrectAnswer = correctAnswer;
    }

    public void NewAnswerReceived(int playerAnswer)
    {
        //bothPalyersAnswersRecieved
        //OnPopupSwapPlayer
        
    }
}