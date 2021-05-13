// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class MessagePopup : MonoBehaviour
{
    private static readonly string Path = "UI/MessagePopup";

    public static MessagePopup Show(string title, string content, Action okCallback = null, Action cancelCallback = null)
    {
        GameObject prefab = Resources.Load(Path) as GameObject;
        GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        MessagePopup component = go.GetComponent<MessagePopup>();
        component.InitializeWith(title, content, okCallback, cancelCallback);

        return component;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public Text txt_title;
    public Text txt_content;

    public Button btn_ok;
    public Button btn_cancel;

    public Action OkClickCallback = null;
    public Action CancelClickCallback = null;

    private void Awake()
    {
        AddListeners();
    }

    public void InitializeWith(string title, string content, Action okCallback = null, Action cancelCallback = null)
    {
        txt_title.text = title;
        txt_content.text = content;

        OkClickCallback = okCallback;
        CancelClickCallback = cancelCallback;
    }

    private void AddListeners()
    {
        btn_ok.onClick.AddListener(OnOkButtonClicked);
        btn_cancel.onClick.AddListener(OnCancelButtonClicked);
    }

    private void DeleteListeners()
    {
        //btn_ok.onClick -= OnOkButtonClicked;
        //btn_cancel.onClick -= OnCancelButtonClicked;
    }

    private void OnOkButtonClicked()
    {
        OkClickCallback?.Invoke();
        DeleteListeners();
        Destroy(gameObject);
    }

    private void OnCancelButtonClicked()
    {
        CancelClickCallback?.Invoke();
        DeleteListeners();
        Destroy(gameObject);
    }
}