using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishe : MonoBehaviour
{
    public langdbase lan;
    public string itemNameKey;
    public string itemDescKey;
    [HideInInspector]
    public string[] itemDesc;

    private void Start()
    {
        itemDesc = lan.Lanra(itemDescKey);
    }

}
