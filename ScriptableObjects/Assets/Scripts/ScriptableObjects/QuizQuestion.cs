using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

[CreateAssetMenu]
public class QuizQuestion : ScriptableObject
{
  public bool useXML = true;

  [SerializeField]
  private string _question;

  [SerializeField]
  private string[] _anwser;

  [SerializeField]
  private int _correctAnwser;

  private void Awake()
  {
    if (useXML)
    {
      if (!File.Exists("Questions.xml"))
      {
        WriteSameQuestionToXml();
      }
    }
  }

  private void WriteSameQuestionToXml()
  {

  }

  public string Question
  {
    get
    {
      return _question;
    }
  }

  public int CorrectAnwser
  {
    get
    {
      return _correctAnwser;
    }
  }

  public string[] Anwsers
  {
    get
    {
      return _anwser;
    }
  }

  public bool Asked
  {
    get;
    internal set;
  }

}

