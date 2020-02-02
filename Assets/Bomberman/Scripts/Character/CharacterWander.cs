using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached this component to an NPC so that it can wander in one direction until it hits the obstacles
/// changes it direction randomly when it hits an obstacle
/// </summary>
public class CharacterWander : CharacterAbility
{
    public float speed = 1;
    public float offset;
    public LayerMask obstacleLayermask;

    public Vector2 currentDirection;

    List<Vector2> directions = new List<Vector2>();
    bool canWander = true;
    RaycastHit2D hit;
    Vector3 previousPos;

    public override void Initialization()
    {
        base.Initialization();

        directions.Add(Vector2.right);
        directions.Add(Vector2.left);
        directions.Add(Vector2.up);
        directions.Add(Vector2.down);

        int randomDirIndex = Random.Range(0, directions.Count);
        currentDirection = directions[randomDirIndex];
    }

    public override void ProcessAbility()
    {
        if (canWander)
        {
            character.ChangeState(CharacterStates.WALK);
            float dst = speed * Time.deltaTime;
            hit = Physics2D.Raycast(transform.position, currentDirection, offset + dst, obstacleLayermask);

            if (hit)
            {
                transform.position += (Vector3)currentDirection * (hit.distance - offset);
                int randomDirIndex = Random.Range(0, directions.Count);
                currentDirection = directions[randomDirIndex];
            }
            else
            {
                transform.position += (Vector3)currentDirection * dst;
            }
            transform.right = (transform.position - previousPos).normalized;
            // I don't know why the y axis is changing when direction is left but since it is changing this is quick fix for now
            // FIXME !!
            if(transform.localEulerAngles.y != 0)
            {
                transform.localEulerAngles = Vector3.forward * 180;
            }
            previousPos = transform.position;
        }
    }

    public override void LateProcessAbility()
    {
        if(character.Animator != null )
        {
            character.Animator.SetFloat("Horizontal", currentDirection.x);
            character.Animator.SetFloat("Vertical", currentDirection.y);
        }
    }

    public void DoWander (bool canWander)
    {
        this.canWander = canWander;
        if(Mathf.Abs(currentDirection.x) > Mathf.Abs(currentDirection.y))
        {
            currentDirection.x = 1;
            currentDirection.y = 0;
        }else
        {
            currentDirection.x = 0;
            currentDirection.y = 1;
        }
    }
}
