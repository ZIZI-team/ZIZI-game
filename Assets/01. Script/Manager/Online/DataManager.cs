using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tile들의 위치에 대한 Data들이 저장되는 strust임. 
/// tileStatus에서 0는 Maintile, 1은 Bushtile, 2는 Itemtile임.
/// stoneStatus에서는 N(ull) or P1 or P2 or 3가지 값을 가짐.
/// </summary>
public struct TileData
{
    public int[,] tileStatus;
    public string[,] stoneStatus;
}
/// <summary>
/// 오목 게임 안에서 필요한 GameData가 저장되어있는 struct임.
/// </summary>
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

/// <summary>
/// 게임 중 나의 인벤토리와 상대의 인벤토리의 data가 저장되는 struct임.
/// </summary>
public struct InbantoryData
{
    public bool[] mydotoriInban;
    public bool[] myleafInban;

    public bool[] opdotoriInban;
    public bool[] opleafInban;
}
/// <summary>
/// 게임의 전체적인 Data를 저장하는 Class
/// </summary>
public class DataManager : Singleton<DataManager>
{
    public TileData tiledata;
    public GameData gamedata;
    public InbantoryData inbantorydata;

    private void Start()
    {
        InitData();
    }

    /// <summary>
    /// Data 전체 초기화 시키는 함수
    /// </summary>
    private void InitData()
    {
        InitTileData();
        InitGameData();
        InitInbantoryData();
    }

    /// <summary>
    /// 11x11의 빈 tile로 초기화 시키는 함수
    /// </summary>
    private void InitTileData()
    {
        tiledata.tileStatus = new int[11, 11];
        tiledata.stoneStatus = new string[11, 11];
    }

    /// <summary>
    /// Game Data 초기화 시키는 함수
    /// </summary>
    private void InitGameData()
    {
        gamedata.turnData = "P1";
        gamedata.isGameStart = false;
        gamedata.mycolor = new Color(255, 255, 255);
        gamedata.opcolor = new Color(255, 255, 255, 0.8f);
        gamedata.isMaxRoomTriger = false;
        gamedata.timertime = 60f;
    }

    /// <summary>
    /// 빈 Inbantory로 초기화 시키는 함수
    /// </summary>
    private void InitInbantoryData()
    {
        inbantorydata.mydotoriInban = new bool[5];
        inbantorydata.myleafInban = new bool[5];

        inbantorydata.opdotoriInban = new bool[5];
        inbantorydata.opleafInban = new bool[5];
    }

    /// <summary>
    /// 타이머 초기화 시키는 함수
    /// </summary>
    public void InitTimer()
    {
        gamedata.timertime = 60;
    }

    /// <summary>
    /// 아이템 획득 시 업데이트 되어지는 함수
    /// </summary>
    /// <param name="myP">내가 획득 시 "My"고 상대가 획득 시 "Op" </param>
    /// <param name="itemName"> Dotori or Leaf </param>
    /// <param name="index">instanceItme.transform.parent.childCount -1 값을 통해 가져옴.</param>
    /// <param name="status"> 값을 받을 때, true면 획득 false면 사용</param>
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
    
  
}
