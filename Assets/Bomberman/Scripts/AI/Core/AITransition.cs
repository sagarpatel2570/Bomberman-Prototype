using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITransition 
{
    public List<AIDecision> Decision;
    public string TrueState;
    public string FalseState;

    public bool Decide ()
    {
        foreach (AIDecision decision in Decision)
        {
            if(!decision.Decide())
            {
                return false;
            }
        }
        return true;
    }

    public void OnExitState ()
    {
        foreach (AIDecision decision in Decision)
        {
            decision.OnExitState();
        }
    }

    public void OnEnterState ()
    {
        foreach (AIDecision decision in Decision)
        {
            decision.OnEnterState();
        }
    }
}

