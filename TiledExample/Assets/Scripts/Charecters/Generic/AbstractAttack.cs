using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
  public float range;
  public float areaOfEffect;
  public int damage;

  public abstract void Attack(Vector2 position);
}
