// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public partial class Utility
{
    public static T InstantiateResource<T>(string resourcesPath, Action<T> callback = null) where T : Component
    {
        GameObject _prefab = Resources.Load(resourcesPath) as GameObject;
        GameObject _gameObject = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);

        T _component = _gameObject.GetComponent<T>();
        callback?.Invoke(_component);

        return _component;
    }
}