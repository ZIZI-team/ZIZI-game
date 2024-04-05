using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct Data
{
    public string TileName;
}

public abstract class Tiles : MonoBehaviour
{
    public Data data;

    public abstract void InitSetting();
    public virtual void SettingTile(Tilemap tilemap, List<TileBase> tilebaseList, int randomnum = 100) 
    {
        Debug.Log(data.TileName);
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                if (randomnum >= Random.Range(1, 101))
                {
                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);
                    tilemap.SetTile(tilePosition, tilebaseList[Random.Range(0,tilebaseList.Count)]);
                }
                
            }
        }
    }

    public virtual void GetTile(Tilemap tilemap) 
    {

        StartCoroutine(GetTileCoroutine(tilemap));

    }

    IEnumerator GetTileCoroutine(Tilemap tilemap)
    {
        yield return new WaitForSeconds(1);
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                TileBase tileBase = tilemap.GetTile(new Vector3Int(x, -y, 0));
                if (tileBase != null)
                {
                    if (data.TileName == "MainTile")
                    {
                        DataManager.Instance.board[x, y] = 0;
                    }
                    else if (data.TileName == "BushTile")
                    {
                        DataManager.Instance.board[x, y] = 2;
                    }
                    else if (data.TileName == "ItemTile")
                    {
                        DataManager.Instance.board[x, y] = 3;
                    }
                }

            }
        }
    }
}
