using FrancisForte.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    #region Attributes

    [SerializeField] private float time, timeLimit;
    [SerializeField] bool isStarted, isStartTriggered, isPaused, isPauseTriggered, isTimeUp, isTimeUpTriggered, isRestartTriggered;
    [SerializeField] private List<SerializedEvent> timerEvents;

    #endregion

    #region Unity Events

    void Update()
    {
        TimerEvents();
        TimeProcessor();
    }

    #endregion

    #region Utility Methods
    Func<bool> GetTimeUntil(float seconds)
    {
        return () => GetTime() >= seconds;
    }

    void TimeProcessor()
    {
        if (isStarted)
        {
            if (isPaused)
                return;
            if (isTimeUp)
                return;
            time += Time.deltaTime;
        }
    }

    public void ResetAll()
    {
        time = 0;
        isStarted = false;
        isPaused = false;
        isTimeUp = false;
        isPauseTriggered = false;
        isStartTriggered = false;
    }

    #endregion

    #region Logic Data Retriever

    public float GetTime()
    {
        return time;
    }

    public float GetTimeLimit()
    {
        return timeLimit;
    }

    public bool GetStarted()
    {
        return isStarted;
    }

    public bool GetPaused()
    {
        return isPaused;
    }

    public bool GetTimeUp()
    {
        return isTimeUp;
    }

    public bool GetStartTriggered()
    {
        return isStartTriggered;
    }

    public bool GetPauseTriggered()
    {
        return isPauseTriggered;
    }

    public bool GetContinueTriggered()
    {
        return isPauseTriggered;
    }

    public bool GetRestartTriggered()
    {
        return isRestartTriggered;
    }

    public bool GetTimeUpTriggered()
    {
        return isTimeUpTriggered;
    }

    #endregion

    #region Logic Data Setter

    public void SetTimeLimit(float newTimeLimit)
    {
        timeLimit = newTimeLimit;
    }

    public void SetStarted()
    {
        isStarted = true;
        time = 0;
    }

    public void SetTimeUp(bool newState)
    {
        isTimeUp = newState;
    }

    public void SetPaused(bool newState)
    {
        isPaused = newState;
    }

    public void SetPauseTriggered()
    {
        SetPaused(true);
        isPauseTriggered = false;
    }

    public void SetContinueTriggered()
    {
        SetPaused(false);
        isPauseTriggered = false;
    }

    public void SetRestartTriggered()
    {
        ResetAll();
        isRestartTriggered = false;
        isStartTriggered = true;
    }

    public void SetStartTriggered()
    {
        isStartTriggered = false;
    }

    public void SetTimeUpTriggered()
    {
        SetTimeUp(true);
        isTimeUpTriggered = false;
    }

    #endregion

    #region Main Process Triggers

    public void TriggerStart()
    {
        isStartTriggered = true;
    }

    public void TriggerPause()
    {
        isPauseTriggered = true;
    }

    public void TriggerRestart()
    {
        isRestartTriggered = true;
    }

    public void TriggerTimeUp()
    {
        isTimeUpTriggered = true;
    }

    #endregion

    #region Event Methods

    public void OnStart()
    {
        Debug.Log("Started!");
    }

    public void OnContinue()
    {
        Debug.Log("Continued!");
    }

    public void OnPause()
    {
        Debug.Log("Paused!");
    }

    public void OnTimeUp()
    {
        Debug.Log("Time is up!");
    }

    public void OnRestart()
    {
        Debug.Log("Let's try again!");
    }

    #endregion

    #region Event Collector

    public void TimerEvents()
    {
        foreach (SerializedEvent item in timerEvents)
        {
            switch (item.eventName)
            {
                case "Start":
                    if (GetTime() == 0 && GetStartTriggered() && !GetStarted())
                    {
                        OnStart();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Time Up":
                    if ((GetTime() > GetTimeLimit() && GetStarted() && !GetTimeUp()) || GetTimeUpTriggered())
                    {
                        OnTimeUp();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Pause":
                    if (GetPauseTriggered() && !GetPaused() && !GetTimeUp())
                    {
                        OnPause();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Continue":
                    if (GetContinueTriggered() && GetPaused() && !GetTimeUp())
                    {
                        OnContinue();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Restart":
                    if (GetRestartTriggered())
                    {
                        OnRestart();
                        item.eventInstance?.Invoke();
                    }
                    break;
            }
        }
    }

    #endregion

    #region Corroutine Methods

    public IEnumerator PauseAt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TriggerPause();
    }

    public IEnumerator ExecuteAfter(float seconds, Action callback)
    {
        yield return new WaitUntil(GetTimeUntil(seconds));
        callback?.Invoke();
    }

    public IEnumerator ExecuteAfter(float seconds, List<Action> Callbacks)
    {
        yield return new WaitUntil(GetTimeUntil(seconds));
        foreach (Action callback in Callbacks)
        {
            callback?.Invoke();
        }
    }

    #endregion
}
