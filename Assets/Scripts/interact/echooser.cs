using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class echooser : MonoBehaviour
{
    private ecall ec;
    public string qKey, cLKey, cRKey;
    private string qString, cLString, cRString;
    public UnityEvent CL, CR;

    private void Start()
    {
        ec = GetComponent<ecall>();
        qString = ec.lan.Lanra(qKey)[0];
        cLString = ec.lan.Lanra(cLKey)[0];
        cRString = ec.lan.Lanra(cRKey)[0];
    }

    public void Choose()
    {
        ec.chooser.StartChoose(qString, cLString, cRString, CL, CR);
    }
}
