using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class edia : MonoBehaviour
{
    private ecall ec;
    public string[] mdiaKey;
    private string[][] mdia;
    public UnityEvent FinalEvent;
    private UnityEvent MLineFinishedEvent;

    private int mdiaIndex;


    private void Start()
    {
        ec = GetComponent<ecall>();
        MLineFinishedEvent = new UnityEvent();
        MLineFinishedEvent.AddListener(MLineFinished);
        if (mdiaKey != null)
        {
            List<string[]> mdiaLoad = new List<string[]>();
            foreach (var key in mdiaKey)
            {
                mdiaLoad.Add(ec.lan.Lanra(key));
            }
            mdia = mdiaLoad.ToArray();
        }
    }

    public void React()
    {
        if (mdiaKey.Length != 0)
        {
            ec.printer.StartAct(mdia[mdiaIndex], MLineFinishedEvent);
        }
    }

    private void MLineFinished()
    {
        if (mdiaIndex + 1 < mdia.Length)
        {
            mdiaIndex++;
        }
        else
        {
            FinalEvent.Invoke();
        }
    }

}
