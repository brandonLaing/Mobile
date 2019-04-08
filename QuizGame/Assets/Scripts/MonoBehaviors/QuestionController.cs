using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Xml;
using System.Linq;

public enum QuestionCategories
{
  ANIME, BOOKS, MOVIES, TV
}

[ExecuteInEditMode]
public class QuestionController : MonoBehaviour
{
  private Dictionary<string, bool> questionIds = new Dictionary<string, bool>();
  public event Action<string, string[]> OnNewQuestionGrabbed = delegate { };
  public QuestionCategories category;
  public TextAsset xmlFile;
  private XmlDocument xmlDoc = new XmlDocument();

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

  private string GetId()
  {
    Debug.Log("Grabbing new Id");
    Debug.LogWarning(questionIds.Count);
    Random rnd = new Random();

    string id = "0000";
    Debug.LogWarning("Ask Tiff about this");
    //try
    //{
    //   id = questionIds
    //    .Where(i => i.Value == false)
    //    .OrderBy(r => Random.value)
    //    .First().Key;
    //}
    //catch
    //{
    //  ResetQuestions();
    //  return GetId();
    //}

    Debug.Log("Grabbed id: " + id);
    return id;
  }

  public void ResetQuestions()
  {
    Debug.Log("Reseting Questions");

    foreach (string key in questionIds.Keys)
      questionIds[key] = true;
  }
}
