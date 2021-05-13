// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class RoomSceneController : Photon.MonoBehaviour
{
    public static Action OnRoomSceneLoaded = null;

    [Header("Room info")]
    public Text txt_room_player_number;

    [Header("Player Left")]
    public GameObject pnl_player_left;
    public Text txt_player_number_left;
    public Text txt_player_name_left;
    public Text txt_player_ready_left;

    [Header("Player Right")]
    public GameObject pnl_player_right;
    public Text txt_player_number_right;
    public Text txt_player_name_right;
    public Text txt_player_ready_right;

    [Header("Button")]
    public Button btn_exit;
    public Button btn_ready_start;

    [Header("PhotonNetwork")]
    public RoomPlayer roomPlayer;
    public RoomPlayer otherRoomPlayer;
    public PhotonView roomPlayerView;

    // used only i am masterclient
    public bool isOtherPlayerReady = false;

    private void Awake()
    {
        OnRoomSceneLoaded?.Invoke();
        OnRoomSceneLoaded = null;

        AddListeners();
        Initialize();
    }

    private void Update()
    {
        if (!roomPlayer || !roomPlayerView)
            return;

        if (PhotonNetwork.inRoom && roomPlayer != null)
        {
            txt_room_player_number.text = $"{PhotonNetwork.room.PlayerCount} / {PhotonNetwork.room.MaxPlayers}";

            isOtherPlayerReady = GetIsOtherPlayerReady();

            if (PhotonNetwork.isMasterClient)
            {
                if (PhotonNetwork.room.PlayerCount == 1)
                {
                    txt_player_ready_left.text = (roomPlayer.isReady ? "Ready" : "Not Ready");
                    txt_player_name_left.text = roomPlayer.playerName;
                }
                else if (PhotonNetwork.room.PlayerCount == 2)
                {
                    txt_player_ready_left.text = (roomPlayer.isReady ? "Ready" : "Not Ready");
                    txt_player_name_left.text = roomPlayer.playerName;

                    if (otherRoomPlayer)
                    {
                        txt_player_ready_right.text = (otherRoomPlayer.isReady ? "Ready" : "Not Ready");
                        txt_player_name_right.text = otherRoomPlayer.playerName;
                    }
                }
            }
            else
            {
                if (PhotonNetwork.room.PlayerCount == 2)
                {
                    if (otherRoomPlayer)
                    {
                        txt_player_ready_left.text = (otherRoomPlayer.isReady ? "Ready" : "Not Ready");
                        txt_player_name_left.text = otherRoomPlayer.playerName;
                    }

                    txt_player_ready_right.text = (roomPlayer.isReady ? "Ready" : "Not Ready");
                    txt_player_name_right.text = roomPlayer.playerName;
                }
            }
        }
    }

    private bool GetIsOtherPlayerReady()
    {
        if (PhotonNetwork.room.PlayerCount == 2 && otherRoomPlayer != null)
            return otherRoomPlayer.isReady;
        else
            return false;
    }

    private void AddListeners()
    {
        btn_exit.onClick.AddListener(delegate { OnExitButtonPressed(); });
        btn_ready_start.onClick.AddListener(delegate { ReadyOrStart(); });
    }

    private void Initialize()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Logger.Log("I am master client");
        }
        else
        {
            Logger.Log("I am client");
        }
    }

    private void OnExitButtonPressed()
    {
        LoadingPannel.Controller.SetActive(true);
        LoadingPannel.Controller.SetMessage("Exiting room");
        PhotonNetwork.LeaveRoom();
    }

    private void ReadyOrStart()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (isOtherPlayerReady)
            {
                roomPlayerView.RPC("GotoGameScene", PhotonTargets.All);
            }
            else
            {
                MessagePopup.Show("Notice", "Other player is not ready");
            }
        }
        else
        {
            roomPlayer.ToggleReady();
        }
    }
}