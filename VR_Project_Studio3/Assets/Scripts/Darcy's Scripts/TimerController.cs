using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover
//Using a YouTube tutorial called "How to Make an In-Game Timer in Unity - Beginner Tutorial" by 'Turbo Makes Games'.

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elaspedTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timerGoing = false;
    }

    public void BeginTimer()
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
