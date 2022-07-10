
using System;
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

        StartCoroutine(
            timerManager.ExecuteAfter(
                2.5f,
                new List<Action>
                {
                    () => scoreManager.AddScore(5),
                    () => scoreManager.TriggerGain()
                }
                )
            );
        StartCoroutine(
            timerManager.ExecuteAfter(
                4f,
                new List<Action>
                {
                    () => scoreManager.AddScore(5),
                    () => scoreManager.TriggerGain()
                }
                )
            );
        StartCoroutine(
            timerManager.ExecuteAfter(
                8f,
                new List<Action>
                {
                    () => scoreManager.AddScore(7),
                    () => scoreManager.TriggerGain()
                }
                )
            );

        timerManager.ResetAll();
        timerManager.SetTimeLimit(10);
        timerManager.TriggerStart();
        StartCoroutine(
            timerManager.PauseAt(3)
            );
    }

}
