using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wait for certain amount of time 
/// this can happen when the NPC is Stunned 
/// Or NPC is stucked by some web may be ??
/// </summary>
public class AIDecisionWait : AIDecision
{
    public float waitTime;

    float currentTime = 0;

    public override void OnEnterState()
    {
        base.OnEnterState();
        currentTime = waitTime;
    }

    public override bool Decide()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            return false;
        }
        return true;
    }

    public override void OnExitState()
    {
        base.OnExitState();
        currentTime = waitTime;
    }
}
