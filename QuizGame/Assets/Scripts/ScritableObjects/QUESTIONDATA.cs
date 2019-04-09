using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container holding info on the questions information for xml writer
/// By: Brandon Laing
/// </summary>
[CreateAssetMenu]
public class QUESTIONDATA : ScriptableObject
{
  public string ID;
  public QuestionCategories CATEGORY;
  public string QUESTION;
  public string[] ANWSERS;
  public int CORRECTANSWER;
}
