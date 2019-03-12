using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QUESTIONDATA : ScriptableObject
{
  public int ID;
  public string QUESTION;
  public string[] ANWSERS;
  public int CORRECTANSWER;
}
