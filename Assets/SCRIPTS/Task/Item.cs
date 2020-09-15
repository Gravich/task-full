using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class Item : MonoBehaviour
{
    public InventoryItem Config;
    public bool Generate = true;//опция для пересоздания конфига итема, если тот уехал со сцены с рюкзак. 
                                //Используется при удалении объекта со сцены. В инвентаре хранится только конфиг
                                //Состояние флага меняется при повторном спавнинге объекта

    private Transform HandsPoint;

    public bool isAttaching;
    public bool isAttached;
    public bool isDragging;

    private Camera Cam;
    public Backpack OwnderBackpack;
    public string HandsTag;

    void Start()
    {
        Cam = Camera.main;
        isAttaching = false;
        isAttached = false;
        isDragging = false;
        HandsPoint = GameObject.FindGameObjectWithTag(HandsTag).transform;
        if (Generate)
        {
            Config = Instantiate<InventoryItem>(Config);
        }
    }

    private void OnMouseDrag()
    {
        if (!isAttached)
        {
            isDragging = true;
            Vector3 MPoint = Input.mousePosition;
            MPoint.z = Cam.WorldToScreenPoint(transform.position).z;
            MPoint = Cam.ScreenToWorldPoint(MPoint);
            transform.position = Vector3.Lerp(MPoint, HandsPoint.transform.position, 0.1f);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.parent = null;
            //для гуя
            Cam.GetComponent<ActorRaycast>().HandedItem = this;
        }
    }

    private void OnMouseUp()
    {
        Backpack select = Cam.GetComponent<ActorRaycast>().BackpackSelect;
        if (select && !isAttached)
        {
            select.Attach(this);
        }
        Cam.GetComponent<ActorRaycast>().HandedItem = null;
        isDragging = false;
    }

}