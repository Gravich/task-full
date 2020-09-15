using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 51)]
public class InventoryItem : ScriptableObject
{
    public int ID;
    public string Name;
    public Item Visual;
    public string Type;
    public float weight;
    public InventoryItem()
    {
    }
    public Item DropFromInventory(Vector3 worlPoint)
    {
        Item dropped = Instantiate<Item>(Visual);
        dropped.transform.position = worlPoint;
        dropped.Generate = false;
        dropped.Config = this;
        return dropped;
    }
    private void OnEnable()
    {
        ID = Random.Range(0, 100);
    }
    public virtual void Parameters()
    {
        Debug.Log(
            "Name  =" + Name +
            "Visual =" + Visual +
            "ID =" + ID
            );
    }
}