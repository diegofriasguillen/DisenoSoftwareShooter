using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InitialMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
