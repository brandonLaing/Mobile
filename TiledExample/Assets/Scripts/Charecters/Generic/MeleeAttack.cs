using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AbstractAttack
{
  [SerializeField]
  private bool isPlayer;
  private Dictionary<Vector3, float> attacks = new Dictionary<Vector3, float>();
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
    Debug.Log($"Attempting attack\nPos: {position}\nRange: {range}\nDirection: {(position - (Vector2)transform.position).normalized}");


    Vector2 attackPoint = (position - (Vector2)transform.position).normalized * range;
    Collider2D[] foundObjects = Physics2D.OverlapCircleAll((Vector2)transform.position + attackPoint, areaOfEffect);

    attacks.Add((Vector2)transform.position + attackPoint, areaOfEffect);
    Debug.Log("Length " + foundObjects.Length);

    for (int i = 0; i < foundObjects.Length; i++)
    {
      Debug.Log("Transform: " + foundObjects[i].name);
      for (Transform trans = foundObjects[i].transform; trans != null; trans = trans.parent)
      {
        Debug.Log("Phase 2: " + foundObjects[i].name);
        HealthSystem hs = foundObjects[i].GetComponent<HealthSystem>();
        if (hs != null && hs.transform != this.transform)
        {
          Debug.Log("Hit something");
          hs.Damage(damage);
        }
      }
    }
  }

  private void OnDrawGizmos()
  {
    if (Application.isPlaying)
    {
      foreach (KeyValuePair<Vector3, float> attack in attacks)
        Gizmos.DrawSphere(attack.Key, attack.Value);
    }
  }
}
