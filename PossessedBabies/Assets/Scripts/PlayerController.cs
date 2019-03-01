using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Movement movementController;
  private Turning turningController;

  private IAttack attack;

  private void Awake()
  {
    movementController = GetComponent<Movement>();
    turningController = GetComponent<Turning>();

    attack = GetComponent<IAttack>();
  }

  private void Update()
  {
    GetInputs(); Possess();
  }

  public void GetInputs()
  {
    #region Movement
    Vector3 direction = new Vector3();

    if (Input.GetKey(KeyCode.W))
      direction += transform.forward;
    if (Input.GetKey(KeyCode.S))
      direction -= transform.forward;
    if (Input.GetKey(KeyCode.A))
      direction -= transform.right;
    if (Input.GetKey(KeyCode.D))
      direction += transform.right;

    movementController.Move(direction);
    #endregion

    #region Turning
    turningController.RotateX(Input.GetAxis("Mouse X"));
    turningController.RotateY(Input.GetAxis("Mouse Y"));
    #endregion

    #region Attack
    if (Input.GetMouseButtonDown(0))
    {
      foreach (Collider obj in Physics.OverlapSphere(transform.forward * 2, 2))
      {
        if (obj.GetComponent<HealthSystem>() && obj.transform != this.transform)
          attack.Attack(obj.GetComponent<HealthSystem>(), 5);
      }
    }
    #endregion
  }

  public void Possess()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      foreach (Collider obj in Physics.OverlapSphere(transform.position, 3))
      {
        Swapper swap = obj.GetComponent<Swapper>();
        if (swap != null && swap.transform != GetComponent<Swapper>().transform)
        {
          swap.Swap();
          GetComponent<Swapper>().Swap();
        }
      }
    }
  }
}
