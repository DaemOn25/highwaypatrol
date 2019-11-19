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

    public void Sign()
    {
        SceneManager.LoadScene("Authentication");
    }

    public void Instruct()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
