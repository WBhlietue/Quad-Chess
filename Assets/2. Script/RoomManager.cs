using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Text playerListText;
    public Text roomName;
    public Button startgame;
    void Start()
    {
        PhotonNetwork.CurrentRoom.MaxPlayers = 4;
        playerListText.text = GetPlayerList();
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        startgame.interactable = PhotonNetwork.IsMasterClient;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerListText.text = GetPlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerListText.text = GetPlayerList();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startgame.interactable = PhotonNetwork.IsMasterClient;
    }
    string GetPlayerList()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            builder.AppendLine(item.Value.NickName);
        }
        return builder.ToString();
    }
    public void LeftRoom()
    {
        Debug.Log("0978");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
    public void StartGame()
    {

    }
}
