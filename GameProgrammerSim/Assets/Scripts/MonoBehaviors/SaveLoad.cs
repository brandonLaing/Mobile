using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
  #region Variables

  #endregion

  #region Mono Behavior Functions
  private void OnDestroy()
  {
    SaveLoadSystem.GameExited();
  }
  #endregion

  #region Functions

  #endregion
}