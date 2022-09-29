using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void TimeManaging(float time){
        StartCoroutine("TimeStopCoroutine", time);
    }   

    IEnumerator TimeStopCoroutine(float time){
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
