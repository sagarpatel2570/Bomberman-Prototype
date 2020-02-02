using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    /// <summary>
    /// label is required so that we can identify for what purpose this action is for 
    /// for eg :: AIActionAtack can be use for two purpose 
    /// 1) when NPC is doing melee attack
    /// 2) when NPC is doing range attack
    /// we can itendity the same script used for different purpose with the help of label
    /// </summary>
    public string label;
    public abstract void PerformAction();
    public bool ActionInProgress { get; set; }
    protected AIBrain brain;

    protected virtual void Start()
    {
        brain = this.gameObject.GetComponent<AIBrain>();
        Initialization();
    }

    protected virtual void Initialization()
    {

    }

    public virtual void OnEnterState()
    {
        ActionInProgress = true;
    }

    public virtual void OnExitState()
    {
        ActionInProgress = false;
    }
}
