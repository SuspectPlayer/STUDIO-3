using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DarcyAnalyticMethods : MonoBehaviour
{
    public void PuzzleOneCompletion(float time, string names, int level)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 1 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names }
                        });

                    Debug.Log("Puzzle 1 Level 1 analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 1 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names }
                        });

                    Debug.Log("Puzzle 1 Level 3 analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleOneAttempt(float attemptTime, float timeInBetweenAttempts, string names, int level)
    {
        switch(level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 1 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 1, Level 1 Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 1 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 1, Level 3 Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleOneAttemptsMade(int attemptsMade, int level, string names)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 1 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 1, Level 1 attempts made Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 1 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 1, Level 3 attempts made Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleTwoCompletion(float time, string names, string location, int level)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 2 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names },
                            { "Location", location }
                        });

                    Debug.Log("Puzzle 2 Level 1 analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 2 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names },
                            { "Location", location }
                        });

                    Debug.Log("Puzzle 2 Level 3 analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleTwoAttempt(float attemptTime, float timeInBetweenAttempts, string names, int level)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 2 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 2, Level 1 Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 2 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 2, Level 3 Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleTwoAttemptsMade(int attemptsMade, int level, string names)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 2 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 2, Level 1 attempts made Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 2 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 2, Level 3 attempts made Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleThreeCompletion(float time, string names, int level)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 3 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names }
                        });

                    Debug.Log("Puzzle 3 Level 1 analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 3 Completion Time",
                        new Dictionary<string, object>
                        {
                            { "Completion Time", time },
                            { "Players", names }
                        });

                    Debug.Log("Puzzle 3 Level 3 analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleThreeAttempt(float attemptTime, float timeInBetweenAttempts, string names, int level)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 3 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 3, Level 1 Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 3 Attempt Times",
                    new Dictionary<string, object>
                    {
                        { "Time elapsed at fail", attemptTime },
                        { "Time in between attempts", timeInBetweenAttempts },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 3, Level 3 Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void PuzzleThreeAttemptsMade(int attemptsMade, int level, string names)
    {
        switch (level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1, Puzzle 3 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 3, Level 1 attempts made Analytics result: " + analyticsResult);
                    break;
                }
            case 3:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 3, Puzzle 3 Attempts Total",
                    new Dictionary<string, object>
                    {
                        { "Attempts Made", attemptsMade },
                        { "Players", names }
                    });
                    Debug.Log("Puzzle 3, Level 3 attempts made Analytics result: " + analyticsResult);
                    break;
                }
        }
    }

    public void LightsTurnedOn(int amountUsed, string lightName, string playerNames, int level)
    {
        switch(level)
        {
            case 1:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Substation 1 Light Usage",
                    new Dictionary<string, object>
                    {
                         { lightName, amountUsed },
                         { "Players", playerNames}
                    });
                    Debug.Log("Light Usage level 1 analytics result: " + analyticsResult);
                }
                break;
            case 2:
                {
                    AnalyticsResult analyticsResult = Analytics.CustomEvent("Abyss Light Usage",
                       new Dictionary<string, object>
                       {
                         { lightName, amountUsed },
                         { "Players", playerNames}
                       });
                    Debug.Log("Light Usage in abyss analytics result: " + analyticsResult);
                    break;
                }
        }
    }
}
