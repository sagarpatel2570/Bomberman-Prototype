using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int currentScore;
    int targetScore;

    private void OnEnable()
    {
        ScoreSystem.RegisterOnScoreChange(OnScoreChanged, true);
    }

    private void OnDisable()
    {
        ScoreSystem.UnRegisterOnScoreChange(OnScoreChanged);
    }

    void OnScoreChanged (int finalScore)
    {
        targetScore = finalScore;
        
        DOTween.To(() => currentScore, a => currentScore = a, targetScore, 0.5f).
            OnUpdate(() => 
            {
                scoreText.text = currentScore.ToString();
            });
        transform.DOPunchScale(Vector3.one * 0.2f, 0.2f).OnComplete(()=>
        {
            transform.localScale = Vector3.one;
        });
    }
}
