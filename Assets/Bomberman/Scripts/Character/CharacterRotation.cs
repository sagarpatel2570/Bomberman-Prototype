using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// does the rotation of the character based on input
/// </summary>
public class CharacterRotation : CharacterAbility
{
    public float turnSpeed = 2;

    Vector3 forwardDirection;
    float targetAngle;
    float currentTurnSpeed;

    protected override void OnEnable()
    {
        currentTurnSpeed = turnSpeed;
    }

    public override void ProcessAbility()
    {
        if ((Vector2)character.Controller.CurrentMovement.normalized != Vector2.zero)
        {
            forwardDirection = character.Controller.CurrentMovement.normalized;
        }

        currentTurnSpeed = turnSpeed;


        targetAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(Mathf.DeltaAngle(character.transform.localEulerAngles.z, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(character.transform.localEulerAngles.z, targetAngle, currentTurnSpeed * 360 * Time.deltaTime);
            character.transform.localEulerAngles = Vector3.forward * angle;
        }
    }
}
