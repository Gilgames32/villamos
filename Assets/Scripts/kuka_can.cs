using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuka_can : MonoBehaviour
{
    public AudioManager aud;
    public kuka_boss boss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!boss.canAim)
        {
            aud.PlayOne("can");
        }
        
    }
}
