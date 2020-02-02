using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public GameObject bombEffectPrefab;
    public int size;
    public float damageDurationTime = 0.1f;

    public override void UseWeapon()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(weaponUseTime);
        CameraShake.Shake();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 && y != 0)
                {
                    continue;
                }

                GameObject obj = SimplePool.Spawn(bombEffectPrefab, transform.position + Vector3.right * x + Vector3.up * y, Quaternion.identity);
                SelfDestruct selfDestruct = obj.GetComponent<SelfDestruct>();
                selfDestruct.selfDestructTime = damageDurationTime;
            }
        }
        OnWeaponUsed?.Invoke(this);
        SimplePool.Despawn(gameObject);
    }
}
