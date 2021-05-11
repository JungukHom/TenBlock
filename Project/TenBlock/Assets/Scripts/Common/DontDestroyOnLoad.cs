// Unity
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        DontDestroyOnLoad(_transform);
    }
}