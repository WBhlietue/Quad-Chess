using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartMenuManager : MonoBehaviourPunCallbacks
{
    private void Awake() {
        Application.targetFrameRate = 60;
    }
    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
