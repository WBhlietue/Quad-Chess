using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chess : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public ChessClass chessClass;
    public Team chessTeam;
    Vector3 zuruu;
    public List<Tile> checkTiles = new List<Tile>();
    bool firstMove = true;
    public Tile tile
    {
        get
        {
            if (checkTiles.Count == 0)
            {
                return null;
            }
            return checkTiles[0];
        }
    }
    public Tile home;
    bool isStart = false;
    Chess otherChess;
    public int num;
    Panel panel;
    public List<Tile> targetTiles = new List<Tile>();
    private void Start()
    {
        panel = Panel.instance;
    }

    bool CheckCanAddTile(Tile tile)
    {
        foreach (var item in checkTiles)
        {
            if (item == tile)
            {
                return false;
            }
        }
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("tile"))
        {
            if (other.GetComponent<Tile>().canUse && CheckCanAddTile(other.GetComponent<Tile>()))
            {
                checkTiles.Add(other.GetComponent<Tile>());
            }
        }
        if (other.gameObject.CompareTag("chess"))
        {
            otherChess = other.GetComponent<Chess>();
        }
        if (!isStart)
        {
            isStart = true;
            try
            {
                tile.stay = this;
                transform.position = tile.transform.position;
            }
            catch
            {

            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("tile"))
        {
            checkTiles.Remove(other.GetComponent<Tile>());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        if (tile != null)
        {
            home = tile;
        }
        zuruu = eventData.position - (Vector2)transform.position;
        switch (chessClass)
        {
            case ChessClass.Pawn:
                PawnGetTile();
                break;
            case ChessClass.King:
                KingGetTiles();
                break;
            case ChessClass.Knight:
                KnightGetTiles();
                break;
            case ChessClass.Queen:
                QueenGetTile();
                break;
            case ChessClass.Bishop:
                BishopGetTiles();
                break;
            case ChessClass.Rook:
                RookGetTile();
                break;

        }
        checkTiles.Clear();

    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector3)eventData.position - zuruu;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (tile != null)
        {
            if (tile.CheckChess(this))
            {
                try
                {
                    tile.stay.gameObject.SetActive(false);
                }
                catch { }
                tile.stay = this;
                home.stay = null;
                firstMove = false;
                transform.position = tile.transform.position;
            }
            else
            {
                transform.position = home.transform.position;
            }
        }
        else
        {
            transform.position = home.transform.position;
        }
        foreach (var item in targetTiles)
        {
            item.ChangeColor(true);
        }
        targetTiles.Clear();
    }

    void GetTileNums()
    {
        for (int i = 0; i < panel.tiles.Length; i++)
        {
            if (tile == panel.tiles[i])
            {
                num = i;
                break;
            }
        }
    }

    void RookGetTile()
    {
        GetTileNums();
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num + i * 14].stay != null)
                {
                    if (panel.tiles[num + i * 14].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num + i * 14]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num + i * 14]);
                }
            }
        }
        catch { }
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num - i * 14].stay != null)
                {
                    if (panel.tiles[num - i * 14].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num - i * 14]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num - i * 14]);
                }
            }
        }
        catch { }
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num + i].transform.position.y != panel.tiles[num + i - 1].transform.position.y)
                {
                    break;
                }
                if (panel.tiles[num + i].stay != null)
                {
                    if (panel.tiles[num + i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num + i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num + i]);
                }
            }
        }
        catch { }
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num - i].transform.position.y != panel.tiles[num - i + 1].transform.position.y)
                {
                    break;
                }
                if (panel.tiles[num - i].stay != null)
                {
                    if (panel.tiles[num - i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num - i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num - i]);
                }
            }
        }
        catch { }
        foreach (var item in targetTiles)
        {
            item.ChangeColor();
        }
    }
    void BishopGetTiles()
    {
        GetTileNums();
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num + i * 14 + i].transform.position.x < panel.tiles[num + (i - 1) * 14 + i - 1].transform.position.x)
                {
                    break;
                }
                if (panel.tiles[num + i * 14 + i].stay != null)
                {
                    if (panel.tiles[num + i * 14 + i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num + i * 14 + i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num + i * 14 + i]);
                }
            }
        }
        catch { }
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num + i * 14 - i].transform.position.x > panel.tiles[num + (i - 1) * 14 - i + 1].transform.position.x)
                {
                    break;
                }
                if (panel.tiles[num + i * 14 - i].stay != null)
                {
                    if (panel.tiles[num + i * 14 - i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num + i * 14 - i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num + i * 14 - i]);
                }
            }
        }
        catch { }
        try
        {
            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num - i * 14 - i].transform.position.x > panel.tiles[num - (i - 1) * 14 - i + 1].transform.position.x)
                {
                    break;
                }
                if (panel.tiles[num - i * 14 - i].stay != null)
                {
                    if (panel.tiles[num - i * 14 - i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num - i * 14 - i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num - i * 14 - i]);
                }
            }
        }
        catch { }
        try
        {

            for (int i = 1; i < 100; i++)
            {
                if (panel.tiles[num - i * 14 + i].transform.position.x < panel.tiles[num - (i - 1) * 14 + i - 1].transform.position.x)
                {
                    break;
                }
                if (panel.tiles[num - i * 14 + i].stay != null)
                {
                    if (panel.tiles[num - i * 14 + i].stay.chessTeam != chessTeam)
                    {
                        targetTiles.Add(panel.tiles[num - i * 14 + i]);
                    }
                    break;
                }
                else
                {
                    targetTiles.Add(panel.tiles[num - i * 14 + i]);
                }
            }
        }
        catch { }
        foreach (var item in targetTiles)
        {
            item.ChangeColor();
        }
    }
    void KingGetTiles()
    {
        GetTileNums();
        try
        {
            if (panel.tiles[num + 14].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 14]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 14].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 14]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num + 1].transform.position.x > transform.position.x)
            {
                targetTiles.Add(panel.tiles[num + 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 1].transform.position.x < transform.position.x)
            {
                targetTiles.Add(panel.tiles[num - 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num + 14 + 1].transform.position.x > transform.position.x && panel.tiles[num + 14 + 1].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 14 + 1]);
            }
        }
        catch { }
        try
        {
            if (panel.tiles[num + 14 - 1].transform.position.x < transform.position.x && panel.tiles[num + 14 - 1].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 14 - 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 14 - 1].transform.position.x < transform.position.x && panel.tiles[num - 14 - 1].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 14 - 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 14 + 1].transform.position.x > transform.position.x && panel.tiles[num - 14 + 1].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 14 + 1]);
            }
        }
        catch { };
        foreach (var item in targetTiles)
        {
            item.ChangeColor();
        }
    }

    void KnightGetTiles()
    {
        GetTileNums();
        try
        {
            if (panel.tiles[num + 28 + 1].transform.position.x > transform.position.x && panel.tiles[num + 28 + 1].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 28 + 1]);
            }
        }
        catch { }
        try
        {
            if (panel.tiles[num + 28 - 1].transform.position.x < transform.position.x && panel.tiles[num + 28 - 1].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 28 - 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 28 - 1].transform.position.x < transform.position.x && panel.tiles[num - 28 - 1].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 28 - 1]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 28 + 1].transform.position.x > transform.position.x && panel.tiles[num - 28 + 1].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 28 + 1]);
            }
        }
        catch { };

        try
        {
            if (panel.tiles[num + 14 + 2].transform.position.x > transform.position.x && panel.tiles[num + 14 + 2].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 14 + 2]);
            }
        }
        catch { }
        try
        {
            if (panel.tiles[num + 14 - 2].transform.position.x < transform.position.x && panel.tiles[num + 14 - 2].transform.position.y < transform.position.y)
            {
                targetTiles.Add(panel.tiles[num + 14 - 2]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 14 - 2].transform.position.x < transform.position.x && panel.tiles[num - 14 - 2].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 14 - 2]);
            }
        }
        catch { };
        try
        {
            if (panel.tiles[num - 14 + 2].transform.position.x > transform.position.x && panel.tiles[num - 14 + 2].transform.position.y > transform.position.y)
            {
                targetTiles.Add(panel.tiles[num - 14 + 2]);
            }
        }
        catch { };
        foreach (var item in targetTiles)
        {
            item.ChangeColor();
        }
    }
    void PawnGetTile()
    {
        GetTileNums();
        try
        {
            if (panel.tiles[num - 14].stay == null)
            {
                targetTiles.Add(panel.tiles[num - 14]);
            }
        }
        catch { }
        if (firstMove)
        {
            try
            {
                if (panel.tiles[num - 28].stay == null)
                {
                    targetTiles.Add(panel.tiles[num - 28]);
                }
            }
            catch { }
        }
        // try
        // {
        //     if (panel.tiles[num - 14 - 1].transform.position.x < transform.position.x && panel.tiles[num - 14 - 1].transform.position.y > transform.position.y)
        //     {
        //         targetTiles.Add(panel.tiles[num - 14 - 1]);
        //     }
        // }
        // catch { };
        // try
        // {
        //     if (panel.tiles[num - 14 + 1].transform.position.x > transform.position.x && panel.tiles[num - 14 + 1].transform.position.y > transform.position.y)
        //     {
        //         targetTiles.Add(panel.tiles[num - 14 + 1]);
        //     }
        // }
        catch { };
        foreach (var item in targetTiles)
        {
            item.ChangeColor();
        }
    }
    void QueenGetTile()
    {
        RookGetTile();
        BishopGetTiles();
    }
}
