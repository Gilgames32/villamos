using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabla_attacks : MonoBehaviour
{
    public tabla_boss bossp, bossg;
    public GameObject[] enemies;
    
    private float timer;
    private bool countTime;


    private void Update()
    {
        if (countTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    public void StartAttcak(int[] enemyTypes, float speed, float frequency, float duration) 
    {
        StartCoroutine(SpawnAttack(enemyTypes, speed, frequency, duration));
    }

    IEnumerator SpawnAttack(int[] enemyTypes, float speed, float frequency, float duration) 
    {
        countTime = true;
        while(timer < duration)
        {
            GameObject newEnemy = Instantiate(enemies[enemyTypes[Random.Range(0, enemyTypes.Length)]], gameObject.transform, false);
            newEnemy.tag = "Enemy";
            newEnemy.transform.position += new Vector3(Random.Range(-5f, 4f), 0, 0);
            newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed, 0);
            
            yield return new WaitForSeconds(frequency);

        }
        countTime = false;
        DestroyEnemies();
        if (bossp.enabled)
        {
            bossp.NextPhase();
        }
        else if (bossg.enabled)
        {
            bossg.NextPhase();
        }


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
