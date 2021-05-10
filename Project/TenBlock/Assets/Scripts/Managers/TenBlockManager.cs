// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class TenBlockManager : MonoBehaviour
{
    public static TenBlockManager Controller { get; set; }

    // public variables
    public bool isEscaped = false;

    [Header("Escape Popup")]
    public GameObject pnl_escape;

    private void Awake()
    {
        Controller = this;
    }

    public bool ToggleEscapeState()
    {
        isEscaped = !isEscaped;
        SetEscapeUIVisible(isEscaped);
        return isEscaped;
    }

    public void SetEscapeUIVisible(bool visible)
    {
        pnl_escape.SetActive(visible);
    }
}
