﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Xml;
using System.Linq;

/// <summary>
/// Gets and stores what questions have and have not been asked
/// By: Brandon Laing
/// </summary>
public class QuestionController : MonoBehaviour
{
  #region Variables
  [Tooltip("Text File to Read out of")]
  [SerializeField]
  public TextAsset xmlFile;

  /// <summary>
  /// Dictionary of ids and whether or not those ids have been read
  /// </summary>
  private Dictionary<string, bool> questionIds = new Dictionary<string, bool>();
  /// <summary>
  /// Currently Selected category
  /// </summary>
  private QuestionCategories category;
  /// <summary>
  /// Document being read out of
  /// </summary>
  private XmlDocument xmlDoc = new XmlDocument();

  public event Action<string, string[]> OnNewQuestionGrabbed = delegate { };
  public event Action<int> OnCorrectAnswerGrabbed = delegate { };
  #endregion

  #region UnityEvent
  private void Awake()
  {
    FindObjectOfType<MainMenuController>().OnNewCategorySet += SetCategory;
    GameController gC = FindObjectOfType<GameController>();
    gC.OnNewGameStarted += GrabIds;
    gC.OnNewRoundStart += GetQuestion;
  }

  private void OnDestroy()
  {
    FindObjectOfType<MainMenuController>().OnNewCategorySet -= SetCategory;
    GameController gC = FindObjectOfType<GameController>();
    gC.OnNewGameStarted -= GrabIds;
    gC.OnNewRoundStart -= GetQuestion;
  }
  #endregion

  #region Functions
  /// <summary>
  /// Sets a question category from main menu
  /// </summary>
  /// <param name="category"></param>
  public void SetCategory(QuestionCategories category)
  {
    this.category = category;
  }

  /// <summary>
  /// Grabs ids for questions based on what category is selected
  /// </summary>
  public void GrabIds()
  {
    questionIds.Clear();
    xmlDoc.LoadXml(xmlFile.text);
    XmlNodeList questionsNode = GetQuestionList(xmlDoc);

    foreach (XmlNode node in questionsNode)
      if (node.SelectSingleNode("category").InnerText == category.ToString())
        questionIds.Add(node.SelectSingleNode("id").InnerText, false);
  }

  /// <summary>
  /// Gets list of questions from xml Doc
  /// </summary>
  /// <param name="xmlDoc">Document to read from</param>
  /// <returns>List of xml questions</returns>
  private static XmlNodeList GetQuestionList(XmlDocument xmlDoc)
  {
    return xmlDoc.SelectSingleNode("questioncollection").SelectNodes("questiondata");
  }

  /// <summary>
  /// Grabs a question from the current dictionary of question ids and calles OnNewQuestionGrabbed
  /// </summary>
  public void GetQuestion()
  {
    string id = GetId();
    questionIds[id] = true;


    XmlNodeList nodeList = GetQuestionList(xmlDoc);

    foreach (XmlNode node in nodeList)
      if (node.SelectSingleNode("id").InnerText == id)
      {
        string question = node.SelectSingleNode("question").InnerText;
        string[] answers = new string[4];
        int correctAnswer = int.Parse(node.SelectSingleNode("correctanswer").InnerText);
        XmlNodeList answersNodeList = node.SelectNodes("answer");
        for (int i = 0; i < answers.Length; i++)
        {
          answers[i] = answersNodeList[i].InnerText;
        }

        Debug.Log($"Got Question:" +
          $"\n{question}" +
          $"\n\t{answers[0]}" +
          $"\n\t{answers[1]}" +
          $"\n\t{answers[2]}" +
          $"\n\t{answers[3]}" +
          $"\n{correctAnswer.ToString()}");

        OnCorrectAnswerGrabbed(correctAnswer);
        OnNewQuestionGrabbed(question, answers);
      }
  }

  /// <summary>
  /// Gets a random questin id of those availible to be asked
  /// </summary>
  /// <returns>Random question id</returns>
  private string GetId()
  {
    Random rnd = new Random();

    string id = "0000";
    Debug.LogWarning("Ask Tiff about this");
    try
    {
      id = questionIds
       .Where(i => i.Value == false)
       .OrderBy(r => Random.value)
       .First().Key;
    }
    catch
    {
      ResetQuestions();
      return GetId();
    }

    return id;
  }

  /// <summary>
  /// Goes though and resets all questions in dictionary
  /// </summary>
  public void ResetQuestions()
  {
    Debug.Log("Reseting Questions");
    string[] copy = questionIds.Keys.ToArray();
    foreach (string key in copy)
      questionIds[key] = false;
  }
  #endregion
}
