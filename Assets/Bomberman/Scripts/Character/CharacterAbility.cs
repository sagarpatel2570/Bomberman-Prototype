using UnityEngine;
using System.Collections;

/// <summary>
/// this class is meant to be inherited 
/// based on different ability we have different class
/// Movement
/// attack
/// rotation
/// </summary>
[RequireComponent(typeof(Character))]
public abstract class CharacterAbility : MonoBehaviour
{
    protected Character character;

    public virtual void Initialization()
    {
        character = GetComponent<Character>();
    }


    public virtual void DeInit()
    {
    }

    protected virtual void HandleInput()
    {

    }

    public virtual void EarlyProcessAbility()
    {
        HandleInput();
    }

    public virtual void ProcessAbility()
    {

    }

    public virtual void LateProcessAbility()
    {

    }

    public virtual void Reset()
    {

    }

    protected virtual void OnEnable()
    {
       
    }

    protected virtual void OnDisable()
    {
        
    }
}