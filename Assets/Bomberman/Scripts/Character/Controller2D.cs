using UnityEngine;
using System.Collections;

/// <summary>
/// this class is responsible for movement of object with the help of physics
/// </summary>
public class Controller2D : MonoBehaviour
{
    public Rigidbody2D RigidBody { get; set; }
    public Vector3 CurrentMovement { get; protected set; }

    Character character;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    void FixedUpdate()
    {
        Vector2 newMovement = RigidBody.position + (Vector2)(CurrentMovement) * Time.fixedDeltaTime;
        RigidBody.MovePosition(newMovement);
    }

    public void SetMovement(Vector3 movement)
    {
        CurrentMovement = movement;
    }
}
