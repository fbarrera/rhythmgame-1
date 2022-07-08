using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerManager))]
public class GameManager : MonoBehaviour
{

    [SerializeField] TimerManager timerManager;

    private void Start()
    {
        timerManager.TriggerStart();
        timerManager.SetTimeLimit(5);
        StartCoroutine(PauseAt(3));
    }

    IEnumerator PauseAt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timerManager.TriggerPause();
    }

}
