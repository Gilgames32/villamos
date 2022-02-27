using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class langui : MonoBehaviour
{
    public langdbase lan;
    public string key;
    public Text texto;

    private void Start()
    {
        if (lan == null || key == "" || texto == null)
        {
            print("MISSING REFERENCES: " + gameObject.name);
        }
        else
        {
            texto.text = lan.Lanra(key)[0];
        }
        
    }
}
