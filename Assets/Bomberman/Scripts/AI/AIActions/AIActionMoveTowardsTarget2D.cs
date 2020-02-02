using UnityEngine;

/// <summary>
/// Moves thee NPC towards the target based on Astar Pathfinding algorithm
/// </summary>
public class AIActionMoveTowardsTarget2D : AIAction
{
    public float updateTargetTime = 0.3f;

    CharacterPathfinding characterPathfinding;
    float currentTime;

    /// <summary>
    /// On init we grab our CharacterMovement ability
    /// </summary>
    protected override void Initialization()
    {
        characterPathfinding = GetComponent<CharacterPathfinding>();
    }

    /// <summary>
    /// On PerformAction we move
    /// </summary>
    public override void PerformAction()
    {
        Move();
    }

    /// <summary>
    /// Moves the character towards the target if needed
    /// </summary>
    protected virtual void Move()
    {
        if (brain.target == null)
        {
            return;
        }

        if (currentTime >= updateTargetTime)
        {
            currentTime = 0;
            characterPathfinding.SetDestination(brain.target.position);
        }else
        {
            currentTime += Time.deltaTime;
        }
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        currentTime = updateTargetTime;
    }

    /// <summary>
    /// On exit state we stop our movement
    /// </summary>
    public override void OnExitState()
    {
        base.OnExitState();
        characterPathfinding.ResetPathfinding();
    }
}
