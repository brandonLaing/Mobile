using System.Xml;
using UnityEngine;

/// <summary>
/// Writes Question data into a XML File
/// By: Brandon Laing
/// </summary>
[ExecuteInEditMode]
public class XMLWriter : MonoBehaviour
{
  /// <summary>
  /// All questions to write into file
  /// </summary>
  [SerializeField]
  private QUESTIONDATA[] questions;

  /// <summary>
  /// Writes questions into an XML file
  /// </summary>
  public void WriteXML()
  {
    XmlWriterSettings setting = new XmlWriterSettings
    {
      Indent = true
    };

    XmlWriter writer = XmlWriter.Create("Assets/QuestionsFile.xml", setting);

    writer.WriteStartElement("questioncollection");
    foreach(QUESTIONDATA data in questions)
    {
      writer.WriteStartElement("questiondata");
      writer.WriteElementString("category", data.CATEGORY.ToString());
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
    writer.Close();
  }
}