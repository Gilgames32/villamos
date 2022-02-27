using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public langdbase lan;
    public player pl;
    public Text ytext;
    public bool eng;
    public string[] yellowText;

    private void Start()
    {
        yellowText = lan.Lanra("yellowtext");
        ytext.text = yellowText[Random.Range(0, yellowText.Length)];
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    public void StartGame()
    {
        pl.enabled = true;
        gameObject.SetActive(false);
    }
}
