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
    // if turnData ==  1{ p1} esle if turnData ==2 {p2}
    public int turnData;
    public bool isGameStart;

    public Color mycolor;
    public Color opcolor;

    public bool isMaxRoomTriger;

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
        gamedata.turnData = 1;
        gamedata.isGameStart = false;
        gamedata.mycolor = new Color(255, 255, 255);
        gamedata.opcolor = new Color(255, 255, 255, 0.8f);
        gamedata.isMaxRoomTriger = false;
    }
    
}
