using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public Tiles myTile;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<TileBase> _tilebase;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        myTile.InitSetting();
        if (myTile.data.TileName == "MainTile")
        {
            myTile.SettingTile(_tilemap, _tilebase);
        }
        else
        {
            myTile.SettingTile(_tilemap, _tilebase, randomnum: 8);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = _tilemap.WorldToCell(mouseWorldPos);
            Debug.Log("Clicked tile position: " + cellPos);
            // 클릭한 타일의 좌표를 반환하거나 해당 위치에 대한 추가 작업 수행
            // 예를 들어, 클릭한 위치에 대한 타일 정보 가져오기 등의 작업을 수행할 수 있습니다.
        }
    }

}
