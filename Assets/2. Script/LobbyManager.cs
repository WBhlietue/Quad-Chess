using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Text;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public InputField playerName;
    public Text roomListText;
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        }
    }
    string GetName()
    {
        return input.text.Trim();
    }
    string GetPlayerName()
    {
        return playerName.text.Trim();
    }
    public void CreateRoom()
    {
        if (GetName().Length > 0)
        {
            PhotonNetwork.CreateRoom(GetName());
            PhotonNetwork.LocalPlayer.NickName = GetPlayerName();
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(GetName());
        PhotonNetwork.LocalPlayer.NickName = GetPlayerName();
    }
    public override void OnJoinedRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in roomList)
        {
            if (item.PlayerCount > 0)
            {
                builder.AppendLine("- (" + item.PlayerCount + "/4) " + item.Name);
            }
        }
        roomListText.text = builder.ToString();
    }
}
