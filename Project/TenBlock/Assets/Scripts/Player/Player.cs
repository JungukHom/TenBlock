// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Player : MonoBehaviour
{
    private int currentX = 0;
    private int currentZ = 0;

    private Transform _transform;

    private void Awake()
    {
        InputHandler.OnESCKeyDown += ToggleKeyBoardEvent;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
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
        currentZ = _transform.position.z.Round();
    }

    private void AddListeners()
    {
        ToggleKeyBoardEvent(TenBlockManager.Controller.isEscaped);
    }

    private void ToggleKeyBoardEvent(bool state)
    {
        if (TenBlockManager.Controller.isEscaped)
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
        Vector3 _position = new Vector3(currentX, 0, currentZ) + Vector.DirectionToNormalizedVector(direction);
        transform.position = _position;
        SetupPosition();
    }
}