using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class NetworkManager : MonoBehaviour
{
    private static NetworkManager Instance { get; set; } = null;

    public string url;
    public string auth;

    private void Start()
    {
        Instance = this;
    }
    public void OnSomeAttach(Item item, Backpack owner)
    {
        Instance.StartCoroutine(Send(item, owner, "attach"));
        Debug.Log(owner.name + " attach " + item.name);
    }

    public void OnSomeDetach(Item item, Backpack owner)
    {
        Instance.StartCoroutine(Send(item, owner, "detach"));
        print(owner.name + " detach " + item.name);
    }

    public void OnSomePut(Item item, Backpack owner)
    {
        Instance.StartCoroutine(Send(item, owner, "put"));
        print(owner.name + " put " + item.name);
    }

    public void OnSomeTake(Item item, Backpack owner)
    {
        Instance.StartCoroutine(Send(item, owner, "take"));
        print(owner.name + " take " + item.name);
    }
    private IEnumerator Send(Item item, Backpack owner, string e)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", item.Config.Name);
        form.AddField("ID", item.Config.ID);
        form.AddField("Owner", owner.name);
        form.AddField("Event", e);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("auth", auth);

        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data: " + www.downloadHandler.text);
        }

    }

}