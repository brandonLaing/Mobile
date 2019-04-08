using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QUESTIONDATA : ScriptableObject
{
  public string ID;
  public string CATEGORY;
  public string QUESTION;
  public string[] ANWSERS;
  public int CORRECTANSWER;
}
