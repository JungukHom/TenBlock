// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class LobbySceneController : MonoBehaviour
{
    [Header("Public variables")]
    public int pageIndex = 0;

    [Header("Public Components")]
    public KeyBoardUIPannel roomListPannel;

    [Header("Public GameObjects")]
    public Transform parent;

    // private variables
    private int pageElementCount = 8;

    private void Awake()
    {
        AddListeners();
        RefreshRoomList();
    }

    private void AddListeners()
    {
        InputHandler.OnRKeyPressed += RefreshRoomList;
        InputHandler.OnCKeyPressed += CreateRandomRoom;
        InputHandler.OnLeftKeyPressed += PageDown;
        InputHandler.OnRightKeyPressed += PageUp;
    }

    private void DeleteListeners()
    {
        InputHandler.OnRKeyPressed -= RefreshRoomList;
        InputHandler.OnCKeyPressed -= CreateRandomRoom;
        InputHandler.OnLeftKeyPressed -= PageDown;
        InputHandler.OnRightKeyPressed -= PageUp;
    }

    public void RefreshRoomList()
    {
        LoadingPannel.Controller.SetActive(true);
        LoadingPannel.Controller.SetMessage("Refreshing room list");

        pageIndex = 0;
        InvalidateListView(pageIndex);
    }

    public void CreateRandomRoom()
    {
        LoadingPannel.Controller.SetActive(true);
        LoadingPannel.Controller.SetMessage("Creating new room");
        SceneLoader.LoadScene(SceneName.Room);
        RoomSceneController.OnRoomSceneLoaded += () =>
        {
            TenBlockManager.Controller.CreateAndJoinRandomRoom();
        };
    }

    private void InvalidateListView(int pageIndex)
    {
        if (PhotonNetwork.countOfRooms == 0)
            return;

        KeyBoardButton firstButton = null;
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        RoomElementButton _previousElement = null;

        int startIndex = pageIndex * pageElementCount;
        int endIndex = 0;
        if (pageIndex == (PhotonNetwork.countOfRooms / pageElementCount) + 1)
            endIndex = PhotonNetwork.countOfRooms % pageElementCount;
        else
            endIndex = roomList.Length >= pageElementCount ? startIndex * (pageElementCount + 1) : roomList.Length;

        for (int i = startIndex; i < endIndex; i++)
        {
            RoomInfo info = roomList[i];
            RoomElementButton _roomElement = RoomElementButton.Create(info.Name, info.PlayerCount, info.MaxPlayers);
            _roomElement.onClick = () =>
            {
                ShowJoinRoomMessageBox(info.Name);
            };
            _roomElement.SetParent(parent);
            if (i == 0)
            {
                firstButton = _roomElement;
                _previousElement = _roomElement;
            }
            else
            {
                _previousElement.btn_down = _roomElement;
                _roomElement.btn_up = _previousElement;
            }
        }

        roomListPannel.SetDefaultSelectedButton(firstButton);

        LoadingPannel.Controller.SetActive(false);
    }

    private void ShowJoinRoomMessageBox(string roomName)
    {
        MessagePopup.Show("Notice", $"Join room [{roomName}]?", () =>
        {
            if (PhotonNetwork.insideLobby && !PhotonNetwork.inRoom)
            {
                DeleteListeners();
                SceneLoader.LoadScene(SceneName.Room);
                RoomSceneController.OnRoomSceneLoaded += () => { TenBlockManager.Controller.JoinRoom(roomName); };
            }
        });
    }

    private void PageDown(MoveDirection direction)
    {
        pageIndex--;
        pageIndex = pageIndex.Clamp(0, PhotonNetwork.countOfRooms / 8 + 1);
    }

    private void PageUp(MoveDirection direction)
    {
        pageIndex++;
        pageIndex = pageIndex.Clamp(0, PhotonNetwork.countOfRooms / 8 + 1);
    }
}