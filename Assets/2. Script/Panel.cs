using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Panel : MonoBehaviour, IPunObservable
{
    public static Panel instance;
    public Tile[] tiles;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].num = i;
        }
    }
    private void Start()
    {
        StartCoroutine(InitTag());
    }

    IEnumerator InitTag()
    {
        yield return new WaitForSeconds(1.1f);
        foreach (var item in tiles)
        {
            item.gameObject.tag = "Untagged";
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
