using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Brandon Laing
 * FithTouch
 * This will will count how many times you have tapped the screen then display how many times you have hit it 5 times
 */
public class FithTouch : MonoBehaviour
{
  /// <summary>
  /// text box to display too
  /// </summary>
  [Tooltip("Text box to display too")]
  public Text display;

  /// <summary>
  /// number of times the screen has been tapped
  /// </summary>
  private int numberOfTouches;

  /// <summary>
  /// takes number of touches and devides it by five and returns 0 if number of touches is 0
  /// </summary>
  private int NumberOfFithTouches
  {
    get
    {
      if (numberOfTouches != 0)
        return numberOfTouches / 5;
      return 0;
    }
  }

  // Update is called once per frame
  void Update()
  {
    // evertime there is more than 1 finger down 
    if (Input.touchCount > 0)
    {
      // check the last one if it just pressed down
      var lastTouch = Input.touches[Input.touchCount - 1];
      if (lastTouch.phase == TouchPhase.Began)
      {
        numberOfTouches++;
      }
    }

    UpdateDispaly();
  }

  /// <summary>
  /// Updates the count to the display
  /// </summary>
  private void UpdateDispaly()
  {
    display.text = NumberOfFithTouches.ToString();
  }
}
