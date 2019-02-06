using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneFingerRightSwipeLeft : MonoBehaviour
{
  /// <summary>
  /// text box to display too
  /// </summary>
  [Tooltip("Text box to display too")]
  public Text display;

  // number of times user has done the right thing
  private int successCount;

  // starting position of the swipes
  public Vector3 leftSwipeStart;
  // distance the swipe has to travel
  [Tooltip("Minimun distance to be considered to be a swipe")]
  public float minSwipeDistance = 2;

  private void Update()
  {
    // if there is more than one touch
    if (Input.touchCount > 1)
    {
      // if the second touch just started save its location
      if (Input.touches[1].phase == TouchPhase.Began)
      {
        Debug.Log("Started new swipe");

        // save it to the start position
        leftSwipeStart = Camera.main.ScreenToViewportPoint(Input.touches[1].position);
        return;
      }

      // find the diffrence
      float xDiff = Input.touches[1].position.x - leftSwipeStart.x;

      // check if the first finger is on the right side
      if (Camera.main.ScreenToViewportPoint(Input.touches[0].position).x > .5F)
      {
        Debug.Log("finger right");
        // check the distance is long enough
        if (Mathf.Abs(xDiff) > minSwipeDistance && Input.touches[1].phase == TouchPhase.Ended)
        {
          Debug.Log("condition cleared");
          // increment the count
          successCount++;
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
    display.text = successCount.ToString();
  }
}
