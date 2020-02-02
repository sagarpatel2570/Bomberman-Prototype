using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this component to an object so that it can give damage when touch 
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DamageOnTouch : MonoBehaviour
{
    public LayerMask targetLayerMask;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & targetLayerMask) == 0)
        {
            return;
        }

        ITakeDamage obj = collider.GetComponent<ITakeDamage>();
        if(obj != null)
        {
            obj.TakeDamage();
        }
    }
}
