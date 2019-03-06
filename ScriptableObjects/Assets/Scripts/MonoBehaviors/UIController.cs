using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
  [SerializeField]
  private Text questionText;
  [SerializeField]
  private Button[] answerBtns;
  [SerializeField]
  private GameObject correctAnwserPopup, wrongAnwserPopup;

  public void SetupUIForQuestion(QuizQuestion question)
  {
    correctAnwserPopup.SetActive(false);
    wrongAnwserPopup.SetActive(false);

    questionText.text = question.Question;

    for (int i = 0; i < answerBtns.Length; i++)
    {
      if (i < question.Anwsers.Length)
      {
        answerBtns[i].gameObject.SetActive(true);
        answerBtns[i].GetComponentInChildren<Text>().text = question.Anwsers[i];
      }
      else
      {
        answerBtns[i].gameObject.SetActive(false);
      }
    }
  }

  public void HandelSubmittedAnswer(bool isCorrect)
  {
    ToggleAnswerButtons(false);

    if (isCorrect) ShowCorrectAnswerPopUp();
    else ShowWrongAnswerPopup();
  }

  private void ToggleAnswerButtons(bool value)
  {
    foreach (Button btn in answerBtns)
      btn.gameObject.SetActive(value);
  }

  private void ShowCorrectAnswerPopUp()
  {
    correctAnwserPopup.gameObject.SetActive(true);
  }

  void ShowWrongAnswerPopup()
  {
    wrongAnwserPopup.SetActive(true);
  }
}
