using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Button[] buttons;
    public Text scoreText;
    public GameObject exitDestroy;     //making an instance of another gameobject
    public GameObject pauseText;       //text change of pause
    public AudioManager am;            // object for audio manager class
    string screenName; 
     

    bool gameover;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentscreen = SceneManager.GetActiveScene();
        screenName = currentscreen.name;

        exitDestroy.SetActive(false);     //setting the other gameObject inactive
        score = 0;
        gameover = false;
        InvokeRepeating("ScoreUpdate", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;
      
    }

    void ScoreUpdate()
    {
        if (screenName == "Level1")
        {
            if (score >= 20)
            {
                SceneManager.LoadScene("Level2Intro");
            }
        }
        else if(screenName == "Level2")
        {
            if(score >= 10)
            {
                SceneManager.LoadScene("FINAL");
            }
        }

        if (!gameover)
        {
            score += 1;
        }
    }

    public void GameOver()
    {
        exitDestroy.SetActive(true);  //setting other game object active
        gameover = true;
        foreach(Button button in buttons)   //loop for arrays(will itterate till last element of array 
        {
            button.gameObject.SetActive(true);   //accessing gameobjects attached to the index and setting them active.
                                                 //this will active all the buttons when this function is called
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseText.GetComponentInChildren<Text>().text = "l>";
            am.sound.Pause();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseText.GetComponentInChildren<Text>().text = "ll";
            am.sound.Play();
        }
    }

    public void Level2Intro()
    {
        SceneManager.LoadScene("Level2Intro");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void HomeMenu()
    {
        SceneManager.LoadScene("Home_Menu");
    }

    public void Final()
    {
        SceneManager.LoadScene("FINAL");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
