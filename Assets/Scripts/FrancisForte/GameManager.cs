using System;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] TimerManager timerManager;
    [SerializeField] ScoreManager scoreManager;

    private void Start()
    {

        scoreManager.ResetAll();
        scoreManager.TriggerSet();
        scoreManager.SetAccumulatedScore(50);
        StartCoroutine(ScoreAt(2.5f, 5));
        StartCoroutine(ScoreAt(4f, 5));
        StartCoroutine(ScoreAt(8f, 7));

        timerManager.ResetAll();
        timerManager.SetTimeLimit(10);
        timerManager.TriggerStart();
        StartCoroutine(PauseAt(3));
    }

    IEnumerator PauseAt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timerManager.TriggerPause();
    }

    Func<bool> GetTime(float seconds)
    {
        return () => timerManager.GetTime() >= seconds;
    }

    IEnumerator ScoreAt(float seconds, int newScore)
    {
        yield return new WaitUntil(GetTime(seconds));
        scoreManager.AddScore(newScore);
        scoreManager.TriggerGain();
    }

}
