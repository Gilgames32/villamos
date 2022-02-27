using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambience : MonoBehaviour
{
    public AudioSource source;
    public player pl;
    public Sound[] amb;

    //this is utterly fucking retarded
    public void AmbUpd()
    {
        if ( -12 <= pl.gameObject.transform.position.x && pl.gameObject.transform.position.x <= 12 && -10 <= pl.gameObject.transform.position.y && pl.gameObject.transform.position.y <= 10)
        {
            source.enabled = true;
            source.clip = amb[0].clip;
            source.volume = amb[0].volume;
            source.Play();
        }
        else if (20 <= pl.gameObject.transform.position.x && pl.gameObject.transform.position.x <= 30 && -10 <= pl.gameObject.transform.position.y && pl.gameObject.transform.position.y <= 10)
        {
            source.enabled = true;
            source.clip = amb[1].clip;
            source.volume = amb[1].volume;
            source.Play();
        }
        else if (45 <= pl.gameObject.transform.position.x && pl.gameObject.transform.position.x <= 55 && -10 <= pl.gameObject.transform.position.y && pl.gameObject.transform.position.y <= 10)
        {
            source.enabled = true;
            source.clip = amb[2].clip;
            source.volume = amb[2].volume;
            source.Play();
        }
        else
        {
            source.enabled = false;
        }
    }
}
