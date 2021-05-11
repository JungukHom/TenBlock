// System
using System;
using System.Collections;

// Unity
using UnityEngine;
using UnityEngine.Networking;

public class NetworkConnection : MonoBehaviour
{
    public void Check(Action<bool> callback)
    {
        StartCoroutine(Ping((result) =>
        {
            callback?.Invoke(result);
        }));
    }

    private IEnumerator Ping(Action<bool> callback)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError
                || request.result == UnityWebRequest.Result.DataProcessingError
                || request.result == UnityWebRequest.Result.ProtocolError
           )
            callback?.Invoke(false);
        else
            callback?.Invoke(true);
    }
}