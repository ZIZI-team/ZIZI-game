using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : Singleton<TileManager>
{
    public List<GameObject> TilemapPrefabsList; // 타일맵 프리팹 리스트
    List<Tilemap> myTilemap = new List<Tilemap>(); // 타일맵 리스트

    public GameObject stonePrefab; // 돌 프리팹

    #region Init Tile Script
    /// <summary>
    /// 타일을 설정하는 함수
    /// </summary>
    public void SetTile(int randomNumber)
    {
        try
        {
            Instantiate(TilemapPrefabsList[randomNumber]); // 랜덤한 타일맵 프리팹을 인스턴스화
            myTilemap.Add(GameObject.Find("Maintile").GetComponent<Tilemap>()); // 메인 타일맵 추가
            myTilemap.Add(GameObject.Find("Itemtile").GetComponent<Tilemap>()); // 아이템 타일맵 추가
            myTilemap.Add(GameObject.Find("Bushtile").GetComponent<Tilemap>()); // 부쉬 타일맵 추가
        }
        catch
        {
            SetTile(randomNumber); // 예외 발생 시 재시도
        }
    }
    /// <summary>
    /// 타일 정보를 가져오는 코루틴
    /// </summary>
    public IEnumerator GetTile()
    {
        yield return new WaitForSeconds(1); // 1초 대기
        foreach (Tilemap tilemap in myTilemap)
        {
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    TileBase tilebase = tilemap.GetTile(new Vector3Int(x, y, 0)); // 현재 타일맵의 타일을 가져옴

                    if (tilebase != null)
                    {
                        if (tilemap.name == "Maintile")
                        {
                            DataManager.Instance.tiledata.tileStatus[x, y] = 0; // 메인 타일맵
                        }
                        else if (tilemap.name == "Bushtile")
                        {
                            DataManager.Instance.tiledata.tileStatus[x, y] = 1; // 부쉬 타일맵
                        }
                        else if (tilemap.name == "Itemtile")
                        {
                            DataManager.Instance.tiledata.tileStatus[x, y] = 2; // 아이템 타일맵
                        }
                    }
                    DataManager.Instance.tiledata.stoneStatus[x, y] = "N"; // 돌 상태 초기화
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// 터치 위치 처리 함수
    /// </summary>
    public void OnClickPosition()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); // 터치 위치를 월드 좌표로 변환
            Vector3Int cellPos = myTilemap[0].WorldToCell(touchWorldPos); // 월드 좌표를 타일 좌표로 변환
            if (cellPos == null) return; // 유효한 위치가 아니면 반환
            if (DataManager.Instance.tiledata.tileStatus[cellPos.x, cellPos.y] != 1 && DataManager.Instance.tiledata.stoneStatus[cellPos.x, cellPos.y] == "N")
            {
                AudioManager.Instance.SFX3(); // 사운드 재생
                NetworkManager.Instance.SendStoneLocation(DataManager.Instance.gamedata.myP, cellPos); // 돌 위치 전송
                NetworkManager.Instance.SendChangeTurn(); // 턴 변경 전송
            }
            Debug.Log("Touched tile position: " + cellPos); // 디버그 로그
        }
    }

    #region Install Stone and Update Tile Condition
    /// <summary>
    /// 돌을 설치하는 함수
    /// </summary>
    /// <param name="player">돌을 놓는 플레이어</param>
    /// <param name="cellPos">돌을 놓는 위치</param>
    public void InstallStone(string player, Vector3Int cellPos)
    {
        DataManager.Instance.tiledata.stoneStatus[cellPos.x, cellPos.y] = player; // 돌 상태 업데이트
        Vector3 stonePosition = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0); // 돌의 위치 계산
        GameObject instanceStone = Instantiate(stonePrefab, stonePosition, Quaternion.identity); // 돌 인스턴스화
        if (player == DataManager.Instance.gamedata.myP)
        {
            instanceStone.GetComponent<SpriteRenderer>().color = DataManager.Instance.gamedata.mycolor; // 내 돌 색상 설정
        }
        else
        {
            instanceStone.GetComponent<SpriteRenderer>().color = DataManager.Instance.gamedata.opcolor; // 상대 돌 색상 설정
        }

        instanceStone.transform.SetParent(GameObject.Find("Stone Pooling").transform); // 돌을 부모 오브젝트에 추가

        UpdateTileCondition(cellPos); // 타일 상태 업데이트
    }

    /// <summary>
    /// 타일 상태를 업데이트하는 함수
    /// </summary>
    /// <param name="cellPos">돌을 놓는 위치</param>
    void UpdateTileCondition(Vector3Int cellPos)
    {
        RemoveBush(cellPos); // 부쉬 제거
        GetItem(cellPos); // 아이템 획득
        StartCoroutine(GameSystem.Instance.CheckWinCondition(DataManager.Instance.gamedata.myP, cellPos.x, cellPos.y)); // 승리 조건 확인
    }

    /// <summary>
    /// 부쉬를 제거하는 함수
    /// </summary>
    /// <param name="cellPos">돌을 놓는 위치</param>
    void RemoveBush(Vector3Int cellPos)
    {
        List<Vector3Int> BushpositionList = new List<Vector3Int>() {
            new Vector3Int(cellPos.x + 1,  cellPos.y), // 오른쪽
            new Vector3Int(cellPos.x - 1, cellPos.y), // 왼쪽
            new Vector3Int(cellPos.x, cellPos.y - 1), // 아래쪽
            new Vector3Int(cellPos.x, cellPos.y + 1) // 위쪽
        };
        for (int i = 0; i < BushpositionList.Count; i++) // 상하좌우
        {
            myTilemap[2].SetTile(BushpositionList[i], null); // 부쉬 타일 제거
            try
            {
                DataManager.Instance.tiledata.tileStatus[BushpositionList[i].x, BushpositionList[i].y] = 0; // 타일 상태 업데이트
            }
            catch
            {
                Debug.Log("Index를 넣어갔습니다"); // 예외 처리
            }
        }
    }

    /// <summary>
    /// 아이템을 획득하는 함수
    /// </summary>
    /// <param name="cellPos">돌을 놓는 위치</param>
    void GetItem(Vector3Int cellPos)
    {
        TileBase tilebaes = myTilemap[1].GetTile(cellPos); // 아이템 타일 가져옴
        myTilemap[1].SetTile(new Vector3Int(cellPos.x, cellPos.y), null); // 아이템 타일 제거
        UIManager.Instance.GetItem(tilebaes, cellPos); // UI 업데이트
    }
    #endregion
}



