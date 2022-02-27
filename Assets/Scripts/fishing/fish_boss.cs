using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fish_boss : MonoBehaviour
{
    public ecall ec;
    public tprinter printer;
    public fish_kampo kampo;
    public int reFish = 8;
    public UnityEvent onCatch, lastCatch;


    private void OnEnable()
    {
        kampo.enabled = true;
    }
    private void LastDestroy()
    {
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            fishe fish = collision.GetComponent<fishe>();
            ec.GiveItem(fish.itemNameKey);
            string[] diafish = fish.itemDesc;
            reFish--;
            if (reFish == 0)
            {
                lastCatch.AddListener(LastDestroy);
                printer.StartAct(diafish, lastCatch);
            }
            else
            {
                printer.StartAct(diafish, onCatch);
            }
            Destroy(fish.gameObject);
            kampo.enabled = false;
            enabled = false;
        }
    }

}
