using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class end_villamos : MonoBehaviour
{
    public villamos_boss boss;
    public ending end;

    public UnityEvent hasJegy, noJegy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (end.jegy)
            {
                hasJegy.Invoke();
                gameObject.SetActive(false);
                enabled = false;
            }
            else
            {
                noJegy.Invoke();
                gameObject.SetActive(false);
                enabled = false;
            }
        }
    }
}
