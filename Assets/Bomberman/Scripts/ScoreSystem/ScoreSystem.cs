using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// keep track of score and trigers an event when score is changed 
public class ScoreSystem : MonoBehaviour
{
    static event Action<int> OnScoreChanged;
    static int score;

    private void OnEnable()
    {
        score = 0;
    }

    public static void AddScore (int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public static void RegisterOnScoreChange(Action<int> callback,bool triggerCallback = false)
    {
        OnScoreChanged += callback;
        if(triggerCallback)
        {
            OnScoreChanged(score);
        }
    }

    public static void UnRegisterOnScoreChange(Action<int> callback)
    {
        OnScoreChanged -= callback;
    }
}
