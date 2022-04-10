using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Tile : MonoBehaviour, IPunObservable
{
    public bool canUse;
    Color initColor;
    Image image;
    public Color checkColor;
    public Chess stay;
    public bool canUpgrade;
    public int num;
    private void Start()
    {
        image = GetComponent<Image>();
        initColor = image.color;
        canUse = image.enabled;
    }
    public void ChangeColor(bool toInit = false)
    {
        if (toInit)
        {
            image.color = initColor;
            gameObject.tag = "Untagged";
        }
        else
        {
            image.color = checkColor;
            gameObject.tag = "tile";
        }
    }
    public bool CheckChess(Chess chess)
    {
        if (stay == null)
        {
            return true;
        }
        if (stay.chessTeam == chess.chessTeam)
        {
            return false;
        }
        return true;
    }
    Gmanager gmanager;
    public void GetChess(int num, Team team)
    {
        Chess chess = null;
        if (num < 0)
        {
            stay = null;
        }
        if (gmanager == null)
        {
            gmanager = Gmanager.instance;
        }
        for (int i = 0; i < gmanager.chessHome.childCount; i++)
        {
            if (gmanager.chessHome.GetChild(i).GetComponent<Chess>().CheckIsThis(num, team))
            {
                chess = gmanager.chessHome.GetChild(i).GetComponent<Chess>();
                break;
            }
        }
        stay = chess;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        // if (stream.IsWriting)
        // {
        //     if (stay != null)
        //     {
        //         // Debug.Log("set");
        //         stream.SendNext(stay.chessNum);
        //         stream.SendNext(stay.chessTeam);
        //     }
        //     else
        //     {
        //         // Debug.Log("toNull");
        //         stream.SendNext(-1);
        //         stream.SendNext(0);
        //     }
        // }
        // else
        // {
        //     stay = GetChess((int)stream.ReceiveNext(), (Team)stream.ReceiveNext());
        //     // Debug.Log("get " + stay);
        // }
    }

}
