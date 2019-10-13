using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Button[] buttons;
    public Text scoreText;
    public Text aliasText;
    public GameObject exitDestroy;     //making an instance of another gameobject
    public GameObject pauseText;       //text change of pause
    public AudioManager am;            // object for audio manager class
    public TrackMove tm;              //object for track move class
    public CarSpawner cs;             //object for carspawner class

    string screenName;

    bool gameover;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentscreen = SceneManager.GetActiveScene();
        screenName = currentscreen.name;

        exitDestroy.SetActive(false);     //setting the other gameObject inactive
        score = 0;
        gameover = false;
        InvokeRepeating("ScoreUpdate", 1.0f, 0.5f);

        aliasText.text = "ALIAS: " + AuthManager.playerName;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

    void ScoreUpdate()
    {
        if (score >= 50)
        {
            tm.speed = 5f;
            cs.delay_timer = 0.6f;
                
        }

        if (score >= 100)
        {
            SceneManager.LoadScene("FINAL");
        }

        if (!gameover)
        {
            score += 1;
        }
    }

    public void GameOver()
    {
        AuthManager.PostToDatabase();
        exitDestroy.SetActive(true);  //setting other game object active
        gameover = true;
        foreach(Button button in buttons)   //loop for arrays(will itterate till last element of array 
        {
            button.gameObject.SetActive(true);   //accessing gameobjects attached to the index and setting them active.
                                                 //this will active all the buttons when this function is called
        }
    }

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

    public void Exit()
    {
        Application.Quit();
    }

    public void Sign()
    {
        SceneManager.LoadScene("Authentication");
    }

}
