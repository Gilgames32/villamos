using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ending : MonoBehaviour
{
    public langdbase lan;
    public player pl;
    public bool tablabefore, jegy;
    public tpcontroll telepurte;
    public Text title, desc, counter;
    public string[] endingsKey;
    public string endKey;
    public general_end trueend, hitend, goodend, tablaend;


    public void Megallo()
    {
        telepurte.Teleport(3.75f, -0.92f, false, true);
        if (!jegy)
        {
            jegy = pl.inventory.Contains(lan.Lanra("jegy_i")[0]);
        }
        if (tablabefore)
        {

            if (jegy)
            {
                print("true");
                trueend.gameObject.SetActive(true);
            }
            else
            {
                print("hit");
                hitend.gameObject.SetActive(true);
            }
        }
        else
        {
            if (jegy)
            {
                print("good");
                goodend.gameObject.SetActive(true);
            }
            else
            {
                print("tabla");
                tablaend.gameObject.SetActive(true);

            }
        }

    }

    public void End(string key)
    {
        pl.ResetDir(true);
        telepurte.Teleport(0, -25, false, true);
        int endingsct = 0;
        PlayerPrefs.SetInt(key, 1);
        foreach (string endingName in endingsKey)
        {
            if (PlayerPrefs.GetInt(endingName, 0) == 1)
            {
                endingsct++;
            }
        }
        counter.text = lan.Lanra(endKey)[0] + endingsct.ToString() + "/" + endingsKey.Length;
        title.text = lan.Lanra(Array.Find(endingsKey, element => element == key))[0];
        desc.text = lan.Lanra(Array.Find(endingsKey, element => element == key))[1];
    }
}
