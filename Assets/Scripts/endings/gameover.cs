using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gameover : MonoBehaviour
{
    public AudioManager aud;
    public GameObject deathScreen, top, bottom;
    public string deathSound1 = "d1", deathSound2 = "d2";
    public UnityEvent startEvent, finishEvent;


    public void GameOver()
    {
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        startEvent.Invoke();
        deathScreen.SetActive(true);
        top.SetActive(true);
        bottom.SetActive(true);
        aud.Play(deathSound1);
        yield return new WaitForSeconds(1f);
        aud.Play(deathSound2);
        top.GetComponent<Rigidbody2D>().simulated = true;
        top.GetComponent<Rigidbody2D>().velocity = new Vector3(2f, 4f, 0);
        Destroy(top, 3f);
        bottom.GetComponent<Rigidbody2D>().simulated = true;
        bottom.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, 4f, 0);
        Destroy(bottom, 3f);
        yield return new WaitForSeconds(3f);
        finishEvent.Invoke();
        yield return new WaitForSeconds(.25f);
        deathScreen.SetActive(false);
    }
}