using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabla_palack : MonoBehaviour
{
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    public float moveSpeed;
    public bool canMove = true;


    //palack movement

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        canMove = true;
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
