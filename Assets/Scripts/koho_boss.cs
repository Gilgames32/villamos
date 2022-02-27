using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class koho_boss : MonoBehaviour
{
    public langdbase lan;
    public AudioManager aud;
    public AudioSource source;
    public tprinter printer;
    public GameObject mutato;
    public GameObject bar;
    public Rigidbody2D pmutato;
    public float threshold = 22.5f;
    public float gravity;
    public float force;
    public float melttime;
    private float meltp;
    private float mangle;
    private bool canMove;
    public string startdiaKey, enddiaKey;
    private string[] startdia, enddia;
    public UnityEvent startEvent, endEvent;


    private void Start()
    {
        startdia = lan.Lanra(startdiaKey);
        enddia = lan.Lanra(enddiaKey);
        startEvent.AddListener(Starter);
    }

    private void OnEnable()
    {
        pmutato.gravityScale = gravity;
        canMove = false;
        bar.transform.localScale = new Vector3(meltp / melttime, 1, 1);
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        printer.StartAct(startdia, startEvent);
    }

    private void Starter()
    {
        canMove = true;
    }

    private void Update()
    {
        mangle = (pmutato.gameObject.transform.position.y-25) / 4 * -135;
        mutato.transform.rotation = Quaternion.Euler(0, 0, mangle);
        if (canMove)
        {
            if (Input.GetButtonDown("Act"))
            {
                pmutato.velocity = new Vector3(0, force, 0);
                aud.Play("click");
            }

            if (Mathf.Abs(mangle) < threshold)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
                if (meltp < melttime)
                {
                    meltp += Time.deltaTime;
                }
                else
                {
                    //win
                    source.Stop();
                    meltp = melttime;
                    canMove = false;
                    printer.StartAct(enddia, endEvent); 
                }
            }
            else
            {
                source.Stop();
                if (meltp > 0)
                {
                    meltp -= Time.deltaTime;
                }
                else
                {
                    meltp = 0;
                }
            }

            bar.transform.localScale = new Vector3(meltp / melttime, 1, 1); 
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject, 1);
    }

}
