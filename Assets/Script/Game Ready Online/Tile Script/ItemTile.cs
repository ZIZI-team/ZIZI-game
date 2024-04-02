using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemTile : Tiles
{
    public override void GetTile()
    {
            
    }

    public override void InitSetting()
    {
        data.TileName = "ItemTile";
    }

    public override void SettingTile(Tilemap tilemap, List<TileBase> tilebase, int randomnum)
    {
        base.SettingTile(tilemap, tilebase, randomnum);
    }


}
