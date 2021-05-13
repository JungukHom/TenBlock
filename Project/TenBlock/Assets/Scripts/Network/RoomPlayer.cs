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
    public PhotonView photonView;

    // private photon serializable variables
    public bool isReady = false;
    public string playerName = "";

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controller = FindObjectOfType<RoomSceneController>();

        if (photonView.isMine)
        {
            controller.roomPlayer = this;
            controller.roomPlayerView = GetComponent<PhotonView>();

            if (PhotonNetwork.isMasterClient)
            {
                isReady = true;
                playerName = PhotonNetwork.masterClient.NickName;
            }
            else
            {
                isReady = false;
                playerName = PhotonNetwork.player.NickName;
            }
        }
        else
        {
            controller.otherRoomPlayer = this;
        }
    }

    public void ToggleReady()
    {
        isReady = !isReady;
        Debug.Log($"set is ready {isReady}");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
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

            //controller.SetPlayerUI(PhotonNetwork.isMasterClient, _isReady, _playerName);
            isReady = _isReady;
            playerName = _playerName;
        }
    }

    private Vector3 masterClientPosition = new Vector3(4f, 5f, 0);
    private Vector3 otherClientPosition = new Vector3(11f, 5f, 0);

    [PunRPC]
    public void GotoGameScene()
    {
        GameSceneController.OnGameSceneLoaded += () =>
        {
            Vector3 targetPosition = PhotonNetwork.isMasterClient ? masterClientPosition : otherClientPosition;
            PhotonNetwork.Instantiate(TenBlockPlayer.Path, targetPosition, Quaternion.identity, 0);
        };
        SceneLoader.LoadScene(SceneName.Game);
    }
}