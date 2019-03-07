using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Text;

public class ManualParse : MonoBehaviour
{
  public TextAsset xmlFile;
  public List<CDInfo> cds = new List<CDInfo>();

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

    /**
    //XmlNode rootNode = xmlDoc.SelectSingleNode("SampleXML");

    //XmlNodeList colorsLists = rootNode.SelectNodes("Colors");
    //int colorsThatHaveGreen = 0, numberOfWhites = 0;

    //StringBuilder sb = new StringBuilder();

    //foreach (XmlNode node in colorsLists)
    //{
    //  bool checkedGreen = false;

    //  sb.AppendLine(node.Name);
    //  for (int i = 1; i <= 4; i++)
    //  {
    //    string colorName = node.SelectSingleNode("Color" + i).InnerText;
    //    sb.AppendLine("\t " + colorName);

    //    if (colorName == "White")
    //      numberOfWhites++;

    //    if (!checkedGreen && colorName == "Green")
    //    {
    //      colorsThatHaveGreen++;
    //      checkedGreen = true;
    //    }
    //  }
    //}

    //Debug.Log(sb);
    //Debug.Log($"Number of green in colors: {colorsThatHaveGreen}\n" +
    //  $"Number of whites: {numberOfWhites}");
    */

    /**
     *
     */
    int UsCDS = 0, UkCDS = 0, EuCDS = 0;
    StringBuilder sb = new StringBuilder();
    XmlNode rootNode = xmlDoc.SelectSingleNode("CATALOG");

    #region Less than $9.00
    int lessThanNineDollars = 0;
    // get all the cds
    foreach (XmlNode cdNode in rootNode.ChildNodes)
    {
      // Get all the nodes in a cd
      foreach (XmlNode cdInfoNode in cdNode.SelectNodes("PRICE"))
      {
          float price; float.TryParse(cdInfoNode.InnerText, out price);

          if (price < 9.00F)
          {
            lessThanNineDollars++;
          }
      }
    }
    #endregion

    #region CDs before 1990
    List<string> cdsAfter1990 = new List<string>();

    foreach (XmlNode cdNode in rootNode.SelectNodes("CD"))
    {
      string innerText = cdNode.SelectSingleNode("YEAR").InnerText;
      int year; int.TryParse(innerText, out year);

      if (year > 1990)
      {
        string name = cdNode.SelectSingleNode("TITLE").InnerText;
        cdsAfter1990.Add(name);
      }
    }
    #endregion

    #region How many CDs in US, EU, UK
    #endregion

    #region Country with heighest average CD Cost
    #endregion

    #region Print names of cds that were released after 1990 and more than 9.00
    #endregion

    #region Names of CDS that were released before 1980 or from polydor company
    #endregion

    foreach (XmlNode node in xmlDoc.SelectSingleNode("CATALOG").SelectNodes("CD"))
    {
      CDInfo newCD = new CDInfo();
      foreach (XmlNode nodeInfo in node.ChildNodes)
      {
        if (nodeInfo.Name == "TITLE")
        {
          newCD.title = nodeInfo.InnerText;
        }
        if (nodeInfo.Name == "ARTIST")
        {
          newCD.artist = nodeInfo.InnerText;

        }
        if (nodeInfo.Name == "COUNTRY")
        {
          newCD.country = nodeInfo.InnerText;

          if (nodeInfo.InnerText == "USA")
            UsCDS++;
          if (nodeInfo.InnerText == "UK")
            UkCDS++;
          if (nodeInfo.InnerText == "EU")
            EuCDS++;
        }
        if (nodeInfo.Name == "COMPANY")
        {
          newCD.company = nodeInfo.InnerText;
        }
        if (nodeInfo.Name == "PRICE")
        {
          float price; float.TryParse(nodeInfo.InnerText, out price);

          newCD.price = price;
        }
        if (nodeInfo.Name == "YEAR")
        {
          int year; int.TryParse(nodeInfo.InnerText, out year);
          newCD.year = year;
        }
      }

      cds.Add(newCD);
    }

    sb.AppendLine($"Number of CD released for less than $9.00: {lessThanNineDollars}");
    sb.AppendLine();

    sb.AppendLine($"There {cdsAfter1990.Count} CDs released after 1990 and they were:");
    foreach (string cdName in cdsAfter1990)
    {
      sb.AppendLine($"\t{cdName}");
    }
    sb.AppendLine();

    sb.AppendLine($"CDs by country then count");
      sb.AppendLine($"\tUS: {UsCDS}");
      sb.AppendLine($"\tUK: {UkCDS}");
      sb.AppendLine($"\tEU: {EuCDS}");
    sb.AppendLine();


    Debug.Log(sb);
  }
}

[System.Serializable]
public class CDInfo
{
  public string title;
  public string artist;
  public string country;
  public string company;
  public float price;
  public int year;
}

