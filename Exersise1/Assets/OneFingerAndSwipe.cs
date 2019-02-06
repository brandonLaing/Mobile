using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Brandon Laing
 * One Finger and Swipe
 * This check is you have on finger on the screen and swipe with another. It will also display the ammount of times you do that to the screen.
 */
public class OneFingerAndSwipe : MonoBehaviour
{
  /// <summary>
  /// text box to display too
  /// </summary>
  [Tooltip("Text box to display too")]
  public Text display;

  /// <summary>
  /// count to increment
  /// </summary>
  private int touchAndSwipeCount;

  /// <summary>
  /// min distance to touch to be considered swipe
  /// </summary>
  [Tooltip("Minimun distance to be considered a swipe")]
  public float swipeMinDistance = 3;

  /// <summary>
  /// start location of a swipe 
  /// </summary>
  private Vector3 swipeStart;

  void Update()
  {
    // when there is more than one touch
    if (Input.touchCount > 1)
    {
      // if the second touch just begain store its location
      if (Input.touches[1].phase == TouchPhase.Began)
      {
        swipeStart = Camera.main.ScreenToViewportPoint(Input.touches[1].position);
        return;
      }
      
      // if the first touch is stationary and the second has ended
      if (Input.touches[0].phase == TouchPhase.Stationary && Input.touches[1].phase == TouchPhase.Ended)
      {
        var xDiff = Camera.main.ScreenToViewportPoint(Input.touches[1].position).x - swipeStart.x;

        // check the distance between the two
        if (Mathf.Abs(xDiff) > swipeMinDistance)
        {
          // add one to the count
          touchAndSwipeCount++;
        }
      }
    }
    UpdateDispaly();
  }

  /// <summary>
  /// Updates the count to the display
  /// </summary>
  private void UpdateDispaly()
  {
    display.text = touchAndSwipeCount.ToString();
  }
}
