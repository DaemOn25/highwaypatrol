﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcarDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyCar")
        {
            Destroy(col.gameObject);
        }
    }
}
