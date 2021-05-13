// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class TenBlockPlayer : MonoBehaviour, IPunObservable
{
    //public static readonly string Path = "GameObject/TenBlockPlayer";
    public static readonly string Path = "GameObject/TenBlockPlayer";

    public SpriteRenderer spriteRenderer;

    public int currentX = 0;
    public int currentY = 0;

    public Transform _transform;
    public PhotonView photonView;

    private bool shift = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.isMine)
            GameSceneController.Controller.localPlayer = this;
        else
            GameSceneController.Controller.otherPlayer = this;

        if (photonView.isMine)
        {
            if (PhotonNetwork.isMasterClient)
                spriteRenderer.color = Utility.GetNormalizedColor(256, 64, 64);
            else
                spriteRenderer.color = Utility.GetNormalizedColor(64, 64, 256);
        }

    }

    private void Start()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            Initialize();
        }
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveGrid(MoveDirection.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveGrid(MoveDirection.Right);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveGrid(MoveDirection.Up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveGrid(MoveDirection.Down);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpaceBarPressed();
            }

            if (Input.GetKey(KeyCode.LeftShift))
                shift = true;
            else
                shift = false;
        }
    }

    private void Initialize()
    {
        CacheComponents();
        SetupPosition();
    }

    private void CacheComponents()
    {
        _transform = GetComponent<Transform>();
    }

    private void SetupPosition()
    {
        currentX = _transform.position.x.Round();
        currentY = _transform.position.y.Round();
    }

    private void MoveGrid(MoveDirection direction)
    {
        Vector3 _direction = Vector.DirectionToNormalizedVector(direction);
        int x = (currentX + _direction.x).Round();
        int y = (currentY + _direction.y).Round();

        x = x.Clamp(0, 15);
        y = y.Clamp(0, 10);

        if (shift)
        {
            if (direction == MoveDirection.Left)
                x = 0;
            if (direction == MoveDirection.Right)
                x = 15;
            if (direction == MoveDirection.Up)
                y = 10;
            if (direction == MoveDirection.Down)
                y = 0;
        }

        Vector3 _position = new Vector3(x, y, 0);
        transform.position = _position;
        SetupPosition();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref currentX);
            stream.Serialize(ref currentY);
        }
        else // stream.isReading
        {
            stream.Serialize(ref currentX);
            stream.Serialize(ref currentY);
        }
    }

    public void OnSpaceBarPressed()
    {
        photonView.RPC("DeleteBlocks", PhotonTargets.All);
    }

    [PunRPC]
    public void DeleteBlocks()
    {
        GameSceneController.Controller.DeleteBlock();
    }

    [PunRPC]
    public void AddScore(int score)
    {
        GameSceneController.Controller.AddScore(score);
    }

    [PunRPC]
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}