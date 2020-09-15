using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public string Type;
    public Item AttachedItem;

    private void Update()
    {
        if (AttachedItem)
        {
            if (AttachedItem.isAttaching)
            {
                AttachedItem.transform.localPosition = Vector3.Lerp(AttachedItem.transform.localPosition, new Vector3(0, 0, 0), 0.05f);
                if ((AttachedItem.transform.position - transform.position).magnitude < 0.1f)
                {
                    AttachedItem.transform.localPosition = new Vector3(0, 0, 0);
                    AttachedItem.isAttaching = false;
                    AttachedItem.isAttached = true;
                }
            }
        }
    }
    public bool Attach(Item item)
    {
        if (!AttachedItem && item.Config.Type == Type)
        {

            AttachedItem = item;
            item.isAttaching = true;
            item.isAttached = false;
            item.transform.parent = transform;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//повесить на ивент
            item.transform.localEulerAngles = new Vector3(0, 0, 0);
            return true;
        }
        else
            return false;
        
    }
    
    public void Detach(Item item)
    {
        AttachedItem = null;
        item.isAttached = false;
        item.transform.parent = null;
        item.OwnderBackpack = null;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
