using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
  private float moveSpeed = 5;
  private Vector3 direction;
  Rigidbody2D rb;
  public void MoveInDirection(Vector3 direction)
  {
    this.direction = direction;
  }

  private void FixedUpdate()
  {
    rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
  }
}
