using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class tprinter : MonoBehaviour
{
    public tchooseprint chooseprinter;
    public Text texto;
    public Animator animo;
    public player playero;
    public eitem pitems;
    public AudioManager aud;


    private string[] dia;
    public UnityEvent eve;
    private int diaIndex;
    private int stringIndex;
    public float diaTime; //time between letters, given in miliseconds
    public float speciaCharWait;
    public char[] specialChars;
    private bool pMoveReEnable;
    public bool isDia; //currently dia is visible
    private bool isPrint; //currently printing


    private void Start()
    {
        diaTime = diaTime * 0.01f;
        speciaCharWait = speciaCharWait * 0.01f;
    }

    private void LateUpdate()
    {
        //idk why and how but lateupdate prevents instaprint from getting called on the first frame
        //and it also calls us the first act so ig its fine /shrug
        if (isDia && Input.GetButtonDown("Act"))
        {
            Act();
        }
    }

    public void StartAct(string[] dialogue, UnityEvent eventAfter)
    {
        if (!playero.isTeleporting && !isDia && !chooseprinter.isDia)
        {
            dia = dialogue;
            eve = eventAfter;
            animo.SetTrigger("popup");
            pMoveReEnable = playero.canMove;
            playero.canMove = false;
            isDia = true;
            if (!Input.GetButtonDown("Act"))
            {
                Act();
            }
        }
        

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
            if (diaIndex < dia.Length)
            {
                //next
                isPrint = true;
                StartCoroutine(PrintLine());
            }
            else
            {
                //close 
                texto.text = "";
                animo.SetTrigger("popdown");
                playero.canMove = pMoveReEnable;
                diaIndex = 0;
                StartCoroutine(DelayIsDia());
            }
        }
    }

    IEnumerator DelayIsDia()
    {
        //prevents player from closing and opening with the same input (it works, dont touch it)
        yield return new WaitForEndOfFrame();
        isDia = false;
        if (eve != null)
        {
            eve.Invoke();
        }
    }

    private void InstaPrint()
    {
        animo.SetTrigger("instant");
        animo.ResetTrigger("popup");
        texto.text = "";
        bool commandStarted = false;
        for (int i = 0; i < dia[diaIndex].Length; i++)
        {
            if (dia[diaIndex][i] == '|')
            {
                commandStarted = !commandStarted;
            }

            else if (!commandStarted)
            {
                texto.text += dia[diaIndex][i];
            }
        }
        isPrint = false;
        diaIndex++;
    }


    private IEnumerator PrintLine()
    {
        stringIndex = 0;
        texto.text = "";
        if (animo.GetCurrentAnimatorClipInfo(0)[0].clip.name != "visible")
        {
            yield return new WaitForSeconds(.4f);
        }
        
        

        while (stringIndex < dia[diaIndex].Length)
        {
            float waitTime = diaTime;
            char letter = dia[diaIndex][stringIndex];
            if (letter == '|')
            {
                string command = "";

                stringIndex++;
                letter = dia[diaIndex][stringIndex];
                while (letter != '|')
                {
                    command += letter;
                    stringIndex++;
                    letter = dia[diaIndex][stringIndex];
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
        isPrint = false;
        diaIndex++;
    }

}
