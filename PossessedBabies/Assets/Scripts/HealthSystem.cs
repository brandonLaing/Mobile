using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
  public float healthMax;

  private float _health;
  public float Health
  {
    get
    {
      return _health;
    }
    set
    {
      if (value > healthMax)
        _health = healthMax;
      else if (value < 0)
        Die();
      else
        _health = value;
    }
  }


  public void Die()
  {

  }
}
