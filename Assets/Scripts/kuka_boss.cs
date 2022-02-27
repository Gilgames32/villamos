using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class kuka_boss : MonoBehaviour
{
    public langdbase lan;
    public tprinter printer;
    public GameObject arrow;
    public GameObject can;
    private Rigidbody2D canRB;
    public float shootForce;
    
    public bool canAim;
    private bool canHit;
    public float rotateSpeed = 2.5f;

    public string diakukaKey;
    private string[] diakuka;

    public UnityEvent winEvent, loseEvent;

    private void Start()
    {
        diakuka = lan.Lanra(diakukaKey);
    }

    private void OnEnable()
    {
        canHit = false;
        canAim = false;
        Spawn();
    }

    private void Update()
    {
        if (canAim)
        {
            arrow.transform.Rotate(0, 0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed * -100);
        }

        if (Input.GetButtonDown("Act"))
        {
            if (canAim)
            {
                Throw();
            }
            else if(!canHit)
            {
                loseEvent.Invoke();
            }

        }
    }

    private void Throw()
    {
        canAim = false;
        float arad = arrow.transform.eulerAngles.z * Mathf.PI /180;
        float ax = Mathf.Sin(arad);
        float ay = Mathf.Cos(arad);
        canRB.velocity = new Vector3(ax*-shootForce, ay*shootForce, 0);
    }


    private void Spawn()
    {
        canAim = true;
        GameObject newcan = Instantiate(can);
        newcan.transform.parent = can.transform.parent;
        newcan.transform.position = can.transform.position;
        newcan.SetActive(true);
        canRB = newcan.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canHit = true;
            printer.StartAct(diakuka, winEvent);
        }
    }

    private void OnDisable()
    {
        Destroy(canRB.gameObject, 0.5f);
    }
}
