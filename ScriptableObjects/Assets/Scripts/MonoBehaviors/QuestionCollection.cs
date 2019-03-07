using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System;
using Random = UnityEngine.Random;

public class QuestionCollection : MonoBehaviour
{
  public QuizQuestion[] allQuestions;

  public bool useXML;

  private string streamLoc
  {
    get { return Application.dataPath + "/Questions.xml"; }

  }
  private void Awake()
  {
    if (useXML)
    {
      if (!File.Exists(streamLoc))
      {
        WriteSameQuestionToXml();
      }
    }

    LoadQuestions();
  }

  private void WriteSameQuestionToXml()
  {
    allQuestions = new QuizQuestion[]
    {
            new QuizQuestion
            {
                Question = "If it's noon in Boston, what time is it in new York?",
                    Anwsers = new string[] {"1PM", "2PM", "Noon", "11AM"}, CorrectAnwser = 2
            },
            new QuizQuestion
            {
                Question = "What type of amimal was babe",
                    Anwsers = new string[] {"Donkey", "Spider", "Pig", "Lamb"}, CorrectAnwser = 3
            }
    };

    XmlSerializer serializer = new XmlSerializer(typeof(QuizQuestion[]));


    using (StreamWriter streamWriter = new StreamWriter(streamLoc))
    {
      serializer.Serialize(streamWriter, allQuestions);
    }

    //XmlSerializer serializer = new XmlSerializer(typeof(QuizQuestion[]));

    //using (StreamReader streamreader = new StreamReader("Questions.xml"))
    //{

    //}
  }

  private void Start()
  {
    LoadQuestions();
  }

  void LoadQuestions()
  {
    if (useXML)
    {
      XmlSerializer serilaizer = new XmlSerializer(typeof(QuizQuestion[]));

      using (StreamReader streamReader = new StreamReader(streamLoc))
      {
        allQuestions = (QuizQuestion[])serilaizer.Deserialize(streamReader);
      }
    }
    else
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
