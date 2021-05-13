// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Block : MonoBehaviour, IPunObservable
{
    public Text txt_number;
    public int x = 0;
    public int y = 0;

    public int number = 0;

    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
        number = UnityEngine.Random.Range(1, 9);
    }

    public void Initialize(int x, int y, int number)
    {
        Initialize(x, y);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref number);
        }
        else
        {
            stream.Serialize(ref number);
            txt_number.text = number.ToString();
        }
    }
}