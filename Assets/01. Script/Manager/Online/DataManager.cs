using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileData
{
    public int[,] tileStatus;
    //input N(ull) or W(hite) or B(lack)
    public string[,] stoneStatus;
}

public struct GameData
{   
    public bool isGameStart;

    public Color mycolor;
    public Color opcolor;

    public bool isMaxRoomTriger;

    public string turnData;
    public string myP;
}

public class DataManager : Singleton<DataManager>
{
    public TileData tiledata;
    public GameData gamedata;

    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        InitTileData();
        InitGameData();
    }

    private void InitTileData()
    {
        tiledata.tileStatus = new int[11, 11];
        tiledata.stoneStatus = new string[11, 11];
    }

    private void InitGameData()
    {
        gamedata.turnData = "P1";
        gamedata.isGameStart = false;
        gamedata.mycolor = new Color(255, 255, 255);
        gamedata.opcolor = new Color(255, 255, 255, 0.8f);
        gamedata.isMaxRoomTriger = false;
    }
    
}
