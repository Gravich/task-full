using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Medic", menuName = "Items/Medic", order = 51)]
public class InventoryMedic : InventoryItem
{
    public int healing;
    public override void Parameters()
    {
        base.Parameters();
        Debug.Log("healing = " + healing);
    }
}
