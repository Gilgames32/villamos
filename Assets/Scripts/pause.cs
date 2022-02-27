using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public player pl;
    public Slider mslider;
    public float volume;
    public bool paused;
    public GameObject pui, vui;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("volume", 1);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape") && pl.enabled)
        {
            if (paused)
            {
                paused = false;
                pui.SetActive(false);
                vui.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                pui.SetActive(true);
                vui.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void UpdateSliders()
    {
        volume = mslider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);

    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
