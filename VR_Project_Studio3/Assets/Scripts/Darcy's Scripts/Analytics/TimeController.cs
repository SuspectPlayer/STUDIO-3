using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover, based on a tutorial on YouTube called; "How to Make an In-Game Timer in Unity" by Turbo Makes Games.

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    TimeSpan timePlaying;
    bool mainTimerGoing, betweenTimerGoing;

    float mainElapsedTime, betweenElapsedTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mainTimerGoing = false;
        betweenTimerGoing = false;
    }

    public void StartMainTimer()
    {
        mainTimerGoing = true;
        mainElapsedTime = 0f;

        StartCoroutine(UpdateMainTimer());
    }

    public void StartBetweenTimer()
    {
        betweenTimerGoing = true;
        betweenElapsedTime = 0f;

        StartCoroutine(UpdateBetweenTimer());
    }

    public void EndMainTimer()
    {
        mainTimerGoing = false;
    }

    public void EndBetweenTimer()
    {
        betweenTimerGoing = false;
    }

    public void ResetBetweenTimer()
    {
        betweenElapsedTime = 0f;
    }

    public float GetTime(int whichTime)
    {
        if(whichTime == 0)
        {
            return mainElapsedTime;
        }
        else
        {
            return betweenElapsedTime;
        }
    }

    IEnumerator UpdateMainTimer()
    {
        while(mainTimerGoing)
        {
            mainElapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(mainElapsedTime);

            yield return null;
        }
    }

    IEnumerator UpdateBetweenTimer()
    {
        while(betweenTimerGoing)
        {
            betweenElapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(betweenElapsedTime);

            yield return null;
        }
    }
}
