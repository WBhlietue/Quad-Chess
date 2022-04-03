using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public static Panel instance;
    public Tile[] tiles;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(InitTag());
    }

    IEnumerator InitTag()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var item in tiles)
        {
            item.gameObject.tag = "Untagged";
        }
    }
}
