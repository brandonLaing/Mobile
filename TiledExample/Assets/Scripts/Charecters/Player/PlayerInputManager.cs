using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputManager : MonoBehaviour
{
  public event Action<Transform> OnInteract = delegate { };
  public event Action<Vector2> OnAttackAttemp = delegate { };

  private void Update()
  {
    if (Input.touchCount > 0)
    {
      int currentTouch = Input.touchCount - 1;
      if (Input.touches[currentTouch].phase == TouchPhase.Began)
      {
        Vector2 touchPosition = Input.touches[currentTouch].position;
        Debug.Log($"New Touch pressed at {touchPosition}");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y)), out hit, 100))
        {
          if (hit.transform.GetComponent<IInteractable>() != null)
          {
            OnInteract(hit.transform);
            return;
          }

          OnAttackAttemp(touchPosition);
        }
      }
    }

    if (Input.GetMouseButtonDown(1))
    {
      Debug.Log("Mouse down");
      Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y)), out hit, 100))
      {
        if (hit.transform.GetComponent<IInteractable>() != null)
        {
          OnInteract(hit.transform);
          return;
        }
      }
      OnAttackAttemp(touchPosition);
    }
  }
}
