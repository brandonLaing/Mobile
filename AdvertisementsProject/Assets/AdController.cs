using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdController : MonoBehaviour
{
  public static AdController main;
  public bool showAd = false;

  private void Start()
  {
    if (main == null)
    {
      main = this;
      DontDestroyOnLoad(this.gameObject);
    }
    else if (main != this)
      Destroy(this.gameObject);
  }
}
