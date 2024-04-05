using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBushSpawnSystem : MonoBehaviour
{

    public GameObject GameGeneralSystem;

    float GapSizeX;
    float GapSizeY;

    float edgeSpotX;
    float edgeSpotY;

    public int[,] BushBoard = new int[15 + 8, 15 + 8];
    public int[,] itemBoard = new int[15 + 8, 15 + 8];

    int selectedVersion = 1;
    public GameObject bush;
    public GameObject leaf;
    public GameObject dotori;



    //pasted
    public GameObject edgeSpot_1;  // Map Grid  Starting Point // Unity : Inspector
    public GameObject edgeSpot_2;  // Map Grid  Starting Point // Unity : Inspector
    public GameObject edgeSpot_3;  // Map Grid  Starting Point // Unity : Inspector
    public float edgeSpot_x;
    public float edgeSpot_y;

    int hasBeenEnteredOnItemFunction = 0;
    int hasBeenEnteredOnBushFunction = 0;

    void Start()
    {
        GameGeneralSystem = GameObject.Find("Game");

        edgeSpot_1 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(0).gameObject; //zizi point1: origin
        edgeSpot_2 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(1).gameObject; //zizi point2: gapx
        edgeSpot_3 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(2).gameObject; //zizi point3: gapy

        edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().localPosition.x;  // : by EventPosition -627
        edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().localPosition.y;  // : by EventPosition -637

        edgeSpotX = edgeSpot_2.GetComponent<RectTransform>().localPosition.x - edgeSpot_x; //-537 + 627
        edgeSpotY = edgeSpot_3.GetComponent<RectTransform>().localPosition.y - edgeSpot_y; //-560 + 637
        
        bushVersion(selectedVersion);// 0, 1
        // spawnBushAndItems();
    }

    void Update()
    {
        // GapSizeX = GameGeneralSystem.GetComponent<GameSceneSystem>().GapSize_x;
        // GapSizeY = GameGeneralSystem.GetComponent<GameSceneSystem>().GapSize_y;

        // edgeSpotX = GameGeneralSystem.GetComponent<GameSceneSystem>().edgeSpot_x;
        // edgeSpotY = GameGeneralSystem.GetComponent<GameSceneSystem>().edgeSpot_y;


        // edgeSpot_1 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(0).gameObject; //zizi point1: origin
        // edgeSpot_2 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(1).gameObject; //zizi point2: gapx
        // edgeSpot_3 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(2).gameObject; //zizi point3: gapy

        // edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().localPosition.x;  // : by EventPosition -627
        // edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().localPosition.y;  // : by EventPosition -637

        // edgeSpotX = edgeSpot_2.GetComponent<RectTransform>().localPosition.x - edgeSpot_x; //-537 + 627
        // edgeSpotY = edgeSpot_3.GetComponent<RectTransform>().localPosition.y - edgeSpot_y; //-560 + 637
        // Debug.Log("GapSizeX: " + GapSizeX);
        // Debug.Log("GapSizeY: " + GapSizeY);
        // Debug.Log("This is Screen.width: " + Screen.width/2);
        // Debug.Log("This is Screen.height: " + Screen.height/2);

        // Debug.Log("This is edgeX: " + edgeSpot_x);
        // Debug.Log("This is edgeY: " + edgeSpot_y);

        // Debug.Log("This is edge2X: " + edgeSpot_2.GetComponent<RectTransform>().localPosition.x);
        // Debug.Log("This is edge3Y: " + edgeSpot_3.GetComponent<RectTransform>().localPosition.y);

        // Debug.Log("This is edgeSpotX: " + edgeSpotX);
        // Debug.Log("This is edgeSpotY: " + edgeSpotY);
        
        // Debug.Log("This is X: " + X);
        // Debug.Log("This is Y: " + Y);
        // if (selectedVersion == 0 || selectedVersion == 1)
        // {
        //     bushVersion(selectedVersion);
        // }
    }


    void bushVersion(int _selectedNumber) //  bush 제작
    {
        int size = 0;
        int hasBeenEnteredOnItemFunction = 0;
        int hasBeenEnteredOnBushFunction = 0;
        size = _selectedNumber == 0? 10 : 5;  // 배열 사이즈

        for (int i = 0; i < 15 + 8; i++)
        {
            for (int j = 0; j < 15 + 8; j++)
            {
                // itemBoard[i, j] = 0;
                if (size > i && size > j)
                {
                    BushBoard[i, j] = 5;
                    setBush(i, j);
                    // setItem(i, j);
                    // Debug.Log(BushBoard[i, j]);
                }
                else
                {
                    BushBoard[i, j] = 0;
                }
            }
        }
    }

    void setBush(int i, int j)
    {
        hasBeenEnteredOnBushFunction += 1;
        // Debug.Log("set bush entered: " + hasBeenEnteredOnBushFunction);
        GameObject bushPrefab = Instantiate(bush); // , new Vector3(X, Y, -0.01f), Quaternion.identity) as GameObject;
        bushPrefab.transform.SetParent(GameGeneralSystem.transform.GetChild(1), false);
        // Debug.Log("This is i: " + i);
        // Debug.Log("This is j: " + j);
        float X = (j - 4) * edgeSpotX; //  - Screen.width/2;
        float Y = (i - 4) * edgeSpotY; // - Screen.height/2;
        Debug.Log($"X, Y : {X}, {Y}");
        bush.transform.position = new Vector3(X, Y, -0.01f);
        // Debug.Log("This is X: " + X);
        // Debug.Log("This is Y: " + Y);

        
        
    }

    void setItem(int i, int j)
    {
        hasBeenEnteredOnItemFunction += 1;
        Debug.Log("set Item entered: " + hasBeenEnteredOnItemFunction);
        float X = (j - 4) * edgeSpotX - Screen.width/2;
        float Y = (i - 4) * edgeSpotY - Screen.height/2;
        // Debug.Log("This is i: " + i);
        // Debug.Log("This is j: " + j);
        // Debug.Log("GapSizeX: " + GapSizeX);
        // Debug.Log("GapSizeY: " + GapSizeY);
        // Debug.Log("This is Screen.width: " + Screen.width/2);
        // Debug.Log("This is Screen.height: " + Screen.height/2);
        // Debug.Log("This is edgeSpotX: " + edgeSpotX);
        // Debug.Log("This is edgeSpotY: " + edgeSpotY);
        // Debug.Log("This is X: " + X);
        // Debug.Log("This is Y: " + Y);

        int itemNumber = UnityEngine.Random.Range(0, 20);
        if (itemNumber == 0)
        {
            itemBoard[i, j] = 3; // leaf
            //Instantiate(leaf, new Vector3(X, Y, -0.01f), Quaternion.identity);
            GameObject leafPrefab = Instantiate(leaf, new Vector3(X, Y, -0.01f), Quaternion.identity )as GameObject;
            leafPrefab.transform.SetParent(GameGeneralSystem.transform.GetChild(1), false);
        }
        else if (itemNumber == 1)
        {
            itemBoard[i,j] = 4; //dotori
            //Instantiate(dotori, new Vector3(X, Y, -0.01f), Quaternion.identity);
            GameObject dotoriPrefab = Instantiate(bush, new Vector3(X, Y, -0.01f), Quaternion.identity )as GameObject;
            dotoriPrefab.transform.SetParent(GameGeneralSystem.transform.GetChild(1), false);

        }
        else
        {

        }
    }



    // void spawnBushAndItems()
    // {

    //     xAxis = (int)GameGeneralSystem.GetComponent<GameSceneSystem>().GapSize_x;
    //     yAxis = (int)GameGeneralSystem.GetComponent<GameSceneSystem>().GapSize_y;
    //     for (int i = 0; i < GameGeneralSystem.GetComponent<GameSceneSystem>().mapGridNum_y + 8; i++)
    //     {
           
    //         for (int j = 0; j < GameGeneralSystem.GetComponent<GameSceneSystem>().mapGridNum_x + 8; j++)
    //         {
    //             Debug.Log("not inside");
    //             if (itemBoard[i, j] == 3)
    //             {
    //                 Debug.Log("working?");
    //                 Instantiate(leaf, new Vector3((j - 4) * xAxis, (i - 4) * xAxis, -0.01f), Quaternion.identity);
    //             }
    //             if (itemBoard[i, j] == 4)
    //             {
    //                 Debug.Log("working?");
    //                 Instantiate(dotori, new Vector3((j - 4) * yAxis, (i - 4) * yAxis, -0.01f), Quaternion.identity);
    //             }
    //         }
    //     }
    // }


}
