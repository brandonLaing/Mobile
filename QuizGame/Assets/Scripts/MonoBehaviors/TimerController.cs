using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks time of game
/// by: Natilie Eidt
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

  private void Start()
  {
    SideBarController sb = FindObjectOfType<SideBarController>();
    sb.OnGamePause += PauseCountDown;
    sb.OnGameUnpaused += ResumeCountDown;

    GameController gc = FindObjectOfType<GameController>();
    gc.OnBothAnswersReceived += PauseCountDown;
    gc.OnNewRoundStart += StartCountDown;
  }

  private void OnDestroy()
  {
    SideBarController sb = FindObjectOfType<SideBarController>();
    sb.OnGamePause -= PauseCountDown;
    sb.OnGameUnpaused -= ResumeCountDown;

    GameController gc = FindObjectOfType<GameController>();
    gc.OnBothAnswersReceived -= PauseCountDown;
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
  /// </summary>
  private void CountDownTimer()
  {
    if (isCountingDown)
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
    isCountingDown = false;
  }

  public void PauseCountDown(int int1, int int2, int int3)
  {
    PauseCountDown();
  }

  /// <summary>
  /// Resumes a countdown
  /// </summary>
  public void ResumeCountDown()
  {
    isCountingDown = true;
  }
}
