using FrancisForte.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{

    #region Attributes

    [SerializeField] private float time, timeLimit; 
    [SerializeField] bool isStarted, isStartTriggered, isPaused, isPauseTriggered, isEnded, isRestartTriggered;
    [SerializeField] private List<SerializedEvent> timerEvents;

    #endregion

    #region Unity Events

    void Start()
    {
        ResetAll();
    }

    void Update()
    {
        TimerEvents();
        TimeProcessor();
    }

    #endregion

    #region Utility Methods

    void TimeProcessor()
    {
        if (isStarted)
        {
            if (isPaused)
                return;
            if (isEnded)
                return;
            time += Time.deltaTime;
        }
    }
    
    void ResetAll(){
        time = 0;
        isStarted = false;
        isPaused = false;
        isEnded = false;
        isPauseTriggered = false;
        isStartTriggered = false;
    }

    #endregion

    #region Logic Data Retriever

    public float GetTime(){
        return time;
    }

    public float GetTimeLimit(){
        return timeLimit;
    }

    public bool GetStarted(){
        return isStarted;
    }

    public bool GetPaused(){
        return isPaused;
    }

    public bool GetEnded(){
        return isEnded;
    }

    public bool GetStartTriggered()
    {
        return isStartTriggered;
    }

    public bool GetPauseTriggered(){
        return isPauseTriggered;
    }

    public bool GetContinueTriggered(){
        return isPauseTriggered;
    }

    public bool GetRestartTriggered()
    {
        return isRestartTriggered;
    }

    #endregion

    #region LogicDataSetter

    public void SetTimeLimit(float newTimeLimit){
        timeLimit = newTimeLimit;
    }

    public void SetStarted()
    {
        isStarted = true;
        time = 0;
    }
    
    public void SetEnded(bool newState)
    {
        isEnded = newState;
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

    #endregion

    #region MainProcessTriggers

    public void TriggerStart()
    {
        isStartTriggered = true;
    }

    public void TriggerPause()
    {
        isPauseTriggered = true;
    }

    #endregion

    #region Event Methods

    public void OnStarted()
    {
        Debug.Log("Started!");
    }

    public void OnContinued()
    {
        Debug.Log("Continued!");
    }

    public void OnPaused()
    {
        Debug.Log("Paused!");
    }

    public void OnEnded()
    {
        Debug.Log("Time is up!");
    }

    public void OnRestarted()
    {
        Debug.Log("Let's try again!");
    }

    #endregion

    #region Event Collector

    public void TimerEvents() {
        foreach(SerializedEvent item in timerEvents)
        {
            switch (item.eventName)
            {
                case "Start":
                    if (GetTime() == 0 && GetStartTriggered() && !GetStarted())
                    {
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Time Up":
                    if (GetTime() > GetTimeLimit() && GetStarted() && !GetEnded())
                    {
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Pause":
                    if (GetPauseTriggered() && !GetPaused() && !GetEnded())
                    {
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Continue":
                    if (GetContinueTriggered() && GetPaused() && !GetEnded())
                    {
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Restart":
                    if (GetRestartTriggered())
                    {
                        item.eventInstance?.Invoke();
                    }
                    break;
            }
        }   
    }

    #endregion
}
