using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMute : MonoBehaviour
{

    public AudioManager am;

    public Sprite mute;
    public Sprite vol;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSprite()
    {
       
        if(GetComponent<Image>().sprite == vol)
        {
            GetComponent<Image>().sprite = mute;
            am.sound.Pause();
        }
        else
        {
            GetComponent<Image>().sprite = vol;
            am.sound.Play();
        }
    }
}
