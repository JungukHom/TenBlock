// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Test : MonoBehaviour
{
    public GameObject cube;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Instantiate(cube, new Vector3(j, 0, i), Quaternion.identity);
            }
        }
    }
}
