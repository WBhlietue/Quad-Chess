using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartMenuManager : MonoBehaviourPunCallbacks
{
    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("start to connect");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("complete");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
