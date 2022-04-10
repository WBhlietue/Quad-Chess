using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChessManager : MonoBehaviour
{
    Chess[] chesses;
    public PhotonView pv;
    private void Awake()
    {

    }
    private void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<Chess>().chessNum = i;
        }
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = Gmanager.instance.pos.parent;
        transform.position = Gmanager.instance.pos.position;
        transform.GetChild(0).eulerAngles = Vector3.zero;
    }
}
