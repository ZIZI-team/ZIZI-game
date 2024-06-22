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

    public float timertime;
}

public struct InbantoryData
{
    public bool[] mydotoriInban;
    public bool[] myleafInban;

    public bool[] opdotoriInban;
    public bool[] opleafInban;
}

public class DataManager : Singleton<DataManager>
{
    public TileData tiledata;
    public GameData gamedata;
    public InbantoryData inbantorydata;

    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        InitTileData();
        InitGameData();
        InitInbantoryData();
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
        gamedata.timertime = 60f;
    }

    private void InitInbantoryData()
    {
        inbantorydata.mydotoriInban = new bool[5];
        inbantorydata.myleafInban = new bool[5];

        inbantorydata.opdotoriInban = new bool[5];
        inbantorydata.opleafInban = new bool[5];
    }

    public void UpdateInbantoryData(string myP, string itemName, int index, bool status)
    {
        if(myP == "My")
        {
            if(itemName == "Dotori")
            {
                inbantorydata.mydotoriInban[index] = status;
            }
            else if (itemName == "Leaf")
            {
                inbantorydata.myleafInban[index] = status;
            }
        }
        else if(myP == "Op")
        {
            if (itemName == "Dotori")
            {
                inbantorydata.opdotoriInban[index] = status;
            }
            else if (itemName == "Leaf")
            {
                inbantorydata.opleafInban[index] = status;
            }
        }
    }
    
    public void InitTimer()
    {
        gamedata.timertime = 60;
    }
}
