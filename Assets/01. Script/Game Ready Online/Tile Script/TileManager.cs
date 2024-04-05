using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    public List<GameObject> TilemapPrefabsList;
    public List<Tilemap> myTilemap = new List<Tilemap>();
    
    void Start()
    {
        SetTile();
        myTilemap.Add(GameObject.Find("Tilemap").GetComponent<Tilemap>());
        myTilemap.Add(GameObject.Find("Itemmap").GetComponent<Tilemap>());
        myTilemap.Add(GameObject.Find("Bushmap").GetComponent<Tilemap>());
        StartCoroutine(GetTile());


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTile()
    {
        // 프리팹을 Resources 폴더에서 로드
        Instantiate(TilemapPrefabsList[Random.Range(0, TilemapPrefabsList.Count)]);

    }

    IEnumerator GetTile()
    {
        yield return new WaitForSeconds(1);
        for(int i = 0; i < myTilemap.Count; i++)
        {
            for(int x=0; x<11; x++)
            {
                for(int y=0; y<11; y++)
                {
                    TileBase tilebase = myTilemap[i].GetTile(new Vector3Int(x, -y, 0));
                    
                    if(tilebase != null)
                    {
                        if (myTilemap[i].name == "Tilemap") { DataManager.Instance.board[x, y] = 0; }
                        //돌이 놓여있는 곳은 1로
                        else if (myTilemap[i].name == "Bushmap") { DataManager.Instance.board[x, y] = 2; }
                        else if (myTilemap[i].name == "Itemmap") { DataManager.Instance.board[x, y] = 3; }

                    }
                }
            }
        }
    }

    
}


