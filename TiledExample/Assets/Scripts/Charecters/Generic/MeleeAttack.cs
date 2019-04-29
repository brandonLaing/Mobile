using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AbstractAttack
{
  public override void Attack(Vector2 position)
  {
    Vector2 attackPoint = (position - (Vector2)transform.position).normalized * range;
    Collider2D[] foundObjects = Physics2D.OverlapCircleAll(attackPoint, areaOfEffect);

    for (int i = 0; i < foundObjects.Length; i++)
      for (Transform trans = foundObjects[i].transform; trans != null; trans = trans.parent)
      {
        HealthSystem hs = GetComponent<HealthSystem>();
        if (hs != null)
          hs.Damage(damage);
      }
  }
}
