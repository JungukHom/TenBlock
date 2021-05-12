// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class LoadingPannel : MonoBehaviour
{
    public static LoadingPannel Controller = null;
    public static bool isActived = false;

    public Transform pnl_loading;
    public Text txt_message;
    public Text txt_dot;

    private WaitForSeconds wfs = new WaitForSeconds(1.0f);
    private string[] dotArray = new string[]
    {
        "¡¤    ", "  ¡¤  ", "    ¡¤"
    };

    private void Awake()
    {
        Controller = this;
    }

    public void SetActive(bool active)
    {
        if (active)
            OnFocus();
        else
            OnOutFocus();
    }

    public void SetMessage(string message)
    {
        txt_message.text = message;
    }

    public void OnFocus()
    {
        isActived = true;
        pnl_loading.gameObject.SetActive(true);
        StartCoroutine(Co_DotIterator());
    }

    public void OnOutFocus()
    {
        isActived = false;
        pnl_loading.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator Co_DotIterator()
    {
        while (isActived)
        {
            for (int i = 0; i < dotArray.Length; i++)
            {
                txt_dot.text = dotArray[i];
                yield return wfs;
            }
        }
    }
}