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
    }

    public void Initialize(int number)
    {
        this.number = number;
    }

    public void ShowText()
    {
        txt_number.text = number.ToString();
    }

    public void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public static int operator +(Block a, Block b)
    {
        return a.number + b.number;
    }

    public static int AddAll(Block[] blocks)
    {
        int result = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            result += blocks[i].number;
        }

        return result;
    }
}