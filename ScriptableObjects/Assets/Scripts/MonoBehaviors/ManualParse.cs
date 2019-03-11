using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Text;
using System;

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
    int UsCDS = 0, UkCDS = 0, EuCDS = 0;
    foreach (XmlNode cdNode in rootNode.SelectNodes("CD"))
    {
      foreach (XmlNode countryNode in cdNode.SelectNodes("COUNTRY"))
      {
        if (countryNode.InnerText == "USA")
          UsCDS++;
        if (countryNode.InnerText == "UK")
          UkCDS++;
        if (countryNode.InnerText == "EU")
          EuCDS++;
      }
    }
    #endregion

    #region Country with heighest average CD Cost
    List<float> ukPrices = new List<float>(), usPrices = new List<float>(), euPrices = new List<float>();
    float ukAverage = 0, usAverage = 0, euAverage = 0;

    foreach (XmlNode cdNode in rootNode.ChildNodes)
    {
      float currentPrice; float.TryParse(cdNode.SelectSingleNode("PRICE").InnerText, out currentPrice);
      string country = cdNode.SelectSingleNode("COUNTRY").InnerText;

      switch (country)
      {
        case "UK":
          ukPrices.Add(currentPrice);
          break;
        case "USA":
          usPrices.Add(currentPrice);
          break;
        case "EU":
          euPrices.Add(currentPrice);
          break;
      }
    }

    ukAverage = GetAverage(ukPrices);
    usAverage = GetAverage(usPrices);
    euAverage = GetAverage(euPrices);
    #endregion

    #region Print names of cds that were released after 1990 and more than 9.00
    List<string> cdsAfter1990LessThan9 = new List<string>();
    foreach (XmlNode cdNode in rootNode.ChildNodes)
    {
      int date; int.TryParse(cdNode.SelectSingleNode("YEAR").InnerText, out date);
      if (date > 1990)
      {
        float price; float.TryParse(cdNode.SelectSingleNode("PRICE").InnerText, out price);
        if (price > 9.00)
        {
          cdsAfter1990LessThan9.Add(cdNode.SelectSingleNode("TITLE").InnerText);
        }
      }
    }
    #endregion

    #region Names of CDS that were released before 1980 or from polydor company
    foreach (XmlNode node in xmlDoc.SelectSingleNode("CATALOG").SelectNodes("CD"))
    {
      CDInfo newCD = new CDInfo(node);

      cds.Add(newCD);
    }

    List<string> before1980OrPolydor = new List<string>();
    foreach (CDInfo cd in cds)
    {
      if (cd.year < 1980 || cd.company.ToLower() == "polydor".ToLower())
      {
        before1980OrPolydor.Add(cd.title);
      }
    }
    #endregion



    BuildStringBuilder(sb, lessThanNineDollars, cdsAfter1990, UsCDS, UkCDS, EuCDS, 
      ukAverage, usAverage, euAverage, cdsAfter1990LessThan9, before1980OrPolydor);

    Debug.Log(sb);
  }

  private static void BuildStringBuilder(StringBuilder sb, int lessThanNineDollars, 
    List<string> cdsAfter1990, int UsCDS, int UkCDS, int EuCDS, float ukAverage, float usAverage, float euAverage, 
    List<string> cdsAfter1990LessThan9, List<string> before1980OrPolydor)
  {
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

    sb.AppendLine("Average CD price by coutry");
    sb.AppendLine($"\tUK: ${Math.Round(ukAverage, 2)}");
    sb.AppendLine($"\tUS: ${Math.Round(usAverage, 2)}");
    sb.AppendLine($"\tEU: ${Math.Round(euAverage, 2)}");
    sb.AppendLine();

    sb.AppendLine("CDs that were released after 1990 for more than $9");
    foreach (string name in cdsAfter1990LessThan9)
    {
      sb.AppendLine($"\t{name}");
    }
    sb.AppendLine();

    sb.AppendLine("CDs that were released before 1980 or from Polydor");
    foreach (string title in before1980OrPolydor)
    {
      sb.AppendLine($"\t{title}");
    }
    sb.AppendLine();

    sb.AppendLine("Thats it");
  }

  private static float GetAverage(List<float> ukPrices)
  {
    float temp = 0;
    foreach (float price in ukPrices)
      temp += price;

    return temp / (float)ukPrices.Count; ;
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

  public CDInfo(XmlNode xmlNode)
  {
    title = xmlNode.SelectSingleNode("TITLE").InnerText;
    artist = xmlNode.SelectSingleNode("ARTIST").InnerText;
    country = xmlNode.SelectSingleNode("COUNTRY").InnerText;
    company = xmlNode.SelectSingleNode("COMPANY").InnerText;
    float.TryParse(xmlNode.SelectSingleNode("PRICE").InnerText, out price);
    int.TryParse(xmlNode.SelectSingleNode("YEAR").InnerText, out year);
  }
}

