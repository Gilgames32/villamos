using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villamos_attack : MonoBehaviour
{
    public villamos_boss boss;
    public GameObject enemy;

    private float timer;
    private bool countTime;
    private float durtime;


    private void Update()
    {
        if (countTime)
        {
            timer += Time.deltaTime;
            if (timer > durtime)
            {
                StopAllCoroutines();
                countTime = false;
                DestroyEnemies();
                boss.NextPhase();
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void StartAttcak(float speed, float frequency, float duration, int count)
    {
        StartCoroutine(SpawnAttack(speed, frequency, duration, count));
    }

    IEnumerator SpawnAttack(float speed, float frequency, float duration, int count)
    {
        durtime = duration;
        countTime = true;
        
        int line = Random.Range(0, 3);
        int lastline = line;
        while (timer < duration)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newEnemy = Instantiate(enemy, gameObject.transform, false);
                newEnemy.SetActive(true);
                newEnemy.tag = "Enemy";
                if (count == 3)
                {
                    line = i;
                }

                else do
                {
                    line = Random.Range(0, 3);
                } while (lastline == line);
                lastline = line;
                if (speed > 0)
                {
                    newEnemy.transform.position += new Vector3(-8, (line - 1) * 1.25f, 0);
                }
                else
                {
                    newEnemy.transform.position += new Vector3(8, (line - 1) * 1.25f, 0);
                    newEnemy.GetComponent<SpriteRenderer>().flipX = false;
                }
                
                newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);

                
            }
            yield return new WaitForSeconds(frequency);

        }
        countTime = false;
        DestroyEnemies();
        boss.NextPhase();
    }

    public void DestroyEnemies()
    {
        GameObject[] spawned = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in spawned)
        {
            Destroy(enemy);
        }
    }
}
