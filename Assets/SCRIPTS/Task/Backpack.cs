using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Backpack : MonoBehaviour
{
    ActorRaycast Actor;
    public AudioClip SoundClose;
    public AudioClip SoundOpen;
    public bool isOpened;
    public Canvas InvGUI;
    public Text InvItemShow;
    public ParticleSystem FunnyPuff;
    
    public CustomUnityEvent DetachEvent;
    public CustomUnityEvent AttachEvent;
    public CustomUnityEvent PutEvent;
    public CustomUnityEvent TakeEvent;


    public List<AttachPoint> AttachPoints;
    public List<Item> Cargo;
    private List<InventoryItem> CargoInside;

    private List<Text> InvItemShows;

    void Start()
    {
        isOpened = false;
        Actor = Camera.main.GetComponent<ActorRaycast>();
        InvItemShows = new List<Text>();
        CargoInside = new List<InventoryItem>();
    }

    public void Attach(Item item)
    {
        if ((Cargo.Count<AttachPoints.Count))
        {
            foreach (AttachPoint slot in AttachPoints)
            {
                if (slot.Attach(item))
                {
                    Cargo.Add(item);
                    item.OwnderBackpack = this;
                    AttachEvent.Invoke(item, this);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Слоты заняты");
            CargoInside.Add(item.Config);
            PutEvent.Invoke(item, this);
            Destroy(item.gameObject);
        }
    }

    public void Detach(Item item)
    {
        if (isOpened)
        {
            item.isAttached = false;
            foreach (AttachPoint slot in AttachPoints)
            {
                if (slot.AttachedItem == item)
                {
                    slot.Detach(item);
                    DetachEvent.Invoke(item, this);
                    break;
                }
            }

            foreach (Item cargoItem in Cargo)
            {
                if (item == cargoItem)
                {
                    Cargo.Remove(cargoItem);
                    break;
                }
            }
        }
    }
    
    public void Open()
    {
        isOpened = true;
        GetComponent<AudioSource>().PlayOneShot(SoundOpen);
        InvGUI.gameObject.SetActive(true);

        //для слотов
        foreach (Item item in Cargo)
        {
            string text = item.Config.Name + " [ID = " + item.Config.ID + "]";
            DrawInventory(new Color(200, 0, 0), text);
        }
        //для внутрянки
        foreach (InventoryItem item in CargoInside)
        {
            string text = item.Name + " [ID = " + item.ID + "]";
            DrawInventory(new Color(0, 250, 0), text);
        }

    }

    public void Close()
    {
        isOpened = false;
        GetComponent<AudioSource>().PlayOneShot(SoundClose);
        //Debug.Log("Closed");
        InvGUI.gameObject.SetActive(false);
        foreach (Text itemShow in InvItemShows)
        {
            Destroy(itemShow.gameObject);
        }
        InvItemShows.Clear();
    }

    public void TakeLast()
    {
        if (CargoInside.Count>0)
        {
            Vector3 pos = this.transform.position;
            pos.y += 3f;
            Item dropped = CargoInside[CargoInside.Count - 1].DropFromInventory(pos);
            TakeEvent.Invoke(dropped, this);
            
            //just for fun
            Vector3 randDir = new Vector3(Random.Range(-1, 1), Random.Range(5, 10), Random.Range(-1, 1));
            dropped.GetComponent<Rigidbody>().AddForce(randDir, ForceMode.Impulse);
            Instantiate(FunnyPuff, dropped.transform.position, dropped.transform.rotation).Play();
            //

            CargoInside.RemoveAt(CargoInside.Count - 1);

            
            Text description = InvItemShows[InvItemShows.Count - 1];
            InvItemShows.Remove(description);
            Destroy(description.gameObject);
        }
        else
        {
            Debug.Log("Рюкзак пуст");
        }
    }
    private void DrawInventory(Color textColor, string text)
    {
        Text itemShow = Instantiate<Text>(InvItemShow, InvGUI.transform, false);
        itemShow.text = text;
        InvItemShows.Add(itemShow);
        itemShow.color = textColor;
        itemShow.rectTransform.anchoredPosition = new Vector3(itemShow.rectTransform.rect.width / 2f + 10f, -(itemShow.rectTransform.rect.height * InvItemShows.Count + 10f));
    }
}


