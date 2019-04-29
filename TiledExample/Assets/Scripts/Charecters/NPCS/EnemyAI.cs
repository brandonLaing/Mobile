using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  public EnemyAIState aiState = EnemyAIState.Idle;
  public Vector2 startingPosition;
  public float chaseRange = 4;
  public Transform targetObject = null;
  public event Action<Vector2> OnTargetLocationSet = delegate { };
  public float attackTime = 1;
  public AbstractAttack attack;

  private void Awake()
  {
    attack = GetComponent<AbstractAttack>();
  }

  private void Start()
  {
    startingPosition = transform.position;
  }

  private void Update()
  {
    AILogic();
  }

  private void AILogic()
  {
    switch (aiState)
    {
      case EnemyAIState.Idle:
        ReturnToStart();
        CheckForEnemy();
        return;
      case EnemyAIState.Chase:
        MoveToTarget();
        CheckAttackRange();
        return;
    }
  }

  private void ReturnToStart()
  {
    OnTargetLocationSet(startingPosition);
  }

  private void CheckForEnemy()
  {
    Collider2D[] foundObjects = Physics2D.OverlapCircleAll(transform.position, chaseRange);
    if (foundObjects.Length > 0)
    {
      bool foundPlayer = false;
      for (int i = 0; i < foundObjects.Length || !foundPlayer; i++)
      {
        for (Transform trans = foundObjects[i].transform; trans != null; trans = trans.parent)
        {
          if (trans.GetComponent<MovementSystem>() != null)
          {
            targetObject = trans;
            aiState = EnemyAIState.Chase;
            break;
          }
        }
      }
    }
  }

  private void MoveToTarget()
  {
    if (Vector2.Distance(transform.position, targetObject.position) > chaseRange * 2)
    {
      targetObject = null;
      aiState = EnemyAIState.Idle;
      return;
    }

    OnTargetLocationSet(targetObject.position);
  }

  private void CheckAttackRange()
  {
    if (Vector2.Distance(transform.position, targetObject.position) <= attack.range)
    {
      aiState = EnemyAIState.Attacking;
      Invoke("SendAttackTrigger", attackTime);
    }
  }

  private void SendAttackTrigger()
  {
    attack.Attack(targetObject.position);
  }
}
