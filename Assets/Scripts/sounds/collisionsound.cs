using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionsound : MonoBehaviour
{
    private AudioSource sauce;
    [Range(0f, 1f)]
    public float pitchVariance = .25f;

    private void Start()
    {
        sauce = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sauce.PlayOneShot(sauce.clip);
        sauce.pitch = (1f + Random.Range(-pitchVariance / 2f, pitchVariance / 2f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sauce.PlayOneShot(sauce.clip);
        sauce.pitch = (1f + Random.Range(-pitchVariance / 2f, pitchVariance / 2f));
    }
}
