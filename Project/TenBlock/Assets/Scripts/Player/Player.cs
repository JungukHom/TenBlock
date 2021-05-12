// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Player : MonoBehaviour, IPunObservable
{
    //public static readonly string Path = "GameObject/TenBlockPlayer";
    public static readonly string Path = "GameObject/TenBlockPlayer";

    private int currentX = 0;
    private int currentY = 0;

    private Transform _transform;

    private void Start()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        InputHandler.OnESCKeyPressed += ToggleKeyBoardEvent;

        CacheComponents();
        SetupPosition();
        AddListeners();
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

    private void AddListeners()
    {
        ToggleKeyBoardEvent(TenBlockManager.Controller.isPopuped);
    }

    private void ToggleKeyBoardEvent(bool state)
    {
        if (TenBlockManager.Controller.isPopuped)
        {
            InputHandler.OnLeftKeyPressed -= MoveGrid;
            InputHandler.OnRightKeyPressed -= MoveGrid;
            InputHandler.OnUpKeyPressed -= MoveGrid;
            InputHandler.OnDownKeyPressed -= MoveGrid;
        }
        else
        {
            InputHandler.OnLeftKeyPressed += MoveGrid;
            InputHandler.OnRightKeyPressed += MoveGrid;
            InputHandler.OnUpKeyPressed += MoveGrid;
            InputHandler.OnDownKeyPressed += MoveGrid;
        }
    }

    private void MoveGrid(MoveDirection direction)
    {
        Vector3 _position = new Vector3(currentX, currentY, 0) + Vector.DirectionToNormalizedVector(direction);
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
}