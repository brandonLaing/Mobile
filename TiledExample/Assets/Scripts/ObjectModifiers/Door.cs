﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
  #region Variables

  #endregion

  #region Mono Behavior Functions

  #endregion

  #region Functions
  public void Interact()
  {
    Destroy(this.gameObject);
  }
  #endregion
}