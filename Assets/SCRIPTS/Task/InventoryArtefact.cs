using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artefact", menuName = "Items/Artefact", order = 51)]
public class InventoryArtefact : InventoryItem
{
    public int costBase;
    public override void Parameters()
    {
        base.Parameters();
        Debug.Log("cost = "+costBase);
    }
}
