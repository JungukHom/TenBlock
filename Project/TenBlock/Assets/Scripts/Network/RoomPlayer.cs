// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class RoomPlayer : MonoBehaviour, IPunObservable
{
    public static readonly string Path = "GameObject/RoomPlayer";

    [Header("Public variables")]
    public RoomSceneController controller;

    // private photon serializable variables
    public bool isReady = false;
    public string playerName = "";

    private void Start()
    {
        controller = FindObjectOfType<RoomSceneController>();
        controller.roomPlayer = this;
        controller.roomPlayerView = GetComponent<PhotonView>();

        playerName = PhotonNetwork.player.NickName;
    }

    public void ToggleReady()
    {
        isReady = !isReady;
        Debug.Log($"set is ready {isReady}");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("123");
        if (stream.isWriting)
        {
            bool _isReady = isReady;
            string _playerName = playerName;

            stream.Serialize(ref _isReady);
            stream.Serialize(ref _playerName);
        }
        else
        {
            bool _isReady = false;
            string _playerName = "";

            stream.Serialize(ref _isReady);
            stream.Serialize(ref _playerName);

            controller.SetPlayerUI(PhotonNetwork.isMasterClient, _isReady, _playerName);
        }
    }

    [PunRPC]
    public void GotoGameScene()
    {
        GameSceneController.OnGameSceneLoaded += () =>
        {
            PhotonNetwork.Instantiate(TenBlockPlayer.Path, Vector3.zero, Quaternion.identity, 0);
        };
        SceneLoader.LoadScene(SceneName.Game);
    }
}