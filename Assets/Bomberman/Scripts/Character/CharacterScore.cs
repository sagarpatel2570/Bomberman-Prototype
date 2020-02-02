using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this class to an NPC so that it can contribute to scores on death
/// </summary>
public class CharacterScore : MonoBehaviour, IGiveScore
{
    public int pointsGiven;

    public void GiveScore()
    {
        ScoreSystem.AddScore(pointsGiven);
    }
}
