using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks time of game
/// by: Natilie Eidt
/// </summary>
public class TimerController : MonoBehaviour
{
  [Tooltip("Time on current count")]
  public float timeCurrent;
  [Tooltip("Max time allowed")]
  public float timeMax = 30;
  [Tooltip("If timer is counting")]
  public bool countingDown;

  public event System.Action OnTimerFinished = delegate { };
  public event System.Action<float> OnTimeChanged = delegate { };

  private void Start()
  {
    SideBarController sb = FindObjectOfType<SideBarController>();
    sb.OnGamePause += PauseCountDown;
    sb.OnGameUnpaused += ResumeCountDown;

    GameController gc = FindObjectOfType<GameController>();
    gc.OnNewRoundStart += StartCountDown;
    gc.OnPopupSwapPlayer += PauseCountDown;
  }

  private void OnDestroy()
  {
    SideBarController sb = FindObjectOfType<SideBarController>();
    sb.OnGamePause -= PauseCountDown;
    sb.OnGameUnpaused -= ResumeCountDown;

    GameController gc = FindObjectOfType<GameController>();
    gc.OnPopupSwapPlayer -= PauseCountDown;
    gc.OnNewRoundStart -= StartCountDown;
  }

  /// <summary>
  /// Resets timer
  /// </summary>
  private void ResetTimer()
  {
    timeCurrent = timeMax;
  }

  /// <summary>
  /// Starts a new countdown
  /// </summary>
  public void StartCountDown()
  {
    Debug.Log("Starting count");
    ResetTimer();
    countingDown = true;
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
  /// </summary>
  private void CountDownTimer()
  {
    if (countingDown)
    {
      timeCurrent -= Time.deltaTime;
      OnTimeChanged(timeCurrent);

      if (timeCurrent <= 0)
        OnTimerFinished();
    }
  }

  /// <summary>
  /// Pauses a countdown
  /// </summary>
  public void PauseCountDown()
  {
    Debug.Log("End Count");
    countingDown = false;
  }

  /// <summary>
  /// Resumes a countdown
  /// </summary>
  public void ResumeCountDown()
  {
    Debug.Log("Starting count");
    countingDown = true;
  }
}
