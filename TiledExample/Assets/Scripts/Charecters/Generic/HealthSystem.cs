using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
  #region Variables
  /// <summary>
  /// Current amount of health
  /// </summary>
  private int _health = 10;

  [Tooltip("Maximum amount of health allowed")]
  public int healthMax = 10;

  /// <summary>
  /// Called when units health gets to or bellow 0
  /// </summary>
  public event Action OnKill = delegate { };

  /// <summary>
  /// Health property that auto adjust and calls information
  /// </summary>
  public int Health
  {
    get
    {
      return _health;
    }
    set
    {
      _health = value;
      if (_health > healthMax)
        _health = healthMax;
      else if (_health <= 0)
        OnKill();
    }
  }
  #endregion

  #region MonoBehavior Events
  private void Awake()
  {
    if (GetComponent<ICanDie>() == null)
      Debug.LogWarning($"Found no death script. No effect upon death will happen for {transform.name}");
  }
  #endregion

  #region Methods
  /// <summary>
  /// Sets up the variables from map loader
  /// </summary>
  /// <param name="health">Starting health of the unit</param>
  /// <param name="max">Max health the play can be at</param>
  public void Initialize(int health, int max)
  {
    _health = health;
    healthMax = max;
  }

  /// <summary>
  /// Adds a certain amount of health to the player
  /// </summary>
  /// <param name="ammountToHeal">Amount to be added to the player</param>
  public void Heal(int ammountToHeal) =>
    Health += ammountToHeal;

  /// <summary>
  /// Removes a certain amount of health from the player
  /// </summary>
  /// <param name="ammountToDamage">Amount to be removed from the player</param>
  public void Damage(int ammountToDamage) =>
    Health -= ammountToDamage;

  /// <summary>
  /// Auto kills the unit from any health
  /// </summary>
  public void Kill() =>
    OnKill();
  #endregion
}
