// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
using ExitGames.Client.Photon;

// Alias

public class TenBlockManager : MonoBehaviour, IPunCallbacks
{
    public static TenBlockManager Controller { get; set; }

    [Header("Public PUN Variables")]
    public string GameVersion = "0.1";

    [Header("Public UI Variables")]
    public bool isPopuped = false;

    [Header("Escape Popup")]
    public EscapePannel pnl_escape;

    private void Awake()
    {
        Controller = this;
    }

    private void Start()
    {
        PhotonNetwork.sendRateOnSerialize = 20;
    }

    #region PUN
    public bool ConnectUsingSettings()
    {
        return PhotonNetwork.ConnectUsingSettings(GameVersion);
    }

    public bool JoinLobby()
    {
        return PhotonNetwork.JoinLobby();
    }

    public bool JoinRoom(string roomName)
    {
        return PhotonNetwork.JoinRoom(roomName);
    }

    public bool CreateAndJoinRandomRoom()
    {
        int number = UnityEngine.Random.Range(10000, 99999);
        return PhotonNetwork.JoinOrCreateRoom(number.ToString(),
            new RoomOptions()
            {
                IsVisible = true,
                MaxPlayers = 2,
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
                {
                    { "RoomState", "Waiting" }
                },
                CustomRoomPropertiesForLobby = new string[] { "RoomState" }
            }, TypedLobby.Default);
    }
    #endregion


    #region UI
    public bool SetPopupedState(bool state)
    {
        isPopuped = state;
        return isPopuped;
    }

    public void SetEscapeUIVisible(bool visible)
    {
        pnl_escape.SetActive(visible);
    }
    #endregion

    #region PUN Callbacks
    public void OnConnectedToMaster()
    {
        Logger.Log("Connected to master");
        LoadingPannel.Controller.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinedLobby()
    {
        Logger.Log("Joined lobby");
        PhotonNetwork.player.NickName = UnityEngine.Random.Range(10000, 99999).ToString();
        LoadingPannel.Controller.SetActive(false);
        SceneLoader.LoadScene(SceneName.Lobby);
    }

    public void OnJoinedRoom()
    {
        Logger.Log("Joined room");
        LoadingPannel.Controller.SetActive(false);
        //SceneLoader.LoadScene(SceneName.Game);
        GameObject _roomPlayer = PhotonNetwork.Instantiate(RoomPlayer.Path, Vector3.zero, Quaternion.identity, 0);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Logger.Log($"Player connected ({newPlayer.NickName})");
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Logger.Log($"Player disconnected ({otherPlayer.NickName})");
    }
    #endregion

    #region PUN Callbacks (Not used)
    public void OnConnectedToPhoton()
    {
        // do nothing
    }

    public void OnLeftRoom()
    {
        // do nothing
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        // do nothing
    }

    public void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        // do nothing
    }

    public void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        // do nothing
    }

    public void OnCreatedRoom()
    {
        // do nothing
    }

    public void OnLeftLobby()
    {
        // do nothing
    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        // do nothing
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
        // do nothing
    }

    public void OnDisconnectedFromPhoton()
    {
        // do nothing
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // do nothing
    }

    public void OnReceivedRoomListUpdate()
    {
        // do nothing
    }

    public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        // do nothing
    }

    public void OnPhotonMaxCccuReached()
    {
        // do nothing
    }

    public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // do nothing
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        // do nothing
    }

    public void OnUpdatedFriendList()
    {
        // do nothing
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        // do nothing
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        // do nothing
    }

    public void OnWebRpcResponse(OperationResponse response)
    {
        // do nothing
    }

    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
        // do nothing
    }

    public void OnLobbyStatisticsUpdate()
    {
        // do nothing
    }

    public void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
    {
        // do nothing
    }

    public void OnOwnershipTransfered(object[] viewAndPlayers)
    {
        // do nothing
    }
    #endregion
}