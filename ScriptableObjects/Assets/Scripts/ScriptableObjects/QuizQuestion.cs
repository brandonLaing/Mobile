using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class QuizQuestion : ScriptableObject
{
  [SerializeField]
  private string _question;

  [SerializeField]
  private string[] _anwser;

  [SerializeField]
  private int _correctAnwser;

  public string Question
  {
    get
    {
      return _question;
    }
    set
    {
      _question = value;
    }
  }

  public int CorrectAnwser
  {
    get
    {
      return _correctAnwser;
    }
    set
    {
      _correctAnwser = value;
    }
  }

  public string[] Anwsers
  {
    get
    {
      return _anwser;
    }
    set
    {
      _anwser = value;
    }
  }

  public bool Asked
  {
    get;
    set;
  }
}

