using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : MonoBehaviour, IAttack
{
  public float damage;
  public float cooldownTime;
  private float cooldown;
  public float radius;

  public void Attack(HealthSystem hs, float damage)
  {
    if (Time.realtimeSinceStartup > cooldown)
    {
      hs.Damage(this.damage);
      cooldown = Time.realtimeSinceStartup + cooldownTime;
    }
  }
}
