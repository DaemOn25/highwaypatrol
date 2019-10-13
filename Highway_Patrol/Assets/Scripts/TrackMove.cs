using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackMove : MonoBehaviour
{
    public float speed = 3f;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
            speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset; 
    }
}
