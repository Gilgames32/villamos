using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditstp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.position = collision.transform.position.x > 0 ? new Vector3(-14.659f, collision.transform.position.y, 0) : new Vector3(14.659f, collision.transform.position.y, 0);
        }
    }
}
