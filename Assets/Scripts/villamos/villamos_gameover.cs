using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villamos_gameover : MonoBehaviour
{
    public AudioManager aud;
    public GameObject deathScreen, top, bottom;
    public villamos_palack pl;
    public villamos_boss boss;
    public villamos_attack attacks;
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
        pl.GetComponent<Rigidbody2D>().velocity = new Vector3();
        pl.enabled = false;
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
        //end.VillamosDeath();

    }
}
