using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and receiving input.
/// </summary>

public enum INPUT_TYPE
{
  Acceleromety,
  Touch

}
[RequireComponent(typeof(Rigidbody))]
public class Move_Player : MonoBehaviour
{

  /// <summary>
  /// A reference to the Rigidbody component
  /// </summary>
  Rigidbody rb;

  [Tooltip("How fast the ball moves left/right")]
  public float dodgeSpeed = 5f;

  [Tooltip("How fast the ball rolls forward (automatically)")]
  [Range(0, 10)]
  public float rollSpeed = 5;

  /// <summary>
  /// How much force should be applied in the horizontal axis
  /// </summary>
  float horizontalSpeed;

  public Vector3 forwardMovement;

  public Vector3 touchStart = new Vector3();

  [Header("Swipe properties")]
  [Tooltip("How far the ball moves if the sceen is swiped")]
  [Range(1,5)]
  public float swipeMove = 2f;

  [Tooltip("How far the player will have to swipe")]
  [Range(0,2)]
  public float swipeMinDistance = 2f;

  [Header("Movement Options")]
  [Tooltip("Swaps between types of movement")]
  public INPUT_TYPE inputType = INPUT_TYPE.Acceleromety;

  /// <summary>
  /// Use this for initialization
  /// </summary>
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  /// <summary>
  /// Update it called by frame
  /// </summary>
  void Update()
  {

    horizontalSpeed = 0;

    #if UNITY_STANDALONE || UNITY_WEBPLAYER
    horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

    // If the mouse button is held down, (the screen is tapped)
    if (Input.GetMouseButton(0))
    {
      var inputPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

      // update horizontal speed
      horizontalSpeed = CalculateMovement(inputPosition) * dodgeSpeed;
    }

    #elif UNITY_IOS || UNITY_ANDROID
    // check for input type
    if (inputType == INPUT_TYPE.Acceleromety)
    {
      // move based on accelerometer
      horizontalSpeed = Input.acceleration.x * dodgeSpeed;
    }

    // real android touch
    if (Input.touchCount > 0)
    {
      var lastTouch = Input.touches[Input.touchCount - 1];

      if (inputType == INPUT_TYPE.Touch)
      {
        var worldPos = Camera.main.ScreenToViewportPoint(lastTouch.position);
        horizontalSpeed = CalculateMovement(worldPos) * dodgeSpeed;

      }

      SwipeTeleport(lastTouch);

    }

    #endif

  }

  private void SwipeTeleport(Touch touch)
  {
    // check if touch has just started
    if (touch.phase == TouchPhase.Began)
    {
      touchStart = touch.position;
    }

    else if (touch.phase == TouchPhase.Ended)
    {
      // get the position we ended the touch
      Vector2 touchEnd = touch.position;

      // calc
      float xDiff = touchEnd.x - touchStart.x;
      
      if (Mathf.Abs(xDiff) < swipeMinDistance)
      {
        return;
      }

      Vector3 moveDir;

      if (xDiff < 0)
      {
        moveDir = Vector3.left;
      }
      else
      {
        moveDir = Vector3.right;
      }

      RaycastHit hit;
      if (!rb.SweepTest(moveDir, out hit, swipeMove))
      {
        rb.MovePosition(rb.position + (moveDir * swipeMove));
      }
    }
    // check if touch was just ended

  }

  private float CalculateMovement (Vector3 pixelPosition)
  {
    // convert 

    var moveX = 0;

    //  // if right side is pressed
    if (pixelPosition.x < 0.5f)
    {
      Debug.Log("Hit left");
      moveX = -1;
    }

    // if left side is pressed
    if (pixelPosition.x > 0.5F)
    {
      Debug.Log("hit right");
      moveX = 1;
    }

    // update horizontal speed
    return moveX;
  }

  private void FixedUpdate()
  {
    //Apply the auto-moving and movement forces
    rb.AddForce(horizontalSpeed, 0, rollSpeed);
    forwardMovement = rb.velocity;

  }
}
