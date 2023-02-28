using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameReadyHub : MonoBehaviour
{   

    public GameObject Final_Skin_1P;                       // ReadyGame_Local1P >> Hub
    public GameObject Final_Skin_2P;                       // ReadyGame_Local2P >> Hub

    public Animator controller;                            // Unity : Inspector : Animator : ReadyGame, Controller : SelectMap

    void Start()
    {
        // Set Initiate Skin
        Final_Skin_1P = Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp1");
        Final_Skin_2P = Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp2");
        

        // PlayerPrefs : PlayerMode
        // 1 : Single_1P
        // 2 : LocalM_2P
        // 3 : OnlineMulti

        // PlayerPrefs : GameMode
        // 1 : Original
        // 2 : Classic
        // 3 : Dynamic
        
        switch(PlayerPrefs.GetInt("PlayerMode")){
            case 1 :
                Destroy(GameObject.Find("2PScript").GetComponent<ReadyGame_Local2P>());
                break;
            case 2 :
                // Destroy(GameObject.Find("2PScript").GetComponent<ReadyGame_Local1P>());
                break;
            case 3 :
                // Add Online script 
                break;
        }
    }

    // Unity : Finish Skin Onclick
    public bool DidYouSelect_Skin1 = false;
    public bool DidYouSelect_Skin2 = false;
    public void Finish_SelectSkin()
    {
        if (PlayerPrefs.GetInt("PlayerMode") == 1 && DidYouSelect_Skin1 == true)
        {
            controller.SetBool("Finish", true);
            ShowMapPalette();
        }
        else if (PlayerPrefs.GetInt("PlayerMode") == 2 && DidYouSelect_Skin1 == true && DidYouSelect_Skin2 == true)
        {
            controller.SetBool("Finish", true);
            ShowMapPalette();
        }
        else return;
    }

    // +++ MAP +++ //
    // Map Prefab : MapName(Str) + MapImg(Img) + MapRule(Script)
    private List<GameObject> Map = new List<GameObject>();
    public int MapIndex = 0;

    public GameObject MapPalette;                      // Start : Find
    public GameObject newMap;                          // Start : Instantiate

    public GameObject Selected_MapPalette;             // ShowMapPalette() : Find 
    public GameObject Selected_Map;                    // Finish_SelectMap()


    public void ShowMapPalette()
    {
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/MAP1"));  
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/MAP2"));
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/MAP3")); 

        MapPalette = GameObject.Find("MAP");
        Selected_MapPalette = GameObject.Find("Finish MAP");

        // Set initiate Map Prefab
        newMap = Instantiate(Map[0], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 900f, 900f);
    }

    public void MapIndexUp()
    {
        Destroy(newMap);
        if (MapIndex == Map.Count - 1){ MapIndex = 0; }
        else { MapIndex++; }

        newMap = Instantiate(Map[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 900f, 900f);
    }

    public void MapIndexDown()
    {
        Destroy(newMap); 
        if (MapIndex == 0){ MapIndex = 2; }
        else { MapIndex--; }

        newMap = Instantiate(Map[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 900f, 900f);
    }


    public GameObject Game;     // Unity : Inspector


    // Unity : Selected_MapPalette Onclick
    public bool DidYouSelect_Map = false;
    public GameObject GameMap;
    public void Finish_SelectMap()
    {
        if (Selected_Map != null){ Destroy(Selected_Map); }
        
        Selected_Map = Instantiate(newMap, new Vector3(0, -500, 0), Quaternion.identity);
        Selected_Map.transform.SetParent(Selected_MapPalette.transform, false);
        SetMapSize(Selected_Map, 900f, 900f);

        // Must Select Map
        DidYouSelect_Map = true;


        // Set Map In Game Panel
        if (Selected_Map != null){ Destroy(GameMap); }
        GameMap = Instantiate(Map[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        GameMap.transform.SetParent(Game.transform, false);
        GameMap.transform.SetSiblingIndex(0);
    }

    RectTransform rectTransform_Map;
    public void SetMapSize(GameObject Map, float width, float height)
    {
        rectTransform_Map = Map.GetComponent<RectTransform>();
        rectTransform_Map.sizeDelta = new Vector2(width, height);
    }


    // Unity : Start Game Onclick
    public void StartGame()
    {
        if (DidYouSelect_Map == true)
        {
            Game.SetActive(true);
            gameObject.SetActive(false);
        }

        GameObject.Find("Game").GetComponent<GameSceneSystem>().Stone_b = Final_Skin_1P;
        GameObject.Find("Game").GetComponent<GameSceneSystem>().Stone_w = Final_Skin_2P;
        
    }

    void Update()
    {

    }
}
