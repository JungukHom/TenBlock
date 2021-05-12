// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class RoomSceneController : MonoBehaviour
{
    public static Action OnRoomSceneLoaded = null;

    private void Awake()
    {
        OnRoomSceneLoaded?.Invoke();
        OnRoomSceneLoaded = null;
    }
}