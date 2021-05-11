// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class EscapePannel : MonoBehaviour
{
    public static bool isActived = false;

    public Transform pnl_escape;

    public void SetActive(bool active)
    {
        if (active)
            OnFocus();
        else
            OnOutFocus();
    }

    public void OnFocus()
    {
        isActived = true;
        pnl_escape.gameObject.SetActive(true);
    }

    public void OnOutFocus()
    {
        isActived = false;
        pnl_escape.gameObject.SetActive(false);
    }
}