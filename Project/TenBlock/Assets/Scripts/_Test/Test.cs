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
        GameObject temp = new GameObject();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject _cube = Instantiate(cube, new Vector3(j, i, 0), Quaternion.identity, temp.transform);
                Block block = _cube.GetComponent<Block>();
                block.Initialize(j, i);
            }
        }

        temp.transform.position = new Vector3(0.5f, 0.5f, 0);
    }
}
