// System
using System.Collections;

// Unity
using UnityEngine;

public class SplashSceneController : MonoBehaviour
{
    public NetworkConnection networkConnection;
    public Transform dontDestroyOnLoadTarget;

    private void Start()
    {
        //DisableCursor();
        CheckConnection();
    }

    private void CheckConnection()
    {
        LoadingPannel.Controller.SetActive(true);
        LoadingPannel.Controller.SetMessage("Checking internet connection");
        networkConnection.Check((result) =>
        {
            if (result)
            {
                Logger.Log("Network connection success. Try to connect photon master server.");
                LoadingPannel.Controller.SetActive(false);
                DontDestroyOnLoad(dontDestroyOnLoadTarget);
                Connect();
            }
            else
            {
                ShowRestartMessage();
            }
        });
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked; // TODO
    }

    private bool Connect()
    {
        LoadingPannel.Controller.SetActive(true);
        LoadingPannel.Controller.SetMessage("Connecting to server");
        return TenBlockManager.Controller.ConnectUsingSettings();
    }

    private void ShowRestartMessage()
    {
        MessagePopup.Show("Notice", "Please check internet connection.\nPress [Cancel] to quit game,\nPress [Ok] to retry connection check.", () =>
        {
            Logger.Log("Network connection failed. Reload splash scene.");
            SceneLoader.LoadScene(SceneName.Splash);
        }, () =>
        {
            Logger.Log("Network connection failed. Quit application.");
            Application.Quit();
        });
    }
}