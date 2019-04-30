using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class NPCDeath : AbstractDeath
{
  [SerializeField]
  [Tooltip("Effect that shows when the NPC dies")]
  private GameObject deathParticle = null;

  /// <summary>
  /// Effects to call when a NPC dies
  /// </summary>
  public override void Die()
  {
    if (deathParticle != null)
    {
      Debug.Log("making particle");
      Instantiate(deathParticle, transform.position, Quaternion.identity);
      Destroy(this.gameObject);
    }
    else
    {
      Destroy(this.gameObject);
    }
  }
}
