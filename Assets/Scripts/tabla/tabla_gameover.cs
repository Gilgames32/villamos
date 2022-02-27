using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabla_gameover : MonoBehaviour
{
    public AudioManager aud;
    public GameObject deathScreen, top, bottom;
    public tabla_palack pl;
    public tabla_boss boss;
    public tabla_attacks attacks;
    public ending end;

    public void GameOver()
    {
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        attacks.StopAllCoroutines();
        attacks.DestroyEnemies();
        attacks.enabled = false;
        boss.enabled = false;
        pl.enabled = false;
        pl.GetComponent<Rigidbody2D>().velocity = new Vector3();
        deathScreen.SetActive(true);
        top.SetActive(true);
        bottom.SetActive(true);
        aud.Play("d1");
        yield return new WaitForSeconds(1f);
        aud.Play("d2");
        top.GetComponent<Rigidbody2D>().simulated = true;
        top.GetComponent<Rigidbody2D>().velocity = new Vector3(2f, 4f, 0);
        Destroy(top, 3f);
        bottom.GetComponent<Rigidbody2D>().simulated = true;
        bottom.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, 4f, 0);
        Destroy(bottom, 3f);
        yield return new WaitForSeconds(3f);
        end.End("");

    }
}
