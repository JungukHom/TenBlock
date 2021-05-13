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
        if (PhotonNetwork.inRoom)
        {
            isOtherPlayerReady = PhotonNetwork.room.PlayerCount >= 2 && txt_player_number_right.text == "Ready";
        }
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

    public void SetPlayerUI(bool isLeft, bool isReady, string playerName)
    {
        pnl_player_left.SetActive(false);
        pnl_player_right.SetActive(false);

        if (isLeft)
        {
            pnl_player_left.SetActive(true);
            txt_player_ready_left.text = (isReady ? "Ready" : "Not Ready");
            txt_player_name_left.text = playerName;
        }
        else
        {
            pnl_player_right.SetActive(true);
            txt_player_ready_right.text = (isReady ? "Ready" : "Not Ready");
            txt_player_name_right.text = playerName;
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
        }
        roomPlayer.ToggleReady();
    }
}