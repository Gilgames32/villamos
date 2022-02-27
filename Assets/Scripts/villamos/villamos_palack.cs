using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villamos_palack : MonoBehaviour
{
    public AudioManager aud;
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    public float moveSpeed;
    public bool canMove = true;

    public int maxhp = 10;
    private int hp;
    public GameObject bar;
    public gameover go;

    private void OnEnable()
    {
        canMove = true;

    }
    private void Start()
    {
        hp = maxhp;
        body = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        bar.transform.localScale = new Vector3((float)hp / maxhp, 1f, 1f);
        if (hp == 0)
        {
            go.GameOver();

        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    private void Update()
    {
        horizontal = canMove ? Input.GetAxisRaw("Horizontal") : 0f;
        vertical = canMove ? Input.GetAxisRaw("Vertical") : 0f;
    }

}
