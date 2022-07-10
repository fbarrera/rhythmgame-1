using FrancisForte.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Attributes

    [SerializeField] private int score, accumulatedScore;
    [SerializeField] bool isGainTriggered, isAccumulatedGainTriggered, isSetTriggered;
    [SerializeField] private List<SerializedEvent> scoreEvents;

    #endregion

    #region Unity Events

    void Start()
    {
    }

    void Update()
    {
        ScoreEvents();
    }

    #endregion

    #region Utility Methods

    public void ResetAll()
    {
        score = 0;
        SetGainTriggered();
        SetAccumulatedGainTriggered();
    }

    #endregion

    #region Logic Data Retriever

    public int GetScore()
    {
        return score;
    }

    public int GetAccumulatedScore()
    {
        return accumulatedScore;
    }

    public bool GetGainTriggered()
    {
        return isGainTriggered;
    }

    public bool GetAccumulatedGainTriggered()
    {
        return isAccumulatedGainTriggered;
    }

    public bool GetSetTriggered()
    {
        return isSetTriggered;
    }

    #endregion

    #region Logic Data Setter

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void SetAccumulatedScore(int newScore)
    {
        accumulatedScore = newScore;
    }

    public void AddScore(int newScoreToAdd)
    {
        score += newScoreToAdd;
    }

    public void AddAccumulatedScoreRaw(int newScoreToAdd)
    {
        accumulatedScore += newScoreToAdd;
    }

    public void AddAccumulatedScore()
    {
        accumulatedScore += score;
    }

    public void SetGainTriggered()
    {
        isGainTriggered = false;
    }

    public void SetAccumulatedGainTriggered()
    {
        isAccumulatedGainTriggered = false;
    }

    public void SetSetTriggered()
    {
        isSetTriggered = false;
    }

    #endregion

    #region Main Process Triggers

    public void TriggerGain()
    {
        isGainTriggered = true;
    }

    public void TriggerAccumulatedGain()
    {
        isAccumulatedGainTriggered = true;
    }

    public void TriggerSet()
    {
        isSetTriggered = true;
    }

    #endregion

    #region Event Methods

    public void OnSet()
    {
        Debug.Log("Score set to 0!");
    }

    public void OnGain()
    {
        Debug.Log("Score gained!");
    }

    public void OnAccumulatedGain()
    {
        Debug.Log("Accumulated score gained!");
    }

    #endregion

    #region Event Collector

    public void ScoreEvents()
    {
        foreach (SerializedEvent item in scoreEvents)
        {
            switch (item.eventName)
            {
                case "Set":
                    if (GetSetTriggered())
                    {
                        OnSet();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "Gain":
                    if (GetGainTriggered())
                    {
                        OnGain();
                        item.eventInstance?.Invoke();
                    }
                    break;
                case "AccumulatedGain":
                    if (GetAccumulatedGainTriggered())
                    {
                        OnAccumulatedGain();
                        item.eventInstance?.Invoke();
                    }
                    break;
            }
        }
    }

    #endregion

}
