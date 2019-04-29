using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
  #region Variables
  [SerializeField]
  private float interactionRange = 4;
  #endregion

  #region Mono Behavior Functions
  private void Awake()
  {
    GetComponent<PlayerInputManager>().OnInteract += ValidateInteraction;
  }

  private void OnDestroy()
  {
    GetComponent<PlayerInputManager>().OnInteract -= ValidateInteraction;
  }
  #endregion

  #region Functions
  private void ValidateInteraction(Transform interactableTransform)
  {
    if (Vector2.Distance(interactableTransform.position, transform.position) <= interactionRange)
    {
      interactableTransform.GetComponent<IInteractable>().Interact();
    }
  }
  #endregion
}