// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class InputHandler : MonoBehaviour
{
    public static event Action<MoveDirection> OnLeftKeyPressed = null;
    public static event Action<MoveDirection> OnRightKeyPressed = null;
    public static event Action<MoveDirection> OnUpKeyPressed = null;
    public static event Action<MoveDirection> OnDownKeyPressed = null;

    public static event Action OnSpaceKeyDown = null;
    public static event Action<bool> OnESCKeyPressed = null;

    public static event Action OnRKeyPressed = null;
    public static event Action OnCKeyPressed = null;

    private static Dictionary<KeyCode, Action> targetInputKey;

    private void Awake()
    {
        targetInputKey = new Dictionary<KeyCode, Action>()
        {
            { KeyCode.Space, SpaceKeyDown },
            { KeyCode.LeftArrow, LeftKeyDown },
            { KeyCode.RightArrow, RightKeyDown },
            { KeyCode.UpArrow, UpKeyDown },
            { KeyCode.DownArrow, DownKeyDown },
            { KeyCode.Escape, ESCKeyDown },
            { KeyCode.R, RKeyDown },
            { KeyCode.C, CKeyDown }
        };
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyValuePair<KeyCode, Action> input in targetInputKey)
            {
                if (Input.GetKeyDown(input.Key))
                    input.Value?.Invoke();
            }
        }
    }

    private void SpaceKeyDown() => OnSpaceKeyDown?.Invoke();
    private void LeftKeyDown() => OnLeftKeyPressed?.Invoke(MoveDirection.Left);
    private void RightKeyDown() => OnRightKeyPressed?.Invoke(MoveDirection.Right);
    private void UpKeyDown() => OnUpKeyPressed?.Invoke(MoveDirection.Up);
    private void DownKeyDown() => OnDownKeyPressed?.Invoke(MoveDirection.Down);
    private void ESCKeyDown()
    {
        //bool state = TenBlockManager.Controller.TogglePopupState();
        bool state = TenBlockManager.Controller.SetPopupedState(!EscapePannel.isActived);
        OnESCKeyPressed?.Invoke(state);
    }

    private void RKeyDown() => OnRKeyPressed?.Invoke();

    private void CKeyDown() => OnCKeyPressed?.Invoke();
}
