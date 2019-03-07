using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Text;

public class ManualParse : MonoBehaviour
{
  public TextAsset xmlFile;

  private void Start()
  {
    // actual XML file
    XmlDocument xmlDoc = new XmlDocument();

    xmlDoc.LoadXml(xmlFile.text);

    /**
    // get Root node
    //XmlNode rootNode = xmlDoc.SelectSingleNode("ArrayOfQuizQuestion");

    // get all quiz questions
    //XmlNodeList quizQuestions = rootNode.SelectNodes("QuizQuestion");

    // select the first question
    //Debug.Log(quizQuestions[0].SelectSingleNode("Anwsers").InnerText);

    //XmlNodeList list = quizQuestions[0].SelectSingleNode("Anwsers").SelectNodes("string");

    //for (int i = 0; i < list.Count; i++)
    //{
    //  Debug.Log(list[i].InnerText);
    //}

    //foreach (XmlNode node in quizQuestions[0].SelectSingleNode("Anwsers").SelectNodes("string"))
    //  Debug.Log(node.InnerText);
    */

    /**
    XmlNode colorsNode = xmlDoc.SelectSingleNode("SampleXML").SelectSingleNode("Colors");

    for (int i = 1; i <= 4; i++)
    {
      Debug.Log(colorsNode.SelectSingleNode("Color" + i).InnerText);
    }
    */

    XmlNode rootNode = xmlDoc.SelectSingleNode("SampleXML");

    XmlNodeList colorsLists = rootNode.SelectNodes("Colors");
    int colorsThatHaveGreen = 0, numberOfWhites = 0;

    StringBuilder sb = new StringBuilder();

    foreach (XmlNode node in colorsLists)
    {
      bool checkedGreen = false;

      sb.AppendLine(node.Name);
      for (int i = 1; i <= 4; i++)
      {
        string colorName = node.SelectSingleNode("Color" + i).InnerText;
        sb.AppendLine("\t " + colorName);

        if (colorName == "White")
          numberOfWhites++;

        if (!checkedGreen && colorName == "Green")
        {
          colorsThatHaveGreen++;
          checkedGreen = true;
        }
      }
    }

    Debug.Log(sb);
    Debug.Log($"Number of green in colors: {colorsThatHaveGreen}\n" +
      $"Number of whites: {numberOfWhites}");
  }
}
