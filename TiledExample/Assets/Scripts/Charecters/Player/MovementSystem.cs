using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 10;
  private Vector2 moveDirection = new Vector2();

  private void Awake()
  {
    if (GetComponent<EnemyAI>() != null)
      GetComponent<EnemyAI>().OnTargetLocationSet += TargetPosition;
  }

  private void OnDestroy()
  {
    if (GetComponent<EnemyAI>() != null)
      GetComponent<EnemyAI>().OnTargetLocationSet -= TargetPosition;
  }

  public void _AddMoveDirection(string moveDirString)
  {
    switch (moveDirString.ToLower())
    {
      case "up":
        moveDirection += Vector2.up;
        return;
      case "down":
        moveDirection += Vector2.down;
        return;
      case "left":
        moveDirection += Vector2.left;
        return;
      case "right":
        moveDirection += Vector2.right;
        return;
    }
  }

  public void _RemoveMoveDirection(string moveDirString)
  {
    switch (moveDirString.ToLower())
    {
      case "up":
        moveDirection -= Vector2.up;
        return;
      case "down":
        moveDirection -= Vector2.down;
        return;
      case "left":
        moveDirection -= Vector2.left;
        return;
      case "right":
        moveDirection -= Vector2.right;
        return;
    }
  }

  private void TargetPosition(Vector2 location)
  {
    transform.Translate(location * moveSpeed * Time.deltaTime);
  }
  
  private void FixedUpdate()
  {
    MovePlayer();
  }

  private void MovePlayer()
  {
    transform.position += (Vector3)moveDirection.normalized * (moveSpeed * Time.deltaTime);
  }
}
