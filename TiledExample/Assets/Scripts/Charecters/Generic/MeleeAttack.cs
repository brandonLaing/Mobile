using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AbstractAttack
{
  [SerializeField]
  private bool isPlayer;
  private void Awake()
  {
    if (isPlayer)
      GetComponent<PlayerInputManager>().OnAttackAttemp += Attack;
  }

  private void OnDestroy()
  {
    if (isPlayer)
      GetComponent<PlayerInputManager>().OnAttackAttemp -= Attack;
  }

  public override void Attack(Vector2 position)
  {
    Debug.Log("Attempting attack");

    Vector2 attackPoint = (position - (Vector2)transform.position).normalized * range;
    Collider2D[] foundObjects = Physics2D.OverlapCircleAll(attackPoint, areaOfEffect);

    Gizmos.DrawSphere(attackPoint, areaOfEffect);

    for (int i = 0; i < foundObjects.Length; i++)
      for (Transform trans = foundObjects[i].transform; trans != null; trans = trans.parent)
      {
        HealthSystem hs = GetComponent<HealthSystem>();
        if (hs != null && hs.transform != this.transform)
          hs.Damage(damage);
      }
  }

  private void OnDrawGizmos()
  {
    if (Application.isPlaying)
    {

    }
  }
}
