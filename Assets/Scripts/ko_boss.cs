using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ko_boss : MonoBehaviour
{
    public langdbase lan;
    public Animator anim;
    public tprinter printer;
    public AudioManager aud;
    public UnityEvent winEvent, loseEvent;


    private string[] states = { "ko", "papir", "ollo" };
    private int sti = 1;

    public string dkoKey;
    public string dpapirKey;
    public string dolloKey;

    private string[] dko;
    private int dko_id;
    private string[] dpapir;
    private string[] dollo;


    private void Start()
    {
        dko = lan.Lanra(dkoKey);
        dpapir = lan.Lanra(dpapirKey);
        dollo = lan.Lanra(dolloKey);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") && !printer.isDia)
        {
            aud.Play("pchoose");
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                if (sti < 2)
                {
                    sti++;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                if (sti > 0)
                {
                    sti--;
                }
            }

            anim.SetTrigger(states[sti]);
            

        }
        if (Input.GetButtonDown("Act"))
        {
            Select();
        }
    }

    private void Select() 
    {
        switch (states[sti])
        {
            case "ko":
                printer.StartAct(new string[] { dko[dko_id] }, null);
                if (dko_id + 1 < dko.Length)
                {
                    dko_id++;
                }
                break;

            case "papir":
                printer.StartAct(dpapir, winEvent);
                break;

            case "ollo":
                printer.StartAct(dollo, loseEvent);
                break;

            default:
                print("fatal error");
                break;
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject, 1);
    }
}
