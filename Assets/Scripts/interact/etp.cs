using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etp : MonoBehaviour
{
    public tpcontroll tp;
    public float x, y;
    public bool instant, letMove = true;

    public void Teleport()
    {
        tp.Teleport(x, y, instant, letMove);
    }
}
