using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_kampo : MonoBehaviour
{
    public langdbase lan;
    public string enchantKey;
    public player pl;
    public AudioManager aud;
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float moveSpeed;
    public float reelForce;
    public float reelGravity;
    public float reelTime;
    public float reelCooldown;
    private float reeltimer;
    private bool canReel;
    private bool enchant;
    public GameObject bar;
    private float barWidth;
    public bool canMove = true; //cant move while reeling
    private GameObject fishe;
    private float timer;
    private bool timeDir = true; //true +, false -

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        rb.gravityScale = 0;
        enchant = pl.inventory.Contains(lan.Lanra(enchantKey)[0]);
        timer = reelTime;
        reeltimer = reelCooldown;
    }

    private void OnDisable()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector3();
        barWidth = 1;
        bar.transform.localScale = new Vector3(barWidth, 1, 1);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }


    private void Update()
    {
        horizontal = canMove ? Input.GetAxisRaw("Horizontal") : 0f;
        vertical = canMove ? Input.GetAxisRaw("Vertical") : 0f;

        if (!canMove)
        {
            if (canReel)
            {
                if (Input.GetButtonDown("Act"))
                {
                    aud.Play("click");
                    rb.velocity = new Vector3(0, (enchant ? reelForce * 1.5f : reelForce)* barWidth, 0);
                    reeltimer = 0;
                    canReel = false;
                }
                

                if (timeDir)
                {
                    if (timer <= reelTime)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = reelTime;
                        timeDir = false;
                    }
                }
                else
                {
                    if (timer >= 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        timer = 0;
                        timeDir = true;
                    }
                }

            }
            else
            {
                if (Input.GetButtonDown("Act"))
                {
                    reeltimer = 0;
                }
                reeltimer += Time.deltaTime;
                if (reeltimer >= reelTime)
                {
                    canReel = true;
                    timer = reelTime;
                    timeDir = false;
                }
            }
            barWidth = timer / reelTime;
            bar.transform.localScale = new Vector3(barWidth, 1, 1);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && canMove)
        {
            aud.Play("fishpickup");
            fishe = collision.gameObject;
            fishe.GetComponent<BoxCollider2D>().isTrigger = false;
            fishe.transform.parent = gameObject.transform;
            fishe.layer = 7;
            if (fishe.GetComponent<Animator>() != null)
            {
                fishe.GetComponent<Animator>().enabled = false;
            }

            fishe.transform.position = new Vector3(-0.1f, -1.94f, 0) + gameObject.transform.position;
            canMove = false;
            rb.velocity = new Vector3();
            rb.gravityScale = reelGravity;
        } 
    }
}
