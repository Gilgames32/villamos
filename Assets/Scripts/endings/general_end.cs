using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class general_end : MonoBehaviour
{
    public player pl;
    public UnityEvent setupEvent, triggerEvent, endEvent;

    private void Start()
    {
        pl.ResetDir(false);
        setupEvent.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerEvent.Invoke();
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj, 5);
    }

    public void EnableMovement(float waitTime)
    {
        StartCoroutine(EnableMovementC(waitTime));
    }

    IEnumerator EnableMovementC(float waitTime) //g5f
    {
        if (waitTime <= .5f)
        {
            yield return new WaitForSeconds(.5f);
            pl.canMove = true;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            pl.canMove = false;
            yield return new WaitForSeconds(waitTime - .5f);
            pl.canMove = true;
        }
        
    }

    public void EndCutscene(float waitTime)
    {
        StartCoroutine(EndCutsceneC(waitTime));
    }
    IEnumerator EndCutsceneC(float waitTime) //g4f
    {
        yield return new WaitForSeconds(waitTime);
        endEvent.Invoke();
    }

}
