// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class KeyBoardButton : MonoBehaviour
{
    public KeyBoardButton btn_left;
    public KeyBoardButton btn_right;
    public KeyBoardButton btn_up;
    public KeyBoardButton btn_down;

    [Header("UI")]
    public Image img_outline;

    [Header("Public Variables")]
    public bool isSelected = false;
    public Action buttonCallback;

    private void Awake()
    {
        if (isSelected)
        {
            OnSelected();
        }
    }

    private void OnDisable()
    {
        OnDisSelected();
    }

    public void OnSelected()
    {
        InputHandler.OnLeftKeyPressed += OnKeyBoardArrowPressed;
        InputHandler.OnRightKeyPressed += OnKeyBoardArrowPressed;
        InputHandler.OnUpKeyPressed += OnKeyBoardArrowPressed;
        InputHandler.OnDownKeyPressed += OnKeyBoardArrowPressed;
        InputHandler.OnSpaceKeyDown += buttonCallback;
        SetOutlineVisible(true);
    }

    public void OnDisSelected()
    {
        InputHandler.OnLeftKeyPressed -= OnKeyBoardArrowPressed;
        InputHandler.OnRightKeyPressed -= OnKeyBoardArrowPressed;
        InputHandler.OnUpKeyPressed -= OnKeyBoardArrowPressed;
        InputHandler.OnDownKeyPressed -= OnKeyBoardArrowPressed;
        InputHandler.OnSpaceKeyDown -= buttonCallback;
        SetOutlineVisible(false);
    }

    private void OnKeyBoardArrowPressed(MoveDirection direction)
    {
        KeyBoardButton target = null;
        switch (direction)
        {
            case MoveDirection.Left:
                target = btn_left;
                break;

            case MoveDirection.Right:
                target = btn_right;
                break;

            case MoveDirection.Up:
                target = btn_up;
                break;

            case MoveDirection.Down:
                target = btn_down;
                break;
        }

        if (target)
        {
            target.OnSelected();
            OnDisSelected();
        }
    }

    private void SetOutlineVisible(bool visible)
    {
        if (img_outline)
        {
            float alpha = visible ? 1 : 0;
            Color newColor = img_outline.color;
            newColor.a = alpha;
            img_outline.color = newColor;
        }
    }
}