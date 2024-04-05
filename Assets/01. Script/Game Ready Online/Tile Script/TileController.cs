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
            // Ŭ���� Ÿ���� ��ǥ�� ��ȯ�ϰų� �ش� ��ġ�� ���� �߰� �۾� ����
            // ���� ���, Ŭ���� ��ġ�� ���� Ÿ�� ���� �������� ���� �۾��� ������ �� �ֽ��ϴ�.
        }
    }

}
