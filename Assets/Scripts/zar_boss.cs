using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class zar_boss : MonoBehaviour
{
    public langdbase lan;
    public tprinter printer;
    public AudioManager aud;
    public GameObject[] pins;
    private int pindex;
    private float[] piny;
    public float threshold;
    public float pinforce;
    public GameObject gem;
    
    private bool control;
    public GameObject koho;
    public string diazarKey;
    private string[] diazar;
    public UnityEvent finishEvent;

    private void Start()
    {
        diazar = lan.Lanra(diazarKey);

        piny = new float[pins.Length];
        for (int i = 0; i < pins.Length; i++)
        {
            piny[i] = pins[i].transform.position.y;
            pins[i].SetActive(true);
        }
    }

    private void OnEnable()
    {
        //telepurte.TeleportPlayer(50, 25, false);
        pindex = 0;
        GemH(pindex);
        control = true;
    }

    private void Update()
    {
        //GemH(pindex);
        if (Input.GetButtonDown("Act"))
        {
            if (control)
            {
                if (Mathf.Abs(pins[pindex].transform.position.y - piny[pindex]) < threshold)
                {
                    aud.Play("click");
                    pins[pindex].GetComponent<Rigidbody2D>().simulated = false;
                    pins[pindex].transform.position = new Vector3(pins[pindex].transform.position.x, piny[pindex], 0);
                    if (pindex + 1 < pins.Length)
                    {
                        pindex++;
                        
                        GemH(pindex);
                    }
                    else
                    {
                        //win
                        control = false;
                        printer.StartAct(diazar, finishEvent);

                    }

                }
                else
                {
                    aud.Play("hit");
                    if (pindex > 0)
                    {
                        pindex--;
                        
                        pins[pindex].GetComponent<Rigidbody2D>().simulated = true;
                    }
                    GemH(pindex);
                }
            }
            
        }
        if (Input.GetAxisRaw("Vertical") > 0 && control)
        {
            gem.GetComponent<Rigidbody2D>().velocity = new Vector3(0, pinforce, 0);
        }
    }

    private void GemH(int index)
    {
        gem.transform.position = new Vector3(pins[index].transform.position.x - gem.GetComponent<SpriteRenderer>().bounds.size.x / 2, 24, 0);
    }

    private void OnDisable()
    {
        Destroy(gameObject, 1);
    }
}
