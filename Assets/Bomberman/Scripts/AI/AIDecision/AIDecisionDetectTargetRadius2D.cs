using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the character is in certain radius it will change the state
/// only if there is no obstacle between NPC and target
/// </summary>
public class AIDecisionDetectTargetRadius2D : AIDecision
{
    /// the radius to search our target in
    public float Radius = 3f;
    [Range(0,180)]
    public float viewAngle = 60;
    public bool cyclicCheck;
    
    public LayerMask TargetLayer;
    public LayerMask obstacleLayermask;

    protected Vector2 raycastOrigin;
    protected Character character;
    protected Collider2D detectionCollider = null;
    protected bool init = false;
    protected float thresholdAngle;

    public override void Initialization()
    {
        thresholdAngle = Mathf.Cos(viewAngle / 2f * Mathf.Deg2Rad);
        character = this.gameObject.GetComponent<Character>();
        init = true;
    }

    public override bool Decide()
    {
        return DetectTarget();
    }

    protected virtual bool DetectTarget()
    {
        detectionCollider = null;
        raycastOrigin = transform.position;
        
        detectionCollider = DetectCollider();

        if (detectionCollider != null)
        {
            Vector2 dir = (detectionCollider.gameObject.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Vector3.Distance(transform.position, detectionCollider.gameObject.transform.position), obstacleLayermask);
            if(hit.transform == null)
            {
                float dotProduct = Vector2.Dot(dir, transform.right);

                if (dotProduct > thresholdAngle || cyclicCheck)
                {
                    brain.SetTarget(detectionCollider.gameObject.transform);
                    return true;
                }
            }
        }
        brain.SetTarget(null);

        return false;
    }

    public Collider2D DetectCollider ()
    {
        return Physics2D.OverlapCircle
            (
            raycastOrigin,
            Radius,
            TargetLayer
            );
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);

        Vector3 forwardDir = transform.right;
        Vector3 leftDir = Quaternion.AngleAxis(viewAngle / 2, Vector3.forward) * forwardDir;
        Vector3 rightDir = Quaternion.AngleAxis(-viewAngle / 2, Vector3.forward) * forwardDir;

        Gizmos.color = (brain != null && brain.target != null) ? Color.red : Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * Radius);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * Radius);
    }
}
