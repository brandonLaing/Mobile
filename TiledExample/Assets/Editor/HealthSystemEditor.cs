using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthSystem))]
public class HealthSystemEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    HealthSystem hs = target as HealthSystem;
    hs.Health = EditorGUILayout.IntField(new GUIContent("Health", "Current health of the unit"), hs.Health);
  }
}
