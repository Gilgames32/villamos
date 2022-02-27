using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settime : MonoBehaviour
{
    public Text texto;
    private void OnEnable()
    {
        texto.text = DateTime.Now.ToString("HH:mm");
        enabled = false;
    }
}
