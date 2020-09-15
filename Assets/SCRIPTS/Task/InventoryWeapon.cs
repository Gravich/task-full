using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 51)]
public class InventoryWeapon : InventoryItem
{
    public int damage;
    public override void Parameters()
    {
        base.Parameters();
        Debug.Log("damage = " + damage);
    }
}

