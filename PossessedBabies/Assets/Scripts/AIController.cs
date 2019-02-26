using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
  Patrol, Arrive, Attack, Hover, ReturnBaby
}

public class AIController : MonoBehaviour
{
  public AIState aiState;

  private void Update()
  {
    switch (aiState)
    {
      case AIState.Patrol:
        PatrolLogic();
        break;
      case AIState.Arrive:
        ArriveLogic();
        break;
      case AIState.Attack:
        AttackLogic();
        break;
      case AIState.Hover:
        HoverLogic();
        break;
      case AIState.ReturnBaby:
        ReturnBabyLogic();
        break;
    }
  }

  private void PatrolLogic()
  {

  }

  private void ArriveLogic()
  {

  }

  private void AttackLogic()
  {

  }

  private void HoverLogic()
  {

  }

  private void ReturnBabyLogic()
  {

  }
}
