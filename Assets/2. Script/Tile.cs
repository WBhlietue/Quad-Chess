using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tile : MonoBehaviour
{
    public bool canUse;
    Color initColor;
    Image image;
    public Color checkColor;
    public Chess stay;
    public bool canUpgrade;
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
}
