using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(XMLWriter))]
public class XMLWriterEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    XMLWriter writer = target as XMLWriter;

    if (GUILayout.Button("MAKE XML FILE"))
    {
      writer.WriteXML();
    }
  }
}
