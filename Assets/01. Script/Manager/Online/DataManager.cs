using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileData
{
    public int[,] mainTile;
    //input N(ull) or W(hite) or B(lack)
    public string[,] stoneTile;
}
public class DataManager : MonoBehaviour
{
    public TileData tiledata;

    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        InitTileData();
    }

    private void InitTileData()
    {
        tiledata.mainTile = new int[11, 11];
        tiledata.stoneTile = new string[11, 11];
    }
    
}
