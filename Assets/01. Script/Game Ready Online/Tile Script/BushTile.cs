using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BushTile : Tiles
{
    

    public override void InitSetting()
    {
        data.TileName = "BushTile";
    }

    public override void SettingTile(Tilemap tilemap, List<TileBase> tilebase, int randomnum)
    {
        base.SettingTile(tilemap, tilebase, randomnum);
    }

    public override void GetTile(Tilemap tilemap)
    {

    }

}
