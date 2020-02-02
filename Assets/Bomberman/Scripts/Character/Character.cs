using UnityEngine;
using System;

// character consisit the list of all the abilities and update s them every update
[SelectionBase]
public class Character : MonoBehaviour,IDeath
{
    public event Action<Character> OnDeath;
    public bool isPlayer;
    public CharacterStates state;

    public Animator Animator { get; protected set; }
    public Controller2D Controller { get; protected set; }
    public CharacterMovement CharacterMovement { get; protected set; }
    public Transform model;

    CharacterAbility[] characterAbilities;

    void Awake()
    {
        Controller = GetComponent<Controller2D>();
    }

    void OnEnable()
    {
        Initialization();
    }

    void OnDisable()
    {
        // basically is is the ondisable for all abilities 
        foreach (CharacterAbility ability in characterAbilities)
        {
            ability.DeInit();
        }
    }

    void Initialization()
    {
        characterAbilities = GetComponents<CharacterAbility>();
        CharacterMovement = GetComponent<CharacterMovement>();
        Animator = GetComponentInChildren<Animator>();

        // basically is is the onenable for all abilities 
        foreach (CharacterAbility ability in characterAbilities)
        {
            ability.Initialization();
        }
    }

    /// <summary>
    /// Every update we update the abilities in three parts
    /// "EarlyProcessAbilities" were we handle the input
    /// "ProcessAbilities" were we do the actions
    /// "LateProcessAbilities" thind we need to do after all the abilites of action is been done
    /// </summary>
    void Update()
    {
        EarlyProcessAbilities();
        ProcessAbilities();
        LateProcessAbilities();
    }

    void EarlyProcessAbilities()
    {
        foreach (CharacterAbility ability in characterAbilities)
        {
            ability.EarlyProcessAbility();
        }
    }

    void ProcessAbilities()
    {
        foreach (CharacterAbility ability in characterAbilities)
        {
            ability.ProcessAbility();
        }
    }

    void LateProcessAbilities()
    {
        foreach (CharacterAbility ability in characterAbilities)
        {
            ability.LateProcessAbility();
        }
    }

    /// <summary>
    /// we update the rotation of model based on the current direction of player
    /// </summary>
    void LateUpdate()
    {
        if (model != null)
        {
            model.transform.localEulerAngles = -Vector3.forward * transform.localEulerAngles.z;
        }
    }

    public void Reset()
    {
        if (characterAbilities == null)
        {
            return;
        }
        
        foreach (CharacterAbility ability in characterAbilities)
        {
            if (ability.enabled)
            {
                ability.Reset();
            }
        }
    }

    /// <summary>
    /// when the state is changed we update the animation
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState (CharacterStates state)
    {
        if(this.state == state)
        {
            return;
        }
        CharacterStates previousState = this.state;
        this.state = state;
        UpdateAnimator(state, previousState);
    }

    void UpdateAnimator(CharacterStates state,CharacterStates previousState)
    {
        if(Animator == null)
        {
            return;
        }

        // Reset Last Trigger
        switch (state)
        {
            case CharacterStates.IDLE:
                Animator.ResetTrigger("Idle");
                break;
            case CharacterStates.WALK:
                Animator.ResetTrigger("Walk");
                break;
            case CharacterStates.ATTACK:
                break;
            case CharacterStates.DEATH:
                break;
        }

        // set trigger
        switch (state)
        {
            case CharacterStates.IDLE:
                Animator.SetTrigger("Idle");
                break;
            case CharacterStates.WALK:
                Animator.SetTrigger("Walk");
                break;
            case CharacterStates.ATTACK:
                break;
            case CharacterStates.DEATH:
                break;
        }
    }

    public void Destroy()
    {
        OnDeath?.Invoke(this);
    }
}

public enum CharacterStates
{
    IDLE = 0,
    WALK = 1,
    ATTACK = 2,
    DEATH = 3
}