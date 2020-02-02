using UnityEngine;
using System.Collections;

/// <summary>
/// this class is responsible for player movement
/// </summary>
public class CharacterMovement : CharacterAbility
{
    public float walkSpeed = 3f;
    public float idleThreshold = 0.05f;
    public float acceleration = 1f;
    public float deceleration = 1f;

    public float MovementSpeed { get; set; }

    float horizontalMovement;
    float verticalMovement;
    Vector3 movementVector;
    Vector2 currentInput;
    Vector2 normalizedInput;
    Vector2 lerpedInput = Vector2.zero;
    float currentAcceleration = 0f;

    public override void Initialization()
    {
        base.Initialization();
        MovementSpeed = walkSpeed;
        character.ChangeState(CharacterStates.IDLE);
    }

    public override void ProcessAbility()
    {
        HandleMovement();
    }

    public override void LateProcessAbility()
    {
        if (character.Animator != null)
        {
            character.Animator.SetFloat("Horizontal", movementVector.x);
            character.Animator.SetFloat("Vertical", movementVector.y);
        }
    }

    protected override void HandleInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(verticalMovement) > Mathf.Abs(horizontalMovement))
        {
            horizontalMovement = 0;
        }
        else
        {
            verticalMovement = 0;
        }
    }

    protected virtual void HandleMovement()
    {
        if ((currentInput.magnitude > idleThreshold)
            && (character.state == CharacterStates.IDLE))
        {
            character.ChangeState(CharacterStates.WALK);
        }

        if ((character.state == CharacterStates.WALK)
            && (character.Controller.CurrentMovement.magnitude <= idleThreshold))
        {
            character.ChangeState(CharacterStates.IDLE);
        }

        DoMovement();
    }

    void DoMovement()
    {
        movementVector = Vector3.zero;
        currentInput = Vector2.zero;

        currentInput.x = horizontalMovement;
        currentInput.y = verticalMovement;

        normalizedInput = currentInput.normalized;

        if ((acceleration == 0) || (deceleration == 0))
        {
            lerpedInput = currentInput;
        }
        else
        {
            if (normalizedInput.magnitude == 0)
            {
                currentAcceleration = Mathf.Lerp(currentAcceleration, 0f, deceleration * Time.deltaTime);
                lerpedInput = Vector2.Lerp(lerpedInput, lerpedInput * currentAcceleration, Time.deltaTime * deceleration);
            }
            else
            {
                currentAcceleration = Mathf.Lerp(currentAcceleration, 1f, acceleration * Time.deltaTime);
                lerpedInput = Vector2.ClampMagnitude(normalizedInput, currentAcceleration);
            }
        }

        movementVector = lerpedInput;
        movementVector *= MovementSpeed;
        if (movementVector.magnitude > MovementSpeed)
        {
            movementVector = Vector3.ClampMagnitude(movementVector, MovementSpeed);
        }
        
        if ((currentInput.magnitude <= idleThreshold) && (character.Controller.CurrentMovement.magnitude < idleThreshold))
        {
            movementVector = Vector3.zero;
        }
        
        character.Controller.SetMovement(movementVector);
    }
}