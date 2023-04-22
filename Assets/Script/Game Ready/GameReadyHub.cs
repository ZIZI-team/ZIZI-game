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


    public void ReadyMapSource()
    {
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/Classic"));  

        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP1"));  
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP2"));  
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP3"));
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP4")); 
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP5")); 
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP6")); 
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP7")); 
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP8")); 
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/BushMAP9")); 

        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/ClassicMap"));  

        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage1"));
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage2")); 
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage3"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage4"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage5"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage6"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage7"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage8"));  
        MapImg.Add(Resources.Load<GameObject>("MAP_Prefab/Map_Img/MapImage9"));  

        MapPalette = GameObject.Find("MAP");
    }


    public GameObject SelectMapButton; // inspector
    public GameObject Panel; // inspector
    public RectTransform PanelRect; // inspector

    public void MakeMapButton()
    {
        int MapNum = Map.Count;
        PanelRect.sizeDelta = new Vector2(850f, 300f * (MapNum) - 60f);
        PanelRect.localPosition = new Vector3(0f, -PanelRect.sizeDelta.y/2 + 860f/2 , 0f);
        GameObject Button;

        for(int i = 0; i < MapNum; i++)
        {
            Button = Instantiate(SelectMapButton, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Button.transform.SetParent(Panel.transform, false);
            Button.transform.localPosition = new Vector3(0f, (PanelRect.sizeDelta.y/2-120f) -300f*i, 0f);
        }
    }

    public void ShowMapPalette()
    {
        ReadyMapSource();
        MakeMapButton();

        // Set initiate Map image
        if (newMap != null){ Destroy(newMap); }
        newMap = Instantiate(MapImg[0], new Vector3(0f, -60f, 0f), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 800f, 800f);

        // Set initiate Map In Game Panel
        if (GameMap != null){ Destroy(GameMap); }
        GameMap = Instantiate(Map[0], new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameMap.transform.SetParent(Game.transform, false);
        GameMap.transform.SetSiblingIndex(0);
    }

    #region MapIndexUp, MapIndexDown // no use

        public void MapIndexUp() 
        {
            Destroy(newMap);
            if (MapIndex == MapImg.Count - 1){ MapIndex = 0; }
            else { MapIndex++; }

            newMap = Instantiate(MapImg[MapIndex], new Vector3(0f, -60f, 0f), Quaternion.identity);
            newMap.transform.SetParent(MapPalette.transform, false);
            SetMapSize(newMap, 800f, 800f);
        }
        
        public void MapIndexDown() 
        {
            Destroy(newMap); 
            if (MapIndex == 0){ MapIndex = 2; }
            else { MapIndex--; }

            newMap = Instantiate(MapImg[MapIndex], new Vector3(0f, -60f, 0f), Quaternion.identity);
            newMap.transform.SetParent(MapPalette.transform, false);
            SetMapSize(newMap, 800f, 800f);
        }

    #endregion

    public GameObject Game;     // Unity : Inspector


    public GameObject GameMap;

    public void Finish_SelectMap(GameObject MapChart)
    {
        MapIndex = MapChart.transform.GetSiblingIndex();

        if (newMap != null){ Destroy(newMap); }
        newMap = Instantiate(MapImg[MapIndex], new Vector3(0f, -60f, 0f), Quaternion.identity);
        newMap.transform.SetParent(MapPalette.transform, false);
        SetMapSize(newMap, 800f, 800f);

        
        // Set Map In Game Panel
        if (GameMap != null){ Destroy(GameMap); }
        GameMap = Instantiate(Map[MapIndex], new Vector3(0f, 0f, 0f), Quaternion.identity);
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
        PlayerPrefs.SetInt("GameScene", 1);
        SceneManager.LoadScene("TitleScene");
    }

    public void GoBack_2()
    {
        GameObject.Find("ReadyGame").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("ReadyGame").transform.GetChild(3).gameObject.SetActive(false);
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
