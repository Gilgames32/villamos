using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ekey : MonoBehaviour
{
    private ecall ec;
    public string keyNameKey;
    private string keyName;
    public UnityEvent HasKey, NoKey;

    private void Start()
    {
        ec = GetComponent<ecall>();
        keyName = ec.lan.Lanra(keyNameKey)[0];
    }

    public void KeyTest()
    {
        if (keyName == "" || ec.pl.inventory.Contains(keyName))
        {
            HasKey.Invoke();
        }
        else
        {
            NoKey.Invoke();
        }
    }
}
