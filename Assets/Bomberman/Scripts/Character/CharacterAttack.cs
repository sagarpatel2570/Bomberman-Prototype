using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is responsible for spawning bomb
/// </summary>
public class CharacterAttack : CharacterAbility
{
    public int numberOfBomb = 1;
    public Weapon initialWeaponPrefab;

    int currentNumberOfBombLeft;

    public override void Initialization()
    {
        base.Initialization();
        currentNumberOfBombLeft = numberOfBomb;
    }

    protected override void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DoAttack();
        }
    }

    void DoAttack()
    {
        if(currentNumberOfBombLeft <= 0)
        {
            return;
        }
        Vector2 weaponSpawnPoint = new Vector2((int)(transform.position.x) + 0.5f, (int)(transform.position.y) + 0.5f);
        Weapon weapon = SimplePool.Spawn(initialWeaponPrefab.gameObject, weaponSpawnPoint, Quaternion.identity).GetComponent<Weapon>();
        currentNumberOfBombLeft--;
        weapon.OnWeaponUsed += OnWeaponUsed;
        weapon.UseWeapon();
    }

    void OnWeaponUsed (Weapon weapon)
    {
        weapon.OnWeaponUsed -= OnWeaponUsed;
        currentNumberOfBombLeft++;
    }


}
