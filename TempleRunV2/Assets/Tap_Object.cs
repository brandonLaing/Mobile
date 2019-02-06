using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_Object : MonoBehaviour
{
  public static GameObject TouchedObject (Touch touch)
  {
    Ray touchedRay = Camera.main.ScreenPointToRay(touch.position);

    RaycastHit hit;
    if (Physics.Raycast(touchedRay, out hit))
    {
      return hit.collider.gameObject;
    }

    // didnt detect hit
    return null;
  }
}
