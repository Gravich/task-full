using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ActorRaycast : MonoBehaviour
{
    public Text ObjInfo;
    public Text BackpackOutput;

    public Item HandedItem;
    public Item CastedItem;

    public Backpack BackpackSelect;
    public Backpack OpenedBackpack;

    void Update()
    {
        BackpackSelect = null;
        CastedItem = null;
        Ray MPoint = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit RayCastObject;




        if (Physics.Raycast(MPoint, out RayCastObject, 10.0f, LayerMask.GetMask("Items")))
        {
            CastedItem = RayCastObject.collider.gameObject.GetComponent<Item>();
            if (CastedItem)
            {
                ObjInfo.text = CastedItem.Config.Name +" "+ CastedItem.Config.ID;
            }
        }
        else
        {
            ObjInfo.text = "";
        }


        if (Physics.Raycast(MPoint, out RayCastObject, 10.0f, LayerMask.GetMask("Backpacks")))
        {
            BackpackSelect = RayCastObject.collider.gameObject.GetComponent<Backpack>();
        }
        else
        {
            BackpackOutput.text = "";
        }
        if (BackpackSelect)
        {
            if (HandedItem)
            {
                BackpackOutput.text = "Attach";
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && !BackpackSelect.isOpened)
                {
                    BackpackSelect.Open();
                    OpenedBackpack = BackpackSelect;
                }
                BackpackOutput.text = "Open";
            }
        }

        if (OpenedBackpack)
            if (Input.GetKeyUp(KeyCode.Mouse0) && OpenedBackpack.isOpened)
            {
                if (CastedItem && CastedItem.OwnderBackpack)
                {
                    CastedItem.OwnderBackpack.Detach(CastedItem);
                }
                OpenedBackpack.Close();
                OpenedBackpack = null;
            }

        if (Input.GetKeyDown(KeyCode.L) && OpenedBackpack && OpenedBackpack.isOpened)
        {
            OpenedBackpack.TakeLast();
        }

    }
}
