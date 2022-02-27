using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class tchooseprint : MonoBehaviour
{

    public tprinter printer;
    public Text texto, cL, cR;
    private Animator animo;
    private player playero;
    private AudioManager aud;

    private string cdia, cLString, cRString;
    public UnityEvent cLEvent, cREvent;
    private int choice; //0:nothing, 1:L, 2:R
    public Color select, deselect;

    private int stringIndex;
    private float diaTime;
    private float speciaCharWait;
    private char[] specialChars;
    private bool pMoveReEnable;
    public bool isDia;
    private bool isPrint;


    private void Start()
    {
        //might not be the best way to do it /shrug
        animo = printer.animo;
        playero = printer.playero;
        aud = printer.aud;
        diaTime = printer.diaTime;
        speciaCharWait = printer.speciaCharWait;
        specialChars = printer.specialChars;
    }

    private void Update()
    {
        if (isDia)
        {
            if (!isPrint && Input.GetButtonDown("Horizontal"))
            {
                aud.Play("pclick");
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    choice = 1;
                    cL.color = select;
                    cR.color = deselect;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    choice = 2;
                    cL.color = deselect;
                    cR.color = select;
                }
            }
            else if (Input.GetButtonDown("Act"))
            {
                Act();
            }
        }
    }

    public void StartChoose(string question, string left, string right, UnityEvent eventLeft, UnityEvent eventRight)
    {
        if (!playero.isTeleporting && !isDia && !printer.isDia)
        {
            cdia = question;
            cLString = left;
            cRString = right;
            cLEvent = eventLeft;
            cREvent = eventRight;
            cL.color = deselect;
            cR.color = deselect;
            animo.SetTrigger("popup");
            pMoveReEnable = playero.canMove;
            playero.canMove = false;
            isDia = true;
            StartCoroutine(DelayPrintChoice());   
        }
    }

    IEnumerator DelayPrintChoice()
    {
        //a weird but somewhat working method to fix instaprints on first frame
        yield return new WaitForEndOfFrame();
        StartCoroutine(PrintChoice());
    }

    private void Act()
    {
        if (isPrint)
        {
            //skip
            StopAllCoroutines();
            InstaPrint();
        }
        else
        {
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    StartCoroutine(Close(cLEvent));
                    break;
                case 2:
                    StartCoroutine(Close(cREvent));
                    break;
                default:
                    break;
            }
        }
        

    }

    IEnumerator Close(UnityEvent chosenEvent)
    {
        aud.Play("pchoose");
        texto.text = "";
        cL.text = "";
        cR.text = "";
        animo.SetTrigger("popdown");
        choice = 0;
        playero.canMove = pMoveReEnable;
        yield return new WaitForEndOfFrame();
        chosenEvent.Invoke();
        isDia = false;

    }

    private void InstaPrint()
    {
        animo.SetTrigger("instant");
        animo.ResetTrigger("popup");
        texto.text = "";
        cL.text = "";
        cR.text = "";
        bool commandStarted = false;
        for (int i = 0; i < cdia.Length; i++)
        {
            if (cdia[i] == '|')
            {
                commandStarted = !commandStarted;
            }

            else if (!commandStarted)
            {
                texto.text += cdia[i];
            }
        }
        cL.text = cLString;
        cR.text = cRString;
        isPrint = false;
    }


    IEnumerator PrintChoice()
    {
        isPrint = true;
        stringIndex = 0;
        texto.text = "";
        cL.text = "";
        cR.text = "";
        if (animo.GetCurrentAnimatorClipInfo(0)[0].clip.name != "visible")
        {
            yield return new WaitForSeconds(.4f);
        }


        while (stringIndex < cdia.Length)
        {
            float waitTime = diaTime;
            char letter = cdia[stringIndex];
            if (letter == '|')
            {
                string command = "";

                stringIndex++;
                letter = cdia[stringIndex];
                while (letter != '|')
                {
                    command += letter;
                    stringIndex++;
                    letter = cdia[stringIndex];
                }
                stringIndex++;
                waitTime += 0.01f * int.Parse(command);
            }
            else
            {
                if (specialChars.Contains(letter))
                {
                    waitTime += speciaCharWait;
                }
                texto.text += letter;
                if (letter != ' ')
                {
                    aud.Play("pclick");
                }
                stringIndex++;
            }
            yield return new WaitForSeconds(waitTime);
        }
        yield return new WaitForSeconds(.1f);
        aud.Play("pclick");
        cL.text = cLString;
        yield return new WaitForSeconds(.1f);
        aud.Play("pclick");
        cR.text = cRString;
        isPrint = false;
    }

}
