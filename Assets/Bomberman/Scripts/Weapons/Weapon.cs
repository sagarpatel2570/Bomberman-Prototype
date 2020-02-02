using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string id;
    public float weaponUseTime;
    public Action<Weapon> OnWeaponUsed;

    public virtual void UseWeapon()
    {
        
    }
}
