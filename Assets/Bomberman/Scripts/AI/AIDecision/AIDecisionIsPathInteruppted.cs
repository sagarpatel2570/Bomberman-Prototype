using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// While moving towards target if the path is interupped because of bomb or may be some moving obstacle the NPC will change its state
/// </summary>
public class AIDecisionIsPathInteruppted : AIDecision
{
    CharacterPathfinding characterPathFinding;

    public override void Initialization()
    {
        base.Initialization();
        characterPathFinding = GetComponent<CharacterPathfinding>();
    }
    public override bool Decide()
    {
        return characterPathFinding.isPathInterupted;
    }
}
