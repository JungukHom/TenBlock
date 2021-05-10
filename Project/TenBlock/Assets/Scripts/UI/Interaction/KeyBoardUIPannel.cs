// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class KeyBoardUIPannel : MonoBehaviour
{
    public KeyBoardButton defaultSelectedButton;

    private void Start()
    {
        defaultSelectedButton.OnSelected();
    }
}
