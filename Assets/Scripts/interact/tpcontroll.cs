using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpcontroll : MonoBehaviour
{
    public player pl;
    public Animator anim;
    public ambience amb;


    public void Teleport(float desx, float desy, bool instant, bool canMove)
    {
        StartCoroutine(TeleportPlayer(desx, desy, instant, canMove));
    }

    IEnumerator TeleportPlayer(float desx, float desy, bool instant, bool desMove)
    {
        pl.canMove = false;
        pl.GetComponent<BoxCollider2D>().enabled = desMove;
        pl.isTeleporting = true;

        if (instant)
        {
            anim.SetTrigger("instafade");
        }
        else
        {
            anim.SetTrigger("fadetrigger");
            yield return new WaitForSeconds(.25f);
        }

        pl.gameObject.transform.position = new Vector3(desx, desy);
        pl.GetComponent<SpriteRenderer>().enabled = desMove;
        amb.AmbUpd();

        yield return new WaitForSeconds(.25f);
        pl.isTeleporting = false;
        pl.canMove = desMove;
    }

}

