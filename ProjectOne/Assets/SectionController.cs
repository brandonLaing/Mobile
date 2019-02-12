using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
  public float moveSpeed;

  private void FixedUpdate()
  {
    transform.position -= PlayerTurn.main.moveDirection * PlayerSpeed.main.playerSpeed * Time.fixedDeltaTime;
  }
}
