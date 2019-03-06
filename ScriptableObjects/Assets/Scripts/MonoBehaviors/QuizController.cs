using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizController : MonoBehaviour
{
  QuestionCollection qC;
  QuizQuestion currentQuestion;
  UIController uiController;

  [SerializeField]
  float delayBetweenQuestions = 3F;

  private void Awake()
  {
    qC = FindObjectOfType<QuestionCollection>();
    uiController = FindObjectOfType<UIController>();
  }

  private void Start()
  {
    PresentQuestion();
  }

  void PresentQuestion()
  {
    currentQuestion = qC.GetUnaskedQuestion();
    uiController.SetupUIForQuestion(currentQuestion);
  }

  public void _SubmitAnswer(int answerNumber)
  {
    bool isCorrect = answerNumber == currentQuestion.CorrectAnwser;
    uiController.HandelSubmittedAnswer(isCorrect);

    Invoke("ShowNextQuestion", delayBetweenQuestions);
  }

  void ShowNextQuestion()
  {
    PresentQuestion();
  }
}
