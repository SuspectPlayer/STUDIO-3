using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DarcyAnalyticMethods : MonoBehaviour
{
    public void PuzzleOneCompletion(float time)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Puzzle 1 Completion Time" + time);
        Debug.Log("Analytics result from regular: " + analyticsResult);
    }

    public void PuzzleOneCompletionTest(string message)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Puzzle 1 Completion Theory Test" + message);
        Debug.Log("Analytics result from test: " + analyticsResult);
    }

    public void PuzzleTwoCompletion(float time)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Puzzle 2 Completion Time" + time);
        Debug.Log("Analytics result: " + analyticsResult);
    }
}
