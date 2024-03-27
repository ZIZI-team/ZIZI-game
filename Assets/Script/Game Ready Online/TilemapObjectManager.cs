using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObjectManager : MonoBehaviour
{
    [Header("MainTile")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilePrefab;
    
    [Header("BushTile")]
    [SerializeField] private Tilemap bushTile;
    [SerializeField] private TileBase bushPrefab;

    [Header("ItemTile")]
    [SerializeField] private Tilemap itemTile;
    [SerializeField] private List<TileBase> itemTilebase = new List<TileBase> ();

    private int[,] board = new int[11, 11]; // 오목판

    void Start()
    {
        // 타일맵에 타일 배치
        PlaceTiles();

        // 배치된 타일 가져오기
        GetTiles();

        //부쉬타일 배치
        RamdomBushTiles();
    }

    void PlaceTiles()
    {
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x , -y , 0);
                tilemap.SetTile(tilePosition, tilePrefab);
            }
        }
    }

    void GetTiles()
    {
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x , -y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                // 타일이 있으면 1, 없으면 0으로 설정합니다.
                if (tile != null)
                {
                    board[x, y] = 1;
                }
                else
                {
                    board[x, y] = 0;
                }
            }
        }
    }

    void RamdomBushTiles()
    {
        for(int y=0; y < 11; y++)
        {
            for(int x=0; x<11; x++)
            {
                int randomNumber = Random.Range(1, 11);
                if (randomNumber == 1)
                {
                    Vector3Int tilePosition = new Vector3Int(x, -y, 0);
                    bushTile.SetTile(tilePosition, bushPrefab);
                }
            }
        }
    }
}
