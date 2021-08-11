using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover, based on a tutorial on YouTube called; "How to Make an In-Game Timer in Unity" by Turbo Makes Games.

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    TimeSpan timePlaying;
    bool timerGoing;

    public float elaspedTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
        elaspedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elaspedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elaspedTime);

            yield return null;
        }
    }
}
