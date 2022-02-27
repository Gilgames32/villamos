using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public langdbase lan;
    public string bicskaKey;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer sprite;
    public Text invText;
    public GameObject invUI;
    public AudioManager aud;
    public SpriteRenderer szatyor;
    
    private float horizontal;
    private float vertical;

    public float runSpeed = 20.0f;
    public bool canMove = true;
    public bool xm = true, xp = true, ym = true, yp = true;
    public bool isTeleporting = false;


    public List<string> inventory;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        invUI.SetActive(false);
        invText.text = "";
        inventory.Add(lan.Lanra(bicskaKey)[0]);
    }

    void Update()
    {
        //set order in layer by y coordinate
        sprite.sortingOrder = (int)(transform.position.y*-10);
        szatyor.sortingOrder = (int)(transform.position.y * -10)-2;

        //input
        if (canMove)
        {
            if (xm && Input.GetAxisRaw("Horizontal") < 0)
            {
                horizontal = -1;
            }
            if (xp && Input.GetAxisRaw("Horizontal") > 0)
            {
                horizontal = 1;
            }
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                horizontal = 0;
            }
            if (ym && Input.GetAxisRaw("Vertical") < 0)
            {
                vertical = -1;
            }
            if (yp && Input.GetAxisRaw("Vertical") > 0)
            {
                vertical = 1;
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                vertical = 0;
            }
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        //anim
        anim.SetFloat("ahor", horizontal);
        if (horizontal == 0f) 
        {
            anim.SetFloat("aver", vertical);
        }
        else
        {
            anim.SetFloat("aver", 0f);
        }
        
        if (horizontal == 0f && vertical == 0f)
        {
            anim.SetBool("still", true);
        }
        else 
        { 
            anim.SetBool("still", false); 
        }

        //tab
        if (Input.GetButtonDown("Tab") && canMove)
        {
            invUI.SetActive(true);
            invText.text = "";
            foreach (var item in inventory)
            {
                invText.text += item + "\n";
            }
        }
        else if (Input.GetButtonUp("Tab"))
        {
            invUI.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        //movement
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void ResetDir(bool can)
    {
        xm = can;
        xp = can;
        ym = can;
        yp = can;
        canMove = can;
    }
    public void EnableDir(string dir)
    {
        switch (dir)
        {
            case "xm":
                xm = true;
                break;
            case "xp":
                xp = true;
                break;
            case "ym":
                ym = true;
                break;
            case "yp":
                yp = true;
                break;

            default:
                break;
        }
    }

    //items
    public void AddItem(string name) 
    {
        inventory.Add(name);
        aud.Play("pickup");
    }
    public void DeleteItem(string name)
    {
        try
        {
            inventory.Remove(name);
        }
        catch (System.Exception)
        {
            print("could not remove item:" + name);
            throw;
        }
        
    }
}
