using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// self destructs the object after some time
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    public float selfDestructTime = 1;

    void OnEnable()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy ()
    {
        yield return new WaitForSeconds(selfDestructTime);
        SimplePool.Despawn(gameObject);
    }

   
}
