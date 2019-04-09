using System.Collections;
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
[ExecuteInEditMode]
public class QuestionController : MonoBehaviour
{
  #region Variables
  [Tooltip("Text File to Read out of")]
  [SerializeField]
  private TextAsset xmlFile;

  /// <summary>
  /// Dictionary of ids and whether or not those ids have been read
  /// </summary>
  private Dictionary<string, bool> questionIds = new Dictionary<string, bool>();
  /// <summary>
  /// Currently Selected category
  /// </summary>
  [SerializeField]
  private QuestionCategories category;
  /// <summary>
  /// Document being read out of
  /// </summary>
  private XmlDocument xmlDoc = new XmlDocument();

  /// <summary>
  /// Sends the question and each answer
  /// </summary>
  public event Action<string, string[]> OnNewQuestionGrabbed = delegate { };
  #endregion

  #region UnityEvent
  private void Awake()
  {
    FindObjectOfType<MainMenuController>().OnNewCategorySet += SetCategory;
  }

  private void OnDestroy()
  {
    FindObjectOfType<MainMenuController>().OnNewCategorySet -= SetCategory;
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
    Debug.Log("Grabbing Ids");
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
    Debug.Log("Getting Question");
    string id = GetId();
    questionIds[id] = true;


    XmlNodeList nodeList = GetQuestionList(xmlDoc);

    foreach (XmlNode node in nodeList)
      if (node.SelectSingleNode("id").InnerText == id)
      {
        string question = node.SelectSingleNode("question").InnerText;
        string[] answers = new string[4];
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
          $"\n\t{answers[3]}");

        OnNewQuestionGrabbed(question, answers);
      }
  }

  /// <summary>
  /// Gets a random questin id of those availible to be asked
  /// </summary>
  /// <returns>Random question id</returns>
  private string GetId()
  {
    Debug.Log("Grabbing new Id");
    Debug.LogWarning(questionIds.Count);
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

    Debug.Log("Grabbed id: " + id);
    return id;
  }

  /// <summary>
  /// Goes though and resets all questions in dictionary
  /// </summary>
  public void ResetQuestions()
  {
    Debug.Log("Reseting Questions");

    foreach (string key in questionIds.Keys)
      questionIds[key] = false;
  }
  #endregion
}
