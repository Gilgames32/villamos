using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backtomenu : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
