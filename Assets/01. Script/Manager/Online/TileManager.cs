using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance
    {
        get
        {
            if(instance == null) { return null; } return instance;
        }
    }

    public List<GameObject> TilemapPrefabsList;
    List<Tilemap> myTilemap = new List<Tilemap>();

    public GameObject stonePrefab;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }
    void Start()
    {

    }

    void Update()
    {
        OnClickPosition();
    }


    public void InitTile()
    {
        SetTile();
        StartCoroutine(GetTile());
    }
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
        for (int i = 0; i < myTilemap.Count; i++)
        {
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    TileBase tilebase = myTilemap[i].GetTile(new Vector3Int(x, y, 0));

                    if (tilebase != null)
                    {
                        if (myTilemap[i].name == "Maintile") { DataManager.Instance.tiledata.mainTile[x, y] = 0; }
                        else if (myTilemap[i].name == "Bushtile") { DataManager.Instance.tiledata.mainTile[x, y] = 1; }
                        else if (myTilemap[i].name == "Itemtile") { DataManager.Instance.tiledata.mainTile[x, y] = 2; }
                    }
                    DataManager.Instance.tiledata.stoneTile[x,y] = "N";
                }
            }
        }
    }

    void OnClickPosition()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3Int cellPos = myTilemap[0].WorldToCell(touchWorldPos);
            if (DataManager.Instance.tiledata.mainTile[cellPos.x, cellPos.y] != 1 && DataManager.Instance.tiledata.stoneTile[cellPos.x, cellPos.y] == "N")
            {
                InstallStone(cellPos);
                RemoveBush(cellPos);
                GetItem(cellPos);
                GameManager.Intances.CheckWinCondition("W", cellPos.x, cellPos.y);
            }
            Debug.Log("Touched tile position: " + cellPos);
            
        }
    }

    void InstallStone(Vector3Int cellPos)
    {
        DataManager.Instance.tiledata.stoneTile[cellPos.x, cellPos.y] = "W";  
        //Tile map과 transform와의 오차값 0.5f
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
        for(int i=0; i< BushpositionList.Count; i++) //상하좌우
        {
            myTilemap[2].SetTile(BushpositionList[i], null);
            try
            {
                DataManager.Instance.tiledata.mainTile[BushpositionList[i].x, BushpositionList[i].y] = 0;
            }
            catch
            {
                Debug.Log("Index를 넣어갔습니다");
            }

        }
        
    }

    void GetItem(Vector3Int cellPos)
    {
        myTilemap[1].SetTile(new Vector3Int(cellPos.x, cellPos.y), null);
        //UIManager.Instance.ItemGoInbantory();
    }

    
}


