using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    private void OnEnable()
    {
        boxCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Controller2D>() != null)
        {
            boxCollider.isTrigger = false;
        }
    }
}
