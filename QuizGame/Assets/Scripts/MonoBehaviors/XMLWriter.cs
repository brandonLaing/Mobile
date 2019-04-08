using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class XMLWriter : MonoBehaviour
{
  public QUESTIONDATA[] questions;

  public void WriteToXML()
  {
    QUESTIONCOLLECTION allQuestions = new QUESTIONCOLLECTION();
    allQuestions.QUESTIONS = questions;

    System.Xml.Serialization.XmlSerializer write =
      new System.Xml.Serialization.XmlSerializer(typeof(QUESTIONCOLLECTION));

    string path = Application.dataPath + "//XMLFile.xml";
    System.IO.FileStream file = System.IO.File.Create(path);

    Debug.Log("Making file");
    write.Serialize(file, allQuestions);

    XmlDocument xmlDoc = new XmlDocument();
    TextAsset xmlFile = new TextAsset();


    file.Close();

  }

  public void WriteXML()
  {
    var setting = new XmlWriterSettings();
    setting.Indent = true;
    XmlWriter writer = XmlWriter.Create("Assets/QuestionsFile.xml", setting);
    writer.WriteStartElement("questioncollection");
    foreach(QUESTIONDATA data in questions)
    {
      writer.WriteStartElement("questiondata");
      writer.WriteElementString("category", data.CATEGORY);
      writer.WriteElementString("id", data.ID.ToString());
      writer.WriteElementString("question", data.QUESTION.ToString());

      foreach(string answer in data.ANWSERS)
      {
        writer.WriteElementString("answer", answer);
      }

      writer.WriteElementString("correctanswer", data.CORRECTANSWER.ToString());
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
    writer.Flush();
  }
}

[System.Serializable]
public class QUESTIONCOLLECTION
{
  public QUESTIONDATA[] QUESTIONS;
}
