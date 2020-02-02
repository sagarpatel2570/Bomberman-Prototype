using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    /// <summary>
    /// label is required so that we can identify for what purpose this decision is for 
    /// for eg :: AIDecisionWait can be use for two purpose 
    /// 1) when NPC is stunned
    /// 2) when NPC is Stucked in mud
    /// we can itendity the same script used for different purpose with the help of label
    /// </summary>
    public string label;
    public abstract bool Decide();

    public bool DecisionInProgress { get; set; }
    protected AIBrain brain;

    protected virtual void Start()
    {
        brain = this.gameObject.GetComponent<AIBrain>();
        Initialization();
    }

    public virtual void Initialization()
    {

    }

    public virtual void OnEnterState()
    {
        DecisionInProgress = true;
    }

    public virtual void OnExitState()
    {
        DecisionInProgress = false;
    }

    public virtual void OnDestroy()
    {
        DeInit();
    }

    public virtual void DeInit()
    {

    }
}
