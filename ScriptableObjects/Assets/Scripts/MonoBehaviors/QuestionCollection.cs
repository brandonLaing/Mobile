using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionCollection : MonoBehaviour
{
  public QuizQuestion[] allQuestions;

  private void Start()
  {
    LoadQuestions();
  }

  void LoadQuestions()
  {
    allQuestions = Resources.LoadAll<QuizQuestion>("Questions");
  }

  public QuizQuestion GetUnaskedQuestion()
  {
    ResetQuestionsIfAllHaveBeenAsked();

    QuizQuestion question = allQuestions
      .Where(t => t.Asked == false)
      .OrderBy(t => Random.Range(0, int.MaxValue))
      .FirstOrDefault();

    if (question == null)
      ResetQuestions();

    question.Asked = true;
    return question;
  }

  void ResetQuestionsIfAllHaveBeenAsked()
  {
    if (allQuestions.Any(t => t.Asked == false) == false)
      ResetQuestions();
  }

  void ResetQuestions()
  {
    Debug.Log("Checking for asked question");

    foreach (QuizQuestion question in allQuestions)
      question.Asked = false;
  }
}
