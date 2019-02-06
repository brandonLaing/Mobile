using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Brandon Laing
 * Three Fingers
 * This will count how many times there has been 3 finger on the screen
 */
public class ThreeFingers : MonoBehaviour
{
  /// <summary>
  /// text box to display too
  /// </summary>
  [Tooltip("Text box to display too")]
  public Text display;

  /// <summary>
  /// number of times there has been 3 fingers on the screen
  /// </summary>
  private int numberOfThreeFingers = 0;

  void Update()
  {
    // if you have 3 fingers down
    if (Input.touchCount == 3)
    {
      // and the third finger was just placed
      if (Input.touches[2].phase == TouchPhase.Began)
      {
        // add one to the count
        numberOfThreeFingers++;
      }
    }

    UpdateDispaly();
  }

  /// <summary>
  /// Updates the count to the display
  /// </summary>
  private void UpdateDispaly()
  {
    display.text = numberOfThreeFingers.ToString();
  }
}