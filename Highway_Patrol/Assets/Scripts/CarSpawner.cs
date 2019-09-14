using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    int carno;
    public float maxpos = 2.15f;
    public float delay_timer = 0.8f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentscreen = SceneManager.GetActiveScene();

        if (currentscreen.name == "Level1")
        {
            delay_timer = 0.8f;
        }

        else if (currentscreen.name == "Level2")
        {
            delay_timer = 0.6f;
        }

        timer = delay_timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Vector3 carpose = new Vector3(Random.Range(-maxpos, maxpos), transform.position.y, transform.position.z);
            carno = Random.Range(0,5);
            Instantiate(cars[carno], carpose, transform.rotation);
            timer = delay_timer;
        }

        
    }
}
