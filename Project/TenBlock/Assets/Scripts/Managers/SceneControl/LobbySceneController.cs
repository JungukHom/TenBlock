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

    [Header("Public GameObjects")]
    public Transform parent;

    [Header("Buttons")]
    public Button btn_create;
    public Button btn_refresh;

    // private variables
    private int pageElementCount = 8;

    private void Awake()
    {
        RefreshRoomList();

        btn_create.onClick.AddListener(CreateRandomRoom);
        btn_refresh.onClick.AddListener(RefreshRoomList);
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
        for (int i = parent.childCount - 1; i >= 0; i--)
            Destroy(parent.GetChild(i).gameObject);

        if (PhotonNetwork.countOfRooms == 0)
            return;

        RoomInfo[] roomList = PhotonNetwork.GetRoomList();

        int startIndex = pageIndex * pageElementCount;
        int endIndex = 0;
        if (pageIndex == (PhotonNetwork.countOfRooms / pageElementCount) + 1)
            endIndex = PhotonNetwork.countOfRooms % pageElementCount;
        else
            endIndex = roomList.Length >= pageElementCount ? startIndex * (pageElementCount + 1) : roomList.Length;

        for (int i = startIndex; i < endIndex; i++)
        {
            RoomInfo info = roomList[i];
            string roomState = info.CustomProperties["RoomState"].ToString();
            RoomElementButton _roomElement = RoomElementButton.Create(info.Name, roomState, info.PlayerCount, info.MaxPlayers);
            _roomElement.onClick = () =>
            {
                ShowJoinRoomMessageBox(info.Name);
            };
            _roomElement.SetParent(parent);
        }

        LoadingPannel.Controller.SetActive(false);
    }

    private void ShowJoinRoomMessageBox(string roomName)
    {
        MessagePopup.Show("Notice", $"Join room [{roomName}]?", () =>
        {
            if (PhotonNetwork.insideLobby && !PhotonNetwork.inRoom)
            {
                SceneLoader.LoadScene(SceneName.Room);
                RoomSceneController.OnRoomSceneLoaded += () => { TenBlockManager.Controller.JoinRoom(roomName); };
            }
        });
    }
}