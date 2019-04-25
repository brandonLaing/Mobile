using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PlayerDeath : AbstractDeath
{
  [SerializeField]
  [Tooltip("Main menu Scene")]
  private string mainSceneName = string.Empty;

  /// <summary>
  /// Effects to call when a NPC dies
  /// </summary>
  public override void Die()
  {
    if (Application.isPlaying)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
    }
  }
}