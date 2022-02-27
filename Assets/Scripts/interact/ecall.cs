using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ecall : MonoBehaviour
{
    public langdbase lan;
    public player pl;
    public tprinter printer;
    public tchooseprint chooser;
    private bool pInside;
    public bool triggerStart;
    public UnityEvent eve;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pInside = false;
        }
    }

    private void Update()
    {
        if (pInside && (Input.GetButtonDown("Act") || (triggerStart && !pl.isTeleporting)))
        {
            eve.Invoke();
        }
    }

    public void GiveItem(string key)
    {
        pl.AddItem(lan.Lanra(key)[0]);
    }

    public void RemoveItem(string key)
    {
        pl.DeleteItem(lan.Lanra(key)[0]);
    }
}
