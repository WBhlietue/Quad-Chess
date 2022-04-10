using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using Photon.Realtime;

public class Gmanager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static Gmanager instance;
    public Transform chessHome;
    public Sprite queenImage;
    public Team nowMoveTeam;
    List<Team> selectTeams = new List<Team> { Team.White, Team.Yellow, Team.Blue, Team.Pink };
    public List<Team> activePlayers = new List<Team> { Team.White, Team.Yellow };
    public Transform pos;
    public Text moveText;
    public Text[] teams;
    public Text winText;
    private void Awake()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }
        else
        {
            instance = this;
            nowMoveTeam = Team.White;
            moveText.text = nowMoveTeam.ToString();
            StartCoroutine(Delay());
            List<string> playerNames = new List<string>();
            foreach (var item in PhotonNetwork.CurrentRoom.Players)
            {
                teams[(int)item.Value.CustomProperties["team"]].text = item.Value.NickName;
            }
            // Hashtable table = new Hashtable();
            // table = PhotonNetwork.LocalPlayer.CustomProperties;
            // foreach (var item in PhotonNetwork.CurrentRoom.Players)
            // {
            //     activePlayers.Add((Team)item.Value.CustomProperties["team"]);
            // }
            // PhotonNetwork.Instantiate(((Team)table["team"]).ToString(), pos.position, Quaternion.identity).transform.parent = pos.parent;
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Hashtable table = new Hashtable();
        table = PhotonNetwork.LocalPlayer.CustomProperties;
        PhotonNetwork.Instantiate(((Team)table["team"]).ToString(), pos.position, Quaternion.identity).transform.parent = pos.parent;
        yield return new WaitForSeconds(0.5f);
        chessHome.parent.eulerAngles = Vector3.forward * (int)((Team)table["team"]) * -90;
    }
    public void ChangeColor(int panelNum, int chessNUm, Team chessTeam, int homeNum, bool eat)
    {
        // if (activePlayers.Count == 1)
        // {
        //     GameOver(activePlayers[0]);
        // }
        // else
        // {
        int a = (int)nowMoveTeam;
        a++;
        if (a >= activePlayers.Count)
        {
            Debug.Log(a);
            a -= activePlayers.Count;
        }

        nowMoveTeam = (Team)a;
        Hashtable table = new Hashtable();
        table.Add("nextTeam", nowMoveTeam);
        table.Add("panelNum", panelNum);
        table.Add("chessNum", chessNUm);
        table.Add("chessTeam", chessTeam);
        table.Add("homeNum", homeNum);
        table.Add("eat", eat);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        // }
    }

    void SetTeam(Team team)
    {
        nowMoveTeam = team;
        moveText.text = team.ToString();

    }
    public void GameOver(Team winner)
    {
        Hashtable table = new Hashtable();
        table.Add("winner", winner);
        table.Add("over", 123);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps["over"] != null)
        {
            winText.text = ((Team)changedProps["winner"]).ToString() + " is winner!!!";
            winText.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            if ((bool)changedProps["eat"])
            {
                Chess c = Panel.instance.tiles[(int)(changedProps["panelNum"])].stay;
                c.gameObject.SetActive(false);
                if (c.chessClass == ChessClass.King)
                {
                    activePlayers.Remove(c.chessTeam);
                    teams[(int)c.chessTeam].gameObject.SetActive(false);
                }
            }
            SetTeam((Team)changedProps["nextTeam"]);
            Panel.instance.tiles[(int)(changedProps["panelNum"])].GetChess((int)changedProps["chessNum"], (Team)changedProps["chessTeam"]);
            Panel.instance.tiles[(int)(changedProps["homeNum"])].GetChess(-1, Team.White);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // if (stream.IsWriting)
        // {
        //     stream.SendNext(nowMoveTeam);
        // }
        // else
        // {
        //     SetTeam((Team)stream.ReceiveNext());
        // }
    }

}

public enum Team
{
    White, Yellow, Blue, Pink
}

public enum ChessClass
{
    Pawn, Knight, Bishop, Queen, King, Rook
}

