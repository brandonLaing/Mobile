using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
  public static PlayerSpeed main;
  public float playerSpeed;
  public float playerSpeedIncreasePerSecond = 5F;

  private void Start()
  {
    main = this;
    InvokeRepeating("IncreaseSpeed", 0, 1);

  }

  private void IncreaseSpeed()
  {
    playerSpeedIncreasePerSecond = 5;
  }
}
