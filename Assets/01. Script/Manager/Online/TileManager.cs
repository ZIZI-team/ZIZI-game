using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : Singleton<TileManager>
{

    public List<GameObject> TilemapPrefabsList;
    List<Tilemap> myTilemap = new List<Tilemap>();

    public GameObject stonePrefab;

    public void InitTile()
    {
        SetTile();
        StartCoroutine(GetTile());
    }

    #region Init Tile Script
    public void SetTile()
    {

        Instantiate(TilemapPrefabsList[Random.Range(0, TilemapPrefabsList.Count)]);
        myTilemap.Add(GameObject.Find("Maintile").GetComponent<Tilemap>());
        myTilemap.Add(GameObject.Find("Itemtile").GetComponent<Tilemap>());
        myTilemap.Add(GameObject.Find("Bushtile").GetComponent<Tilemap>());
    }   

    IEnumerator GetTile()
    {
        yield return new WaitForSeconds(1);
        foreach (Tilemap tilemap in myTilemap)
        {
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    TileBase tilebase = tilemap.GetTile(new Vector3Int(x, y, 0));

                    if (tilebase != null)
                    {
                        if (tilemap.name == "Maintile") { DataManager.Instance.tiledata.tileStatus[x, y] = 0; }
                        else if (tilemap.name == "Bushtile") { DataManager.Instance.tiledata.tileStatus[x, y] = 1; }
                        else if (tilemap.name == "Itemtile") { DataManager.Instance.tiledata.tileStatus[x, y] = 2; }
                    }
                    DataManager.Instance.tiledata.stoneStatus[x,y] = "N";
                }
            }
        }
    }

    #endregion

    public void OnClickPosition()
    {
        if ((DataManager.Instance.gamedata.turnData == 1 && DataManager.Instance.gamedata.myP == "P1") || (DataManager.Instance.gamedata.turnData == 2 && DataManager.Instance.gamedata.myP == "P2"))
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector3Int cellPos = myTilemap[0].WorldToCell(touchWorldPos);
                if (DataManager.Instance.tiledata.tileStatus[cellPos.x, cellPos.y] != 1 && DataManager.Instance.tiledata.stoneStatus[cellPos.x, cellPos.y] == "N")
                {
                    InstallStone(DataManager.Instance.gamedata.myP, cellPos);
                    RemoveBush(cellPos);
                    GetItem(cellPos);
                    StartCoroutine(GameSystem.Instance.CheckWinCondition(DataManager.Instance.gamedata.myP, cellPos.x, cellPos.y));
                }
                Debug.Log("Touched tile position: " + cellPos);

            }
        }
    }

    void InstallStone(string player ,Vector3Int cellPos)
    {
        DataManager.Instance.tiledata.stoneStatus[cellPos.x, cellPos.y] = player;  
        //Tile map�� transform���� ������ 0.5f
        Vector3 stonePosition = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0);
        GameObject instanceStone = Instantiate(stonePrefab, stonePosition, Quaternion.identity);
        instanceStone.transform.SetParent(GameObject.Find("Stone Pooling").transform);
    }

    void RemoveBush(Vector3Int cellPos)
    {
        List<Vector3Int> BushpositionList = new List<Vector3Int>() {
            new Vector3Int(cellPos.x + 1,  cellPos.y),
            new Vector3Int(cellPos.x -1, cellPos.y),
            new Vector3Int(cellPos.x, cellPos.y-1),
            new Vector3Int(cellPos.x , cellPos.y+1)
        };
        for(int i=0; i< BushpositionList.Count; i++) //�����¿�
        {
            myTilemap[2].SetTile(BushpositionList[i], null);
            try
            {
                DataManager.Instance.tiledata.tileStatus[BushpositionList[i].x, BushpositionList[i].y] = 0;
            }
            catch
            {
                Debug.Log("Index�� �־���ϴ�");
            }

        }
        
    }

    void GetItem(Vector3Int cellPos)
    {
        myTilemap[1].SetTile(new Vector3Int(cellPos.x, cellPos.y), null);
        //UIManager.Instance.ItemGoInbantory();
    }

    
}


