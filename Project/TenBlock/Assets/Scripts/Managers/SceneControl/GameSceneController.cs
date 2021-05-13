// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class GameSceneController : MonoBehaviour
{
    public static Action OnGameSceneLoaded = null;
    public static GameSceneController Controller = null;

    public Text txt_score;
    public Button btn_exit;

    public GameObject blockPrefab;
    public Block[,] blocks = new Block[15, 10];

    public TenBlockPlayer localPlayer;
    public TenBlockPlayer otherPlayer;

    public int score = 0;

    private void Awake()
    {
        Invoke(nameof(InvokeCallback), 1.0f);
        Controller = this;

        if (PhotonNetwork.isMasterClient)
        {
            RoomPlayer[] roomPlayers = FindObjectsOfType<RoomPlayer>();
            for (int i = roomPlayers.Length - 1; i >= 0; i--)
            {
                PhotonNetwork.Destroy(roomPlayers[i].GetComponent<PhotonView>());
            }
        }

        if (PhotonNetwork.inRoom)
        {
            ExitGames.Client.Photon.Hashtable roomProperties = PhotonNetwork.room.CustomProperties;
            roomProperties["RoomState"] = "Playing";
            PhotonNetwork.room.SetCustomProperties(roomProperties);
        }

        btn_exit.onClick.AddListener(OnExitButtonClicked);
    }

    public void DeleteBlock()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Delete Block");
            Debug.Log($"master positon : x:{localPlayer.currentX}, y:{localPlayer.currentY}");
            Debug.Log($"other positon : x:{otherPlayer.currentX}, y:{otherPlayer.currentY}");

            int x1 = localPlayer.currentX;
            int x2 = otherPlayer.currentX;

            int y1 = localPlayer.currentY;
            int y2 = otherPlayer.currentY;

            if (x1 == x2) return;
            if (y1 == y2) return;

            if (x1 > x2) Swap(ref x1, ref x2);
            if (y1 > y2) Swap(ref y1, ref y2);

            List<Block> blockList = new List<Block>();
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    if (blocks[x, y] != null)
                    {
                        blockList.Add(blocks[x, y]);
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < blockList.Count; i++)
            {
                sum += blockList[i].number;
            }

            Debug.Log($"sum : {sum}");
            if (sum == 10)
            {
                for (int i = 0; i < blockList.Count; i++)
                {
                    PhotonNetwork.Destroy(blockList[i].GetComponent<PhotonView>());
                    blockList[i] = null;
                }
                int score = blockList.Count;
                blockList.Clear();

                localPlayer.photonView.RPC("AddScore", PhotonTargets.All, score);
            }

            // delete blocks
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        txt_score.text = $"Score : {this.score}";
    }

    private void Swap(ref int a, ref int b)
    {
        int temp = b;
        b = a;
        a = temp;
    }

    private void InvokeCallback()
    {
        OnGameSceneLoaded?.Invoke();
        OnGameSceneLoaded = null;

        if (PhotonNetwork.isMasterClient)
        {
            InstantiateBlocks();
        }
    }

    private void InstantiateBlocks()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject _cube = PhotonNetwork.Instantiate("GameObject/Block", new Vector3(j + 0.5f, i + 0.5f, 1), Quaternion.identity, 0);
                Block block = _cube.GetComponent<Block>();
                blocks[j, i] = block;
                block.Initialize(j, i);
                block.ShowText();
            }
        }
    }

    private void OnExitButtonClicked()
    {
        localPlayer.photonView.RPC("ExitRoom", PhotonTargets.All);
    }
}