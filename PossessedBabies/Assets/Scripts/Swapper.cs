using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
  private bool _isPlayer;
  public bool IsPlayer
  {
    get { return _isPlayer; }
    set
    {
      if (value)
        transform.tag = "Player";
      else
        transform.tag = "Untagged";
    }
  }

  private void Start()
  {
    if (GetComponent<PlayerController>())
      IsPlayer = true;
    else
      IsPlayer = false;

  }

  public void Swap()
  {
    if (GetComponent<AIController>())
    {
      Destroy(GetComponent<AIController>());
      gameObject.AddComponent<PlayerController>();
      GetComponentInChildren<Camera>().enabled = true;
      GetComponentInChildren<Camera>().transform.tag = "MainCamera";

      IsPlayer = true;
    }
    else
    {
      Destroy(GetComponent<PlayerController>());
      gameObject.AddComponent<AIController>();
      GetComponentInChildren<Camera>().enabled = false;
      GetComponentInChildren<Camera>().transform.tag = "Untagged";

      IsPlayer = false;
    }
  }
}
