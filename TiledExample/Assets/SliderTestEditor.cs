using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SliderTest))]
public class SliderTestEditor : Editor
{
  float sizeCurrent;
  float sizeMin = 0, sizeMax = 10;

  public override void OnInspectorGUI()
  {
    float startingSize = sizeCurrent;
    sizeCurrent = EditorGUILayout.FloatField("Saved temp",sizeCurrent);

    EditorGUILayout.Space();
    EditorGUILayout.Space();
    EditorGUILayout.Space();

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("UnboundSlider", GUILayout.Width(100));

    float sliderSize;
    if (sizeCurrent > sizeMax)
      sliderSize = GUILayout.HorizontalSlider(sizeMax, sizeMin, sizeMax, GUILayout.Width(200));
    else if (sizeCurrent < sizeMin)
      sliderSize = GUILayout.HorizontalSlider(sizeMin, sizeMin, sizeMax, GUILayout.Width(200));
    else
      sliderSize = GUILayout.HorizontalSlider(sizeCurrent, sizeMin, sizeMax, GUILayout.Width(200));

    float boxSize = EditorGUILayout.FloatField(sizeCurrent, GUILayout.Width(100));

    if (sliderSize != boxSize)
    {
      if (boxSize != startingSize)
        sizeCurrent = boxSize;
      else if (sliderSize > sizeMin && sliderSize < sizeMax)
        sizeCurrent = sliderSize;
      else
      {
        if (boxSize > sizeMax || boxSize < sizeMin)
          sizeCurrent = boxSize;
        else
          sizeCurrent = sliderSize;
      }
    }
    EditorGUILayout.EndHorizontal();
  }
}
