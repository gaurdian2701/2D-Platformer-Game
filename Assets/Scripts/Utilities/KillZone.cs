﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public static Action PlayerFallen;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerFallen.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
