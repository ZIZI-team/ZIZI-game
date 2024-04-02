using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public Tiles mytile;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<TileBase> _tilebase;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_tilebase.Count);
        mytile.InitSetting();
        if (mytile.data.TileName == "MainTile")
        {
            mytile.SettingTile(_tilemap, _tilebase);
        }
        else
        {
            mytile.SettingTile(_tilemap, _tilebase, randomnum: 8);
        }
    }

}
