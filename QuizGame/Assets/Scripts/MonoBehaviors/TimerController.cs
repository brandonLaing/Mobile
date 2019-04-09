using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks time of game
/// by: Natilie
/// </summary>
public class TimerController : MonoBehaviour
{
  [SerializeField]
  [Tooltip("Time on current count")]
  private float timeCurrent;
  [SerializeField]
  [Tooltip("Max time allowed")]
  private float timeMax = 30;
  [SerializeField]
  [Tooltip("If timer is counting")]
  private bool isCountingDown;

  public event System.Action OnTimerFinished = delegate { };
  public event System.Action<float> OnTimeChanged = delegate { };

  /// <summary>
  /// Resets timer
  /// </summary>
  private void ResetTimer()
  {
    timeCurrent = 0;
  }

  /// <summary>
  /// Starts a new countdown
  /// </summary>
  public void StartCountDown()
  {
    ResetTimer();
    isCountingDown = true;
  }

  /// <summary>
  /// Calls countdown timer
  /// </summary>
  private void Update()
  {
    CountDownTimer();
  }

  /// <summary>
  /// Increments time and sends on time changed call. If timer is done calls on timer finished
  /// By: Brandon Laing
  /// </summary>
  private void CountDownTimer()
  {
    timeCurrent += Time.deltaTime;
    OnTimeChanged(timeCurrent);

    if (timeCurrent >= timeMax)
      OnTimerFinished();
  }

  /// <summary>
  /// Pauses a countdown
  /// </summary>
  public void PauseCountDown()
  {
    isCountingDown = false;
  }

  /// <summary>
  /// Resumes a countdown
  /// </summary>
  public void ResumeCountDown()
  {
    isCountingDown = true;
  }
}
