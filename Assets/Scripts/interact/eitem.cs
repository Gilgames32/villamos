using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class eitem : MonoBehaviour
{
    private ecall ec;

    private void Start()
    {
        ec = GetComponent<ecall>();
    }

    public void GiveItem(string key)
    {
        ec.pl.AddItem(ec.lan.Lanra(key)[0]);
    }

    public void RemoveItem(string key)
    {
        ec.pl.DeleteItem(ec.lan.Lanra(key)[0]);
    }

}
