using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnComplete));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnComplete = null)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        OnComplete?.Invoke();
    }
}
