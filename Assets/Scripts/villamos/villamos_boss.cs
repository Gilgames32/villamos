using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class villamos_boss : MonoBehaviour
{
    public langdbase lan;
    public tprinter printer;
    public villamos_attack attacker;
    public villamos_palack palack;
    private int atIndex;
    public string[] atKey;
    private string[][] at;
    public UnityEvent endEvent;
    private UnityEvent NextPhaseEvent;

    private void Start()
    {
        NextPhaseEvent = new UnityEvent();
        NextPhaseEvent.AddListener(NextPhase);
        at = new string[atKey.Length][];
        for (int i = 0; i < atKey.Length; i++)
        {
            if (atKey[i][0] == '#')
            {
                at[i] = new string[] { atKey[i] };
            }
            else
            {
                at[i] = lan.Lanra(atKey[i]);
            }
        }
        palack.enabled = true;
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        NextPhase();
    }

    public void NextPhase()
    {
        if (atIndex < at.Length)
        {
            if (at[atIndex][0][0] == '#')
            {
                Attack(at[atIndex][0].Remove(0, 1));
            }
            else
            {
                printer.StartAct(at[atIndex], NextPhaseEvent);
            }
            atIndex++;
        }
        else
        {
            endEvent.Invoke();
            palack.enabled = false;
            enabled = false;
        }
    }

    private void Attack(string attack)
    {
        switch (attack)
        {
            case "1":
                attacker.StartAttcak(8, 0.75f, 10, 1);
                break;

            case "2":
                attacker.StartAttcak(16, 0.75f, 10, 1);
                break;

            case "3":
                attacker.StartAttcak(6, 1f, 20, 2);
                break;

            case "4":
                attacker.StartAttcak(-2, 2, 4.8f, 3);
                break;

            default:
                break;
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject, 5);
    }
}
