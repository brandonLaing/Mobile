using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float timeCurrent;
    private float timeMax;
    private bool isCountingDown;

    public event System.Action OnTimerFinished = delegate { };
    public event System.Action<float> OnTimeChanged = delegate { };

    public void ResetTimer()
    {
        timeCurrent = 0;
    }
    public void StartCountDown()
    {
        //resetTimer
        ResetTimer();
        isCountingDown = true;
    }
    public void CountDownTimer()
    {
        // OnTimeChanged(currentTime)
        // OnTimerFinished()

        //increment timer
        timeCurrent -= Time.deltaTime;
    }

    private void Update()
    {
        // CountDownTimer()
        CountDownTimer();
    }

    public void PauseCountDown()
    {
        isCountingDown = false;
    }
    public void ResumeCountDown()
    {
        isCountingDown = true;
    }
}
