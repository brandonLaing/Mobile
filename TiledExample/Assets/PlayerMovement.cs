using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed;
  Vector2 moveDirection = new Vector3();
  void Update()
  {
    moveDirection = Vector2.zero;
    if (Input.GetKey(KeyCode.W)) moveDirection += Vector2.up;
    if (Input.GetKey(KeyCode.S)) moveDirection += Vector2.down;
    if (Input.GetKey(KeyCode.A)) moveDirection += Vector2.left;
    if (Input.GetKey(KeyCode.D)) moveDirection += Vector2.right;

    moveDirection.Normalize();
  }

  private void FixedUpdate()
  {
    MovePlayer(moveDirection);
  }

  private void MovePlayer(Vector3 direction)
  {
    transform.position += direction * (moveSpeed * Time.deltaTime);
  }
}
