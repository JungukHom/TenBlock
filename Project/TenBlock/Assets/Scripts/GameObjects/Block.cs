// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Block : MonoBehaviour
{
    public Text txt_number;

    private int number = 0;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        number = UnityEngine.Random.Range(1, 9);
        txt_number.text = number.ToString();
    }

    public void Initialize(int number)
    {
        this.number = number;
        txt_number.text = number.ToString();
    }
}