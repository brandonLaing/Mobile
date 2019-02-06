using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Destroy : MonoBehaviour
{
  public GameObject deathEffect;

  private void Update()
  {
    if (Input.touchCount > 0)
    {
      Touch lastTouch = Input.touches[Input.touchCount - 1];

      if (lastTouch.phase == TouchPhase.Began)
      {
        GameObject tappedObject = Tap_Object.TouchedObject(lastTouch);
        if (tappedObject != null && tappedObject.CompareTag("Obstacle"))
        {
          Destroy(Instantiate(deathEffect, tappedObject.transform.position, Quaternion.identity), 1);
          Destroy(tappedObject);
        }
      }
    }
  }
}
