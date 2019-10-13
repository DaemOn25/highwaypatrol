using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void Level()
    {
        SceneManager.LoadScene("Level");
    }


    public void HomeMenu()
    {
        SceneManager.LoadScene("Home_Menu");
    }

    public void Final()
    {
        SceneManager.LoadScene("FINAL");
    }

    public void Sign()
    {
        SceneManager.LoadScene("Authentication");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
