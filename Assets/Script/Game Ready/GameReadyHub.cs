using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameReadyHub : MonoBehaviour
{   

    public GameObject Final_Skin_1P;                       // ReadyGame_Local1P >> Hub
    public GameObject Final_Skin_2P;                       // ReadyGame_Local2P >> Hub

    // public Animator controller;                            // Unity : Inspector : Animator : ReadyGame, Controller : SelectMap


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
            GameObject.Find("ReadyGame").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("ReadyGame").transform.GetChild(3).gameObject.SetActive(true);
            ShowMapPalette();
        }
        else if (PlayerPrefs.GetInt("PlayerMode") == 2 && DidYouSelect_Skin1 == true && DidYouSelect_Skin2 == true)
        {
            GameObject.Find("ReadyGame").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("ReadyGame").transform.GetChild(3).gameObject.SetActive(true);
            ShowMapPalette();
        }
        else return;

        // controller.SetBool("Finish", true);
    }


    // +++ MAP +++ //
    // Map Prefab : MapName(Str) + MapImg(Img) + MapRule(Script)
    private List<GameObject> MapImg = new List<GameObject>();
    private List<GameObject> Map = new List<GameObject>();
    
    public int MapIndex = 0;

    public GameObject MapPalette;                      // Start : Find
    public GameObject newMap;                          // Start : Instantiate

    public GameObject Selected_MapPalette;             // ShowMapPalette() : Find 
    public GameObject Selected_Map;                    // Finish_SelectMap()


    public void ShowMapPalette()
    {
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP2"));  
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP3"));
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP4")); 

        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/BushMap1"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/BushMap2"));
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/BushMap3")); 

        MapPalette = GameObject.Find("MAP");
        // Selected_MapPalette = GameObject.Find("Finish MAP");

        // Set initiate Map Prefab
        newMap = Instantiate(MapImg[0], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 600f, 600f);

        // Set initiate Map In Game Panel
        GameMap = Instantiate(Map[0], new Vector3(0, 0, 0), Quaternion.identity);
        GameMap.transform.SetParent(Game.transform, false);
        GameMap.transform.SetSiblingIndex(0);
    }

    public void MapIndexUp() // no use
    {
        Destroy(newMap);
        if (MapIndex == MapImg.Count - 1){ MapIndex = 0; }
        else { MapIndex++; }

        newMap = Instantiate(MapImg[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 600f, 600f);
    }

    public void MapIndexDown() // no use
    {
        Destroy(newMap); 
        if (MapIndex == 0){ MapIndex = 2; }
        else { MapIndex--; }

        newMap = Instantiate(MapImg[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 600f, 600f);
    }

    public GameObject Game;     // Unity : Inspector


    // Unity : Selected_MapPalette Onclick
    public GameObject GameMap;

    public void Finish_SelectMap(GameObject MapChart)
    {
        if (Selected_Map != null){ Destroy(Selected_Map); }
        
            // Selected_Map = Instantiate(newMap, new Vector3(0, -500, 0), Quaternion.identity);
            // Selected_Map.transform.SetParent(Selected_MapPalette.transform, false);
            // SetMapSize(Selected_Map, 500f, 500f);

        MapIndex = MapChart.transform.GetSiblingIndex();
        Destroy(newMap); 

        newMap = Instantiate(MapImg[MapIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 600f, 600f);

        Selected_Map = newMap;
        
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

    public void GoBack_1()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GoBack_2()
    {
        GameObject.Find("ReadyGame").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("ReadyGame").transform.GetChild(3).gameObject.SetActive(false);

        // Set initiate Map Prefab
        newMap = Instantiate(MapImg[0], new Vector3(0, 0, 0), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 600f, 600f);
    }

    // Unity : Start Game Onclick
    public void StartGame()
    {
        Game.SetActive(true);
        gameObject.SetActive(false);

        GameObject.Find("Game").GetComponent<GameSceneSystem>().Stone_b = Final_Skin_1P;
        GameObject.Find("Game").GetComponent<GameSceneSystem>().Stone_w = Final_Skin_2P;
    }

    void Update()
    {

    }
}
