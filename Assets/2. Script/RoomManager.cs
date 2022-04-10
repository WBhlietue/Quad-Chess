using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Text playerListText;
    public Text roomName;
    public Button startgame;
    Coroutine delay;
    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            PhotonNetwork.CurrentRoom.MaxPlayers = 4;
            playerListText.text = GetPlayerList();
            roomName.text = PhotonNetwork.CurrentRoom.Name;
            startgame.interactable = PhotonNetwork.IsMasterClient;
        }
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
            List<Team> teams = new List<Team> { Team.White, Team.Yellow, Team.Blue, Team.Pink };
            List<Player> players = new List<Player>();
            foreach (var item in PhotonNetwork.CurrentRoom.Players)
            {
                // Hashtable table = new Hashtable();
                // Team t = teams[Random.Range(0, teams.Count)];
                // table.Add("team", t);
                // teams.Remove(t);
                // item.Value.SetCustomProperties(table);
                // Debug.Log("01231023");
                players.Add(item.Value);
            }
            while (players.Count > 0)
            {
                Player p = players[Random.Range(0, players.Count)];
                Hashtable table = new Hashtable();
                Team t = teams[0];
                table.Add("team", t);
                teams.Remove(t);
                players.Remove(p);
                p.SetCustomProperties(table);
            }
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        targetPlayer.CustomProperties = changedProps;
        if (delay == null)
        {
            delay = StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
