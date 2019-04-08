using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestionController))]
public class QuestionControllerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    QuestionController targetScript = target as QuestionController;
    if (GUILayout.Button("Grab Ids"))
      targetScript.GrabIds();

    if (GUILayout.Button("Grab Question"))
      targetScript.GetQuestion();
  }
}
