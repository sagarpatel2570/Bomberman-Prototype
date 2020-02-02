using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wanders the NPC to one direction until wall or some obstacles is detected
/// </summary>
public class AIActionWander : AIAction
{
    CharacterWander characterWander;

    protected override void Initialization()
    {
        characterWander = GetComponent<CharacterWander>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        characterWander.DoWander(true);
    }

    public override void PerformAction()
    {
        
    }

    public override void OnExitState()
    {
        base.OnExitState();
        characterWander.DoWander(false);
    }
}
