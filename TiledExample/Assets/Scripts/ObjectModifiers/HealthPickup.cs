using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
  #region Variables
  public int healthHealed = 2;
  #endregion

  #region Mono Behavior Functions
  private void OnTriggerEnter(Collider other)
  {
    for (Transform trans = other.transform; trans != null; trans = trans.parent)
    {
      HealthSystem hs = GetComponent<HealthSystem>();
      if (hs != null)
        hs.Heal(healthHealed);
      Destroy(this.gameObject);
    }
  }
  #endregion

  #region Functions

  #endregion
}