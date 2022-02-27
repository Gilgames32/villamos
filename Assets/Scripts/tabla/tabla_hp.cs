using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabla_hp : MonoBehaviour
{
    public int maxhp = 10;
    private int hp;
    public GameObject bar;
    public gameover go;
    public AudioManager aud;
    
    private void Start()
    {
        hp = maxhp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            hp--;
            aud.Play("hit");
            UpdateBar();
        }
        
    }

    private void UpdateBar()
    {
        bar.transform.localScale = new Vector3((float)hp/maxhp, 1f, 1f);
        if (hp == 0)
        {
            go.GameOver();

        }
    }
}
