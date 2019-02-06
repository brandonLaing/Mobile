using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Brandon Laing (JCCC)
 * This script will for a message to be sent to it from the input controller. Once its has the 
 * signal it will follow the input device around the screen. It will then stop following the
 * inputs once it has received another direction to stop tracking the inputs.
 */

public class MyLineRenderer : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// Specific code given for this dot
  /// </summary>
  [Tooltip("Code given for this dot")]
  public char identifier;

  /// <summary>
  /// Transform for drawn the line
  /// </summary>
  [Tooltip("Transform for the drawn line")]
  public RectTransform lineTransform;

  /// <summary>
  /// Width of the drawn line
  /// </summary>
  [Tooltip("How thicc your line will be")]
  public float lineWidth;

  /// <summary>
  /// The visual for the endpoint
  /// </summary>
  [Tooltip("Visual for endpoint")]
  public RectTransform endPointTrans;

  /// <summary>
  /// Offset of the dot from the bottom left
  /// </summary>
  [Tooltip("Offset of the dot from the bottom left of the screen")]
  public Vector2 offset;


  /// <summary>
  /// Whether or not the line will be drawn
  /// </summary>
  [HideInInspector]
  public bool drawLine = false;

  /// <summary>
  /// Check if the line has already been drawn
  /// </summary>
  [HideInInspector]
  public bool lineFinished = false;

  /// <summary>
  /// Default material for the line
  /// </summary>
  [Tooltip("Default material for the line")]
  public Material lineDefaultMat;
  #endregion

  private void Start()
  {
    // turn off the line renderer so its not displaying anything
    lineTransform.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (drawLine)
      UpdateLine();
  }

  /// <summary>
  /// Method to start the line following the input
  /// </summary>
  /// <returns>Position of this point</returns>
  public void StartFollowingInput()
  {
    lineTransform.gameObject.SetActive(true);
    drawLine = true;
  }

  Vector2 endPoint;
  /// <summary>
  /// Method to draw line from this objects start to the current input
  /// </summary>
  private void UpdateLine()
  {
    if (Input.touchCount > 0)
    {
      endPoint = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y) + offset;
    }
    DrawLine(endPoint);

  }

  /// <summary>
  /// Set the endpoint of the line and updates the visual
  /// </summary>
  /// <param name="endPosition">End position of the current line</param>
  public void EndFollow(Vector2 endPosition)
  {
    var endPoint = endPosition + offset;
    DrawLine(endPoint);

    drawLine = false; lineFinished = true;
  }

  /// <summary>
  /// Draws the line from start to its current position
  /// </summary>
  /// <param name="inputPosition">Current position of your input source</param>
  private void DrawLine(Vector2 inputPosition)
  {
    // resize the line to new length
    lineTransform.sizeDelta = new Vector2(inputPosition.magnitude, lineWidth);
    lineTransform.pivot = new Vector2(0, 0.5F);
    // get the angle to the new input position
    float angle = Mathf.Atan2(inputPosition.y, inputPosition.x) * Mathf.Rad2Deg;
    // rotate line to look to that direction
    lineTransform.rotation = Quaternion.Euler(0, 0, angle);

    // update the debugging visual
    endPointTrans.localPosition = inputPosition;
  }

  /// <summary>
  /// If the line is currently active it changes the material of the line
  /// </summary>
  /// <param name="mat">Mat the line will be changed too</param>
  public void ChangeLinesColor(Material mat)
  {
    if (lineTransform.gameObject.activeSelf)
      lineTransform.gameObject.GetComponent<Image>().material = mat;
  }

  /// <summary>
  /// Hides lines and puts them back to their default state
  /// </summary>
  public void ResetLine()
  {
    if (lineTransform.gameObject.activeSelf)
    {
      lineTransform.gameObject.SetActive(false);
      lineTransform.GetComponent<Image>().material = lineDefaultMat;
    }

    lineFinished = false;
  }
}
