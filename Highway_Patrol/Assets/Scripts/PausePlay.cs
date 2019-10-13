using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePlay : MonoBehaviour
{

    public AudioManager am;

    public Sprite paus;
    public Sprite playy;


    public void Pause()
    {
        //if (Time.timeScale == 1)
        //{
        //    Time.timeScale = 0;     
        //    GetComponent<Image>().sprite = playy;
       
        //    //pauseText.GetComponentInChildren<Text>().text = "l>";
        //    am.sound.Pause();
        //}
        //else if (Time.timeScale == 0)
        //{
        //    Time.timeScale = 1;
        //    GetComponent<Image>().sprite = playy;
            
        //    //pauseText.GetComponentInChildren<Text>().text = "ll";
        //    am.sound.Play();
        //}

        if(GetComponent<Image>().sprite == paus)
        {
            GetComponent<Image>().sprite = playy;
            Time.timeScale = 0;
            am.sound.Pause();
        }

        else if(GetComponent<Image>().sprite == playy)
        {
            GetComponent<Image>().sprite = paus;
            Time.timeScale = 1;
            am.sound.Play();
        }

    }

}
