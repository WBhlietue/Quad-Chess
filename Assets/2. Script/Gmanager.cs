using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gmanager : MonoBehaviour
{
    public static Gmanager instance;
    public Transform chessHome;
    public Sprite queenImage;
    public Team nowMoveTeam;
    List<Team> selectTeams = new List<Team> { Team.White, Team.Yellow, Team.Blue, Team.Pink };
    List<Team> teams = new List<Team> { Team.White, Team.Yellow, Team.Blue, Team.Pink };
    public List<Players> activePlayers = new List<Players>();
    private void Awake()
    {
        instance = this;
        nowMoveTeam = Team.White;
    }

    public void ChangeColor()
    {
        if (activePlayers.Count == 1)
        {
            GameOver(activePlayers[0]);
        }
        int a = (int)nowMoveTeam;
        a++;
        if (a >= 4)
        {
            a -= 4;
        }

        nowMoveTeam = (Team)a;
    }
    public void GameOver(Players winner)
    {

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

[System.Serializable]
public class Players
{
    public string playerName;
    public Team team;
    public bool isActive = true;
}
