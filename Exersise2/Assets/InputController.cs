using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// Transform of the current input
  /// </summary>
  [Tooltip("Transform for the input object with the hitbox")]
  public RectTransform inputTransform;

  /// <summary>
  /// Current line that is being tracked
  /// </summary>
  [HideInInspector]
  private MyLineRenderer currentLine;

  /// <summary>
  /// Correct combination of dots to connect
  /// </summary>
  [Tooltip("Correct combination to unlock the thing")]
  public string correctCode;

  /// <summary>
  /// Current combination to unlock the thing
  /// </summary>
  [Tooltip("Current combination to unlock the thing")]
  public string currentCode;

  /// <summary>
  /// maxNumber of connections possible
  /// </summary>
  public int MaxNumberOfConnections { get { return allLineRenderers.Count; } }

  /// <summary>
  /// How many connections have been made so far
  /// </summary>
  private int currentNumberOfConnections = 0;

  /// <summary>
  /// Materials for failure or success to get right combo
  /// </summary>
  [Tooltip("Materials for failure or success to get right combo")]
  public Material lineFailMat, lineSuccessMat;

  /// <summary>
  /// Tracks if a combo has succeed or failed
  /// </summary>
  private bool comboFailed = false, solved = false;

  /// <summary>
  /// List of all dots on the board
  /// </summary>
  private List<MyLineRenderer> allLineRenderers = new List<MyLineRenderer>();
  #endregion

  private void Start()
  {
    // fills out the list of all line renderers
    foreach (GameObject lineObj in GameObject.FindGameObjectsWithTag("Dot"))
    {
      var lineRend = lineObj.GetComponent<MyLineRenderer>();
      allLineRenderers.Add(lineRend); 
    }
  }

  void Update()
  {
    // if there is a touch
    if (Input.touchCount > 0)
    {
      // update the position of the hit box
      inputTransform.position = Input.touches[0].position;

      // if the finger has lifted check if the code is correct
      if (Input.touches[0].phase == TouchPhase.Ended)
      {
        ValidateCode(currentLine);
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    // on triggering something check if it has a line renderer
    if (collision.GetComponent<MyLineRenderer>() != null)
    {
      // if it does check what to do with that new line renderer
      var lineRenderer = collision.GetComponent<MyLineRenderer>();
      NewLineFound(lineRenderer);
    }
  }

  /// <summary>
  /// Function on what should happen when a new line is found
  /// </summary>
  /// <param name="lineRenderer">New line renderer that has been found</param>
  private void NewLineFound(MyLineRenderer lineRenderer)
  {
    // if a combo has been failed
    if (comboFailed)
    {
      // invert the failed
      comboFailed = false;

      // reset lines of each line renderer
      foreach (MyLineRenderer lineRend in allLineRenderers)
      {
        lineRend.ResetLine();
      }
    }

    // check if the new line is already done or if its our current line
    if (!lineRenderer.lineFinished && lineRenderer != currentLine)
    {
      // now that its a valid line update info
      currentCode += lineRenderer.identifier;
      currentNumberOfConnections++;

      // check if its our last lineRenderer
      if (currentNumberOfConnections == MaxNumberOfConnections)
      {
        // if it is check if we have the right code then leave function
        currentLine.EndFollow(lineRenderer.offset * -1);
        ValidateCode(lineRenderer);
        return;
      }

      // have the new line start following input
      lineRenderer.StartFollowingInput();

      // check if we are working with a current line
      if (currentLine != null)
      {
        // if we are set its endpoint
        currentLine.EndFollow(lineRenderer.offset * -1);
      }

      // set our new line renderer as our current
      currentLine = lineRenderer;
    }
  }

  /// <summary>
  /// This will validate the current code we are working with
  /// </summary>
  private void ValidateCode(MyLineRenderer currentLineRend)
  {
    Debug.Log("Validating Code");

    // if its solved dont do anything
    if (solved == true)
      return;

    // if the code is correct
    if (currentCode == correctCode)
    {
      // start coroutine to reload the scene
      StartCoroutine(WaitThenReloadScene());
      // set solved to true
      solved = true;
      
      // if the current line is drawing the reset it
      if (currentLine.drawLine)
      {
        currentLine.ResetLine();
      }

      // change the color of all lines to green
      foreach (MyLineRenderer lineRend in allLineRenderers)
      {
        lineRend.ChangeLinesColor(lineSuccessMat);
        lineRend.lineFinished = true;
      }
    }
    // if the combo was wrong
    else
    {
      // reset the code, number of connections so far, and set combo failed to true
      currentCode = "";
      currentNumberOfConnections = 0;
      comboFailed = true;

      // if we have a current line and its drawing reset the line
      if (currentLine != null && currentLine.drawLine)
      {
        currentLine.ResetLine();
      }

      // update the color of the line to red
      foreach (MyLineRenderer lineRend in allLineRenderers)
      {
        lineRend.ChangeLinesColor(lineFailMat);
      }
    }

    // get rid of our current line
    currentLine = null;
  }

  /// <summary>
  /// Waits a specific amount of time then reloads the scene
  /// </summary>
  /// <returns></returns>
  private IEnumerator WaitThenReloadScene()
  {
    yield return new WaitForSecondsRealtime(5);
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
  }
}
