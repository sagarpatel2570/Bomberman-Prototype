using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BRAIN is the heat of AI which updates the current STATE every frame
// STATE then sequencially update its ACTION And TRANSISITION
// ACTION can be wandering ,movingtowards target
// TRANSISITION consist the list of decisions which has to be made while doing action 
// DECISIONS can be , if the target is in radius, or simply wait when he is stunned
// Based on the decision new BRAIN changes its STATE


public class AIBrain : MonoBehaviour
{
    public List<AIState> states;
    public bool brainActive = true;
    public float timeInThisState;
    public Transform target;

    public AIState CurrentState { get; protected set; }
    public Transform PreviousTarget { get; protected set; }
    public Character Character { get; protected set; }

    AIDecision[] decisions;

    protected virtual void Awake()
    {
        foreach (AIState state in states)
        {
            state.SetBrain(this);
        }
        decisions = this.gameObject.GetComponents<AIDecision>();
        Character = GetComponent<Character>();
    }


    void OnDeath ()
    {
        brainActive = false;
    }

    void Start()
    {
        if (states.Count > 0)
        {
            CurrentState = states[0];
        }            
    }

    void Update()
    {
        if (!brainActive || CurrentState == null)
        {
            return;
        }

        CurrentState.UpdateState();
        timeInThisState += Time.deltaTime;
    }

    public void TransitionToState(string newStateName)
    {
        if (newStateName != CurrentState.StateName)
        {
            CurrentState.ExitState();
            OnExitState();

            CurrentState = FindState(newStateName);
            if (CurrentState != null)
            {
                CurrentState.EnterState();
            }                
        }
    }

    void OnExitState()
    {
        timeInThisState = 0f;
    }

    void InitializeDecisions()
    {
        foreach(AIDecision decision in decisions)
        {
            decision.Initialization();
        }
    }

    AIState FindState(string stateName)
    {
        foreach (AIState state in states)
        {
            if (state.StateName == stateName)
            {
                return state;
            }
        }
        return null;
    }

    public void SetTarget(Transform targetTransform)
    {
        if (target != null)
        {
            PreviousTarget = target;
        }
        target = targetTransform;
    }
}

