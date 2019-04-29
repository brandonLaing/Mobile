using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionDoor : MonoBehaviour, IInteractable
{
  #region Variables
  public string nextScene;
  #endregion

  #region Mono Behavior Functions

  #endregion

  #region Functions
  public void Interact()
  {
    SceneManager.LoadScene(nextScene);
  }
  #endregion
}