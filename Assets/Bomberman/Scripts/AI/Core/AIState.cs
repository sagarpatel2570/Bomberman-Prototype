using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// State consists of list of actions and transisitions
/// </summary>
[System.Serializable]
public class AIState 
{
    public string StateName;

    public List<AIAction> Actions;
    public List<AITransition> Transitions;


    protected AIBrain brain;

    public virtual void SetBrain(AIBrain brain)
    {
        this.brain = brain;
    }
                		
    public virtual void UpdateState()
    {
        PerformActions();
        EvaluateTransitions();
    }

    public virtual void EnterState()
    {
        foreach (AIAction action in Actions)
        {
            action.OnEnterState();
        }
        foreach (AITransition transition in Transitions)
        {
            if (transition.Decision != null)
            {
                transition.OnEnterState();
            }
        }
    }

    public virtual void ExitState()
    {
        foreach (AIAction action in Actions)
        {
            action.OnExitState();
        }
        foreach (AITransition transition in Transitions)
        {
            if (transition.Decision != null)
            {
                transition.OnExitState();
            }
        }
    }

    protected virtual void PerformActions()
    {
        if (Actions.Count == 0) { return; }
        for (int i=0; i<Actions.Count; i++) 
        {
            if (Actions[i] != null)
            {
                Actions[i].PerformAction();
            }
            else
            {
                Debug.LogError("An action in " + brain.gameObject.name + " is null.");
            }
        }
    }
        
    protected virtual void EvaluateTransitions()
    {
        if (Transitions.Count == 0) { return; }
        for (int i = 0; i < Transitions.Count; i++) 
        {
            if (Transitions[i].Decision != null)
            {
                if (Transitions[i].Decide())
                {
                    if (Transitions[i].TrueState != "")
                    {
                        brain.TransitionToState(Transitions[i].TrueState);
                        return;
                    }
                }
                else
                {
                    if (Transitions[i].FalseState != "")
                    {
                        brain.TransitionToState(Transitions[i].FalseState);
                        return;
                    }
                }
            }                
        }
    }        
}

