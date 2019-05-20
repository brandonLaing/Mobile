using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoneyTracker))]
public class MoneyTrackerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    MoneyTracker mt = target as MoneyTracker;

    mt.money = (decimal)EditorGUILayout.FloatField(new GUIContent("Money"), (float)mt.money);
  }
}