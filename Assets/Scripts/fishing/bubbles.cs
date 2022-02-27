using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbles : MonoBehaviour
{
    public GameObject bubble;
    public float frequency;

    private void Start()
    {
        StartCoroutine(SpawnBubbes(frequency));
    }

    IEnumerator SpawnBubbes(float delay)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(delay);
            GameObject newbubble = Instantiate(bubble, gameObject.transform, false);
            newbubble.transform.position += new Vector3(Random.Range(-5f, 5f), -2, 0);
            newbubble.SetActive(true);
            Destroy(newbubble, 2);
        }
        
    }
}
