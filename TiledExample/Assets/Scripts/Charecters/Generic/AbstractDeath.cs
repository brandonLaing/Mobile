using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public abstract class AbstractDeath : MonoBehaviour, ICanDie
{
  /// <summary>
  /// Health system this death is subscribed to
  /// </summary>
  private HealthSystem hs = null;

  /// <summary>
  /// Grabs health system and subscribed death to it
  /// </summary>
  private void Awake()
  {
    hs = GetComponent<HealthSystem>();
    if (hs != null)
      hs.OnKill += Die;
    else
      Destroy(this);
  }

  /// <summary>
  /// Effects to call when a NPC dies
  /// </summary>
  public abstract void Die();

  /// <summary>
  /// Unsub check if the NPC never dies
  /// </summary>
  private void OnDestroy()
  {
    if (hs != null)
      hs.OnKill -= Die;
  }
}
