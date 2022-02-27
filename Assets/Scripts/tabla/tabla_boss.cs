using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tabla_boss : MonoBehaviour
{
    public langdbase lan;
    public tprinter printer;
    public tabla_attacks attacker;
    public tabla_palack palack;
    private int atIndex;
    public ending end;
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
            end.tablabefore = true;
            palack.enabled = false;
            enabled = false;
        }
    }

    private void Attack(string attack)
    {
        switch (attack)
        {
            case "1":
                int[] types1 = { 0 };
                attacker.StartAttcak(types1, 2.5f, 0.6f, 5);
                break;

            case "2":
                int[] types2 = { 1, 7};
                attacker.StartAttcak(types2, 2, 0.5f, 10);
                break;

            case "3":
                int[] types3 = { 2, 3, 4, 5, 6 };
                attacker.StartAttcak(types3, 1, 0.3f, 10);
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
