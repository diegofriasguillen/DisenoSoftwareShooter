using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InitialMenu : MonoBehaviour
{
    public GameObject level2Button;
    private void Start()
    {
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            level2Button.SetActive(true);
        }
        else
        {
            level2Button.SetActive(false);
        }
    }


    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
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
