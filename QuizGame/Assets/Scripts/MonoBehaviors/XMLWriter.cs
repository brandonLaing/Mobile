using System.Collections;
using System.Collections.Generic;
using System.Xml;
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
}

[System.Serializable]
public class QUESTIONCOLLECTION
{
  public QUESTIONDATA[] QUESTIONS;
}
