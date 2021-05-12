// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class RoomElementButton : KeyBoardButton
{
    private static readonly string Path = "UI/RoomElement";

    public Text txt_roomName;
    public Text txt_playerCount;

    public static RoomElementButton Create(string roomName, int connectedPlayerCount, int maxPlayerCount)
    {
        GameObject _prefab = Resources.Load(Path) as GameObject;
        GameObject _gameObject = Instantiate(_prefab);
        RoomElementButton _element = _gameObject.GetComponent<RoomElementButton>();
        _element.InitializeWith(roomName, connectedPlayerCount, maxPlayerCount);
        _element.onClick += () =>
        {
            LoadingPannel.Controller.SetActive(true);
            LoadingPannel.Controller.SetMessage("Connecting room...");
        };

        return _element;
    }

    public void InitializeWith(string roomName, int connectedPlayerCount, int maxPlayerCount)
    {
        txt_roomName.text = roomName;
        txt_playerCount.text = $"{connectedPlayerCount} / {maxPlayerCount}";
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}