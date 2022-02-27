using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class langdbase : MonoBehaviour
{
    public bool hu;
    public langline[] langdb;


    private void Awake()
    {
        //0 is hu, 1 is eng
        hu = PlayerPrefs.GetInt("lang", 0) == 0;
    }

    public void SwitchLang()
    {
        if (hu)
        {
            PlayerPrefs.SetInt("lang", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lang", 0);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //stands for language return array 
    public string[] Lanra(string key)
    {
        if (key == "")
        {
            return new string[]{""};
        }
        string[] r;
        if (hu)
        {
            try
            {
                r = Array.Find(langdb, item => item.key == key).hu;
            }
            catch (Exception)
            {
                print("MISSING KEY:    " + key);
                return new string[] { "" };
                throw;
            }
            
        }
        else
        {
            try
            {
                r = Array.Find(langdb, item => item.key == key).en;
            }
            catch (Exception)
            {
                print("MISSING KEY:    " + key);
                return new string[] { "" };
                throw;
            }
        }
        return r;
    }
}
