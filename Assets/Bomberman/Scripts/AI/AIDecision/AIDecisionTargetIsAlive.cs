using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the Target is dead the NPC changes its state
/// </summary>
public class AIDecisionTargetIsAlive : AIDecision
{
    protected Character character;

    public override bool Decide()
    {
        return CheckIfTargetIsAlive();
    }

    protected virtual bool CheckIfTargetIsAlive()
    {
        if (brain.target == null)
        {
            return false;
        }

        character = brain.target.gameObject.GetComponent<Character>();
        if (character != null)
        {
            if (character.state == CharacterStates.DEATH)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
