using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stuff", menuName = "Items/Stuff", order = 51)]
public class InventoryStuff : InventoryItem
{
    public int tasty;
    public override void Parameters()
    {
        base.Parameters();
        Debug.Log("tasty = " + tasty);
    }
}
