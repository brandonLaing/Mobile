using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  public float moveSpeed;

  /// <summary>
  /// Moves the player in a given direction not delta timed or normalized
  /// </summary>
  /// <param name="direction">Direction to move in not delta timed or normalized</param>
  public void Move(Vector3 direction) 
    => transform.position += direction.normalized * moveSpeed * Time.deltaTime;

  public void MoveTo(Vector3 position)
    => transform.Translate(position);
}
