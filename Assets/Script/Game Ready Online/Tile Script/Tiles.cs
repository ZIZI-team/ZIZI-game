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

    public abstract void GetTile();
}
