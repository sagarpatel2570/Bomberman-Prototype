using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this component to an object so that i can take Damage and destroy
/// </summary>
public class Health : MonoBehaviour, ITakeDamage
{
    public ParticleSystem deathEffect;

    public void TakeDamage()
    {
        IGiveScore obj = GetComponent<IGiveScore>();
        if(obj != null)
        {
            obj.GiveScore();
        }

        IDeath death = GetComponent<IDeath>();
        if (death != null)
        {
            death.Destroy();
        }

        if(deathEffect != null)
        {
            SimplePool.Spawn(deathEffect.gameObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
