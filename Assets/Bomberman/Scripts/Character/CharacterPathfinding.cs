using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// attach this component to an npc so that it can find the stortest distance to targetposition with the help od a start algorithm
/// </summary>
public class CharacterPathfinding : CharacterAbility
{
    public float movementSpeed = 3;
    public AnimationCurve curve;
    public LayerMask obstacleLayermask;
    public float offset = 0.5f;
    public bool isPathInterupted;

    Tween tween;
    Vector3 previousPos;
    RaycastHit2D hit;
    public bool hasReachedDestination = true;

    public void SetDestination(Vector3 targetPos)
    {
        if(!hasReachedDestination)
        {
            return;
        }
        
        if(tween != null)
        {
            tween.Kill();
        }
        isPathInterupted = false;
        Vector3[] points = NavMesh.GetPath(transform.position, targetPos);
        hasReachedDestination = false;
        float distance = points.Length;
        float time = distance / movementSpeed;
        
        tween = transform.DOPath(points, time, PathType.Linear).SetEase(curve).OnUpdate(()=>
        {
             Vector3 currentDirection = (transform.position - previousPos).normalized;
             float dst = movementSpeed * Time.deltaTime;
             hit = Physics2D.Raycast(transform.position, currentDirection, offset + dst, obstacleLayermask);
             if(hit)
             {
                 tween.Kill();
                 isPathInterupted = true;
                 hasReachedDestination = true;
             }
            transform.right = (transform.position - previousPos).normalized;
            if (transform.localEulerAngles.y != 0)
            {
                transform.localEulerAngles = Vector3.forward * 180;
            }
            previousPos = transform.position;
            GetComponent<CharacterWander>().currentDirection = transform.right;
        }).OnComplete(() => { hasReachedDestination = true; });
    }

    public void ResetPathfinding()
    {
        hasReachedDestination = true;
        tween.Kill();
    }
}
