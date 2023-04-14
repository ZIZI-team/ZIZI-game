using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameSceneSystem : MonoBehaviour
{


// +++++ UI +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //


    [Header("Timer")]
    public int setTime;
    public Text gameText;                           // inspector
    float time = 20f;
    float fullTime = 20f;
    
    [Header("Main UI")]
    public GameObject MainUI;                       // inspector

    [Header("PlayerTurn Function")]
    public GameObject playerTurnIcon;               // inspector

    [Header("Pause Menu")]
    public GameObject AssignedMapPosition;          // inspector  // This object is for 'Ready Game', and this needs be replaced by 'ActualMapPosition' later
    public GameObject PauseBox;                     // inspector
    bool pauseIsOnSight = false;

    [Header("Game Result Panel")]
    public GameObject GameResultBox;                // inspector
    public GameObject mostTopCanvas;                // This object is declared for 'GameCanvas'




// +++++ Map +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    public GameObject Map;

    [SerializeField] public int[,,] ZIZIBoard = new int[11+ 12, 11+ 12, 2]; // ZIZI : B & W
    [SerializeField] public int[,,] RockBoard = new int[11+ 12, 11+ 12, 2]; 
    [SerializeField] public int[,,] BushBoard = new int[11+ 12, 11+ 12, 2]; 
    [SerializeField] public int[,,] ItemBoard = new int[11+ 12, 11+ 12, 2]; 

    // List<GameObject> ItemList = new List<GameObject>();
    List<GameObject> ZIZIList = new List<GameObject>();

    public int mapGridNum_x;
    public int mapGridNum_y;
    
    public List<List<int>> assignedList = new List<List<int>>();    


// +++++ Stone +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //
  

    GameObject Stone; 

    public GameObject Stone_b, Stone_w;     // Unity : Inspector
    RectTransform rectTransform_b;
    RectTransform rectTransform_w;

    [SerializeField] private bool isBlack = true;




// +++++ Item List +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //


    [Header("Gameplay Item System")]
    // public GameObject ActualMapPosition;    // this needs to replace 'AssignedMapPosition' in last
    public GameObject SkillUI;
    public GameObject ItemSlotUI_1P;
    public GameObject ItemSlotUI_2P;
    public GameObject leafIcon;
    public GameObject dotoriIcon;
    public List<GameObject> P1_Item = new List<GameObject>();
    public List<GameObject> P2_Item = new List<GameObject>();



// +++++ InPut & Stone Position +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //


    public float GapSize_x;
    public float GapSize_y;


    Vector3 InputPos;  // PanelOnclick(), Camera view position
    float xPos = 0f;   // InputPos.x
    float yPos = 0f;   // InputPos.y

    float xNamuji = 0f;
    float yNamuji = 0f;

    int xGapNum = 0;
    int yGapNum = 0;


    float x_correction;  // correct position
    float y_correction;  // correct position

    public GameObject edgeSpot_1;  // Map Grid  Starting Point // Unity : Inspector
    public GameObject edgeSpot_2;  // Map Grid  Starting Point // Unity : Inspector
    public GameObject edgeSpot_3;  // Map Grid  Starting Point // Unity : Inspector

    public float edgeSpot_x;
    public float edgeSpot_y;

    bool inMap = true; // is Mouse position is in map




// +++++ Win Condition +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //


    public int blackStoneCount = 0;
    public int whiteStoneCount = 0;
    public bool musicIsOn = false;

    // public GameObject Line;
    // GameObject[] Black_line;
    // GameObject[] White_line;

// ------------------------------------------------------------------------------------------------------------------------ //


// ------------------------------------------------------------------------------------------------------------------------ //

    void Start()
    {

// >> [1] Set UI Panel
        // MainUI.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(0f, 790f, -0.4f);
        // ItemSlotUI_1P.transform.localPosition = MainUI.transform.localPosition + new Vector3(0f, -391f, -0.4f); // y position is 824 in Inspector
        // ItemSlotUI_2P.transform.localPosition = MainUI.transform.localPosition + new Vector3(0f, -2215f, -0.4f);
        SkillUI.transform.localPosition += new Vector3(-1620f, 0f);


// >> [2] Set Map Grid & Stone (Initiate state)

        Map = GameObject.Find("Game").transform.GetChild(0).gameObject;
        
        // >>>>>. Screen Ratio
            panel = Map.transform.GetChild(1).gameObject.GetComponent<RectTransform>();

            // Panel Size
            panelSize = panel.rect.size;

            // Get Canvas Scaler component
            CanvasScaler canvasScaler = Map.transform.GetChild(1).gameObject.GetComponentInParent<CanvasScaler>();

            // Calculate Size by bCanvas Scaler match mode
            if (canvasScaler.matchWidthOrHeight == 0)
            {
                // Width Match
                float scaleFactor = Screen.width / canvasScaler.referenceResolution.x;
                panelSize *= scaleFactor;
            }
            else
            {
                // Height Match 
                float scaleFactor = Screen.height / canvasScaler.referenceResolution.y;
                panelSize *= scaleFactor;
            }


        // 1. Calculate Grid Size with edgePoint 1, 2, 3 : Game > Map Prefab

            edgeSpot_1 = Game.transform.GetChild(7).transform.GetChild(0).gameObject;
            edgeSpot_2 = Game.transform.GetChild(7).transform.GetChild(1).gameObject;
            edgeSpot_3 = Game.transform.GetChild(7).transform.GetChild(2).gameObject;

            edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().position.x;  // : by EventPosition // localPosition 4개
            edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().position.y;  // : by EventPosition

            GapSize_x = (edgeSpot_2.GetComponent<RectTransform>().position.x - edgeSpot_x);
            GapSize_y = (edgeSpot_3.GetComponent<RectTransform>().position.y - edgeSpot_y);


        // 2. Make Initiate Board (Record Initiate Information)
            Reset_Item();
    }


    void Reset_Item()
    {    
        // Should Make More....
        // mapGridNum_x = (int)((edgeSpot_x * (-1) * 2) / GapSize_x) + 1;    // : positive  // 10
        // mapGridNum_y = (int)((edgeSpot_y * (-1) * 2) / GapSize_y) + 1;    // : positive  // 10

        mapGridNum_x = 11;
        mapGridNum_y = 11;
        for (int i = 0; i < mapGridNum_y + 12; i++) // will reset ZIZIBoard by 0, and designate items randomly
        {
            for (int j = 0; j < mapGridNum_x + 12; j++)
            {
                ZIZIBoard[i, j, 0] = 0;     ZIZIBoard[i, j, 1] = 0;
                RockBoard[i, j, 0] = 0;     RockBoard[i, j, 1] = 0;
                BushBoard[i, j, 0] = 0;     BushBoard[i, j, 1] = 0;
                ItemBoard[i, j, 0] = 0;     ItemBoard[i, j, 1] = 0;
            }
        }


        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < mapGridNum_x + 12; j++)
            {
                ZIZIBoard[i, j, 0] = 6;
            }
        }
        for(int i = 6; i < mapGridNum_y + 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                ZIZIBoard[i, j, 0] = 6;
            }
            for(int j = mapGridNum_x + 6; j < mapGridNum_x + 12; j++)
            {
                ZIZIBoard[i, j, 0] = 6;
            }
        }
        for(int i = mapGridNum_y + 6; i < mapGridNum_y + 12; i++)
        {
            for(int j = 0; j < mapGridNum_x + 12; j++)
            {
                ZIZIBoard[i, j, 0] = 6;
            }
        }


        for (int i = 0; i < Map.transform.GetChild(0).transform.GetChild(1).childCount; i++){ Destroy(Map.transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject); } // Dotori
        for (int i = 0; i < Map.transform.GetChild(0).transform.GetChild(2).childCount; i++){ Destroy(Map.transform.GetChild(0).transform.GetChild(2).transform.GetChild(i).gameObject); } // Leaf

        for (int i = 0; i < ZIZIList.Count; i++){ Destroy(ZIZIList[i]); } // ZIZI

        // ItemList = new List<GameObject>();
        ZIZIList = new List<GameObject>();
        
        GameObject Rock = Map.transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject Bush = Map.transform.GetChild(0).transform.GetChild(3).gameObject;
        
        int RockNum = Map.transform.GetChild(0).transform.GetChild(0).childCount;  // index Max
        int BushNum = Map.transform.GetChild(0).transform.GetChild(3).childCount;  // index Max

        Dotori_P1 = 0;
        Dotori_P2 = 0;
        Leaf_P1 = 0;
        Leaf_P2 = 0;

        Dotori_Count = 0;
        Leaf_Count = 0;
        ZIZI_Count = 0;
        
        Board_Count = 0;
        for (int i = 0; i < RockNum; i++){ Get_BoardItem_Index(Rock.transform.GetChild(i).gameObject, RockBoard); } // Record Rock in Board

        Board_Count = 0;
        for (int i = 0; i < BushNum; i++){ Get_BoardItem_Index(Bush.transform.GetChild(i).gameObject, BushBoard, true); } // Record Bush in Board
        for (int i = 0; i < BushNum; i++){ Bush.transform.GetChild(i).gameObject.SetActive(true); } // SetActive true Bush in Board

        Curr_Item = GameObject.Find("ZIZI").gameObject;     

        for (int j = 0; j < P1_Item.Count; j++){ Destroy(P1_Item[j]); }
        for (int j = 0; j < P2_Item.Count; j++){ Destroy(P2_Item[j]); }

        assignedList = new List<List<int>>();

        UsedItem = false;
        P1_Item = new List<GameObject>();
        P2_Item = new List<GameObject>();

        SKill_Dotori = false;
        Debug.Log("Dotori false : Reset");
        SKill_Leaf = false;

        Turn = 0;
    }


    public int Dotori_Count = 0;
    public int Leaf_Count = 0;
    public int Board_Count = 0;     
    
    void Get_BoardItem_Index(GameObject BoardItem, int[,,] Board, bool IsBushBoard = false)
    {
        RectTransform BoardItemRect = BoardItem.GetComponent<RectTransform>();

        xPos = BoardItemRect.localPosition.x;
        yPos = BoardItemRect.localPosition.y;
        
        xNamuji = (xPos - edgeSpot_x) % GapSize_x; // xNamuji = 0.0f ~ < GapSize
        yNamuji = (yPos - edgeSpot_y) % GapSize_y; // yNamuji = 0.0f ~ < GapSize
       
    // initiate working 
        xGapNum = (int)((xPos - edgeSpot_x) / GapSize_x);  // int, Grid Index
        yGapNum = (int)((yPos - edgeSpot_y) / GapSize_y);  // int, Grid Index

    // correction working (inMap)
        xGapNum += Correction(xNamuji, GapSize_x); // xGapNum : 0 ~ 10  // 0 : 
        yGapNum += Correction(yNamuji, GapSize_y); // yGapNum : 0 ~ 10

    // Push Item To List
        Board[yGapNum + 6, xGapNum + 6, 0] = 1;
        Board[yGapNum + 6, xGapNum + 6, 1] = Board_Count++;

    // Make Item (Dotori, Leaf) if Board is BushBoard
        if (IsBushBoard == true)
        { 
            List<int> ItemPercentage = new List<int>(){1, 1, 1, 1, 1, 1, 1, 1, 2, 3};
            ItemBoard[yGapNum + 6, xGapNum + 6, 0] = ItemPercentage[Random.Range(0, ItemPercentage.Count)];
            ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] = 6;

                float ratio = (float)Screen.height / (float)3040; // width
        
                x_correction = (xGapNum * GapSize_x + edgeSpot_x);// - Screen.width/2);// * ratio;  
                y_correction = (yGapNum * GapSize_y + edgeSpot_y);// - Screen.height/2);// * ratio;

            if (ItemBoard[yGapNum + 6, xGapNum + 6, 0] == 2) // Dotori
            {
                ItemBoard[yGapNum + 6, xGapNum + 6, 1] = Dotori_Count++;
                GameObject Dotori = Instantiate(Resources.Load("Item_Prefab/"+"dotori"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
                Dotori.transform.SetParent(Map.transform.GetChild(0).transform.GetChild(1).transform, false);
            }
            else if (ItemBoard[yGapNum + 6, xGapNum + 6, 0] == 3) // Leaf
            {
                ItemBoard[yGapNum + 6, xGapNum + 6, 1] = Leaf_Count++;
                GameObject Leaf = Instantiate(Resources.Load("Item_Prefab/"+"leaf"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
                Leaf.transform.SetParent(Map.transform.GetChild(0).transform.GetChild(2).transform, false);
            }

            // None : 1
            // Dotori : 2
            // Leaf : 3
        }
    }


    public GameObject[] b_zizi = new GameObject[] {};
    
    void Update()
    {
        // Set Stone Size > Small Stone
        rectTransform_b = Stone_b.GetComponent<RectTransform>();
        rectTransform_b.sizeDelta = new Vector2(120f, 120f);
        
        rectTransform_w = Stone_w.GetComponent<RectTransform>();
        rectTransform_w.sizeDelta = new Vector2(120f, 120f);

        // Debug.Log($"{Black(list).Length}, {White(list).Length}");

        Timer();


        // edgeSpot_1.GetComponent<RectTransform>().localPosition.x
        // edgeSpot_1.GetComponent<RectTransform>().localPosition.y
    }



// >> For UI << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public GameObject TimerHand;                                           // inspector
    public void Timer()
    {
        time -= Time.deltaTime;
        gameText.text = "????���? : " + time.ToString("F1");
        TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 360f*(fullTime-time)/fullTime);

        if (time <= 0)
        {
            changePlayer(); 
        }
    }

    public void OnClickReset()
    {
        Reset_Item();

        isBlack = true;
        // playerTurnIcon.transform.position = MainUI.transform.position + new Vector3(0f, 0f, 0f);

        Time.timeScale = 1f;
        time = fullTime;
        TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        Player1Win.SetActive(false);
        Player2Win.SetActive(false);
        
        GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-5000f, 0f, 0f);
    }

    public void OnClickPause()
    {
        if (pauseIsOnSight == false)
        {
            pauseIsOnSight = true;
            PauseBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-788f, 0f, 0f);;

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            pauseIsOnSight = false;   
            PauseBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-5000f, 0f, 0f); 
        }
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
        GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-5000f, 0f, 0f);
        OnClickReset();
    }
    
    public void OnClickContinue()
    {
        Time.timeScale = 1f;
        SkillUI.transform.localPosition = new Vector3(-5000f, 0f, 0f);
        Curr_Item.SetActive(true);

        SKill_Dotori = false;
        Debug.Log("Dotori false : Continue");
        SKill_Leaf = false;
    }


// >> Game (Click Panel) << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public RectTransform panel;
    public Vector2 panelSize;

    private Vector2 initialPanelSize;

    public CanvasScaler canvasScaler;
    public Vector2 localMousePos;

    float scaleFactor = 1;


    public void PanelOnclick()
    {
        inMap = true;

    // Get Mouse Position
        // InputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, null, out localMousePos);
            panelSize = panel.rect.size;

            InputPos = localMousePos;

        // Get Mouse Position in panel's local coordinates
        
            //InputPos = Input.mousePosition; // EventSystem : Position

            // Vector2 localMousePos;
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, null, out localMousePos);
            // InputPos = localMousePos;

        xPos = InputPos.x;
        yPos = InputPos.y;
        
        xNamuji = (xPos - edgeSpot_x) % GapSize_x; // xNamuji = 0.0f ~ < GapSize
        yNamuji = (yPos - edgeSpot_y) % GapSize_y; // yNamuji = 0.0f ~ < GapSize
       
    // initiate working 
        xGapNum = (int)((xPos - edgeSpot_x) / GapSize_x);  // int, Grid Index
        yGapNum = (int)((yPos - edgeSpot_y) / GapSize_y);  // int, Grid Index

    // correction working (inMap)
        xGapNum += Correction(xNamuji, GapSize_x); // xGapNum : 0 ~ 10  // 0 : 
        yGapNum += Correction(yNamuji, GapSize_y); // yGapNum : 0 ~ 10 

        /* Correction() : 
            Namuji by Grid :
            40% >= x : 0 return
            60% <= y> : 1 return,  GapNum += 1

            // inMap : management Instantiate, #1    
            40% ~ 60% : change to : inMap = false : Instantiate X
        */            
            
            // inMap : management Instantiate, #2
            // GapNum x < 0 && x >= 11 : outside the map, Instantiate X

            // mapGridNum = 11
                if (xGapNum < 0 || xGapNum >= mapGridNum_x){ inMap = false; } // normal xGapNum : 0 ~ 10,  xGapNum abnormal     
                if (yGapNum < 0 || yGapNum >= mapGridNum_y){ inMap = false; } // normal yGapNum O : 0 ~ 10,  yGapNum abnormal 

            // clicked outside the map near the line : (Namuji < 0, negative)
                if ((xNamuji < 0 && xGapNum == 0) && (yGapNum > 0 && yGapNum < mapGridNum_y)){ inMap = true; }
                if ((yNamuji < 0 && yGapNum == 0) && (xGapNum > 0 && xGapNum < mapGridNum_x)){ inMap = true; }

    // Set correct ZIZI Position

        float ratio = (float)3040 / (float)Screen.height; // width

        x_correction = (xGapNum * GapSize_x + edgeSpot_x);// - panelSize.y * 0.5f);// * ratio;  
        y_correction = (yGapNum * GapSize_y + edgeSpot_y);// - panelSize.x * 0.5f);// * ratio;


    // Set Stone Condition
        Stone = isBlack ? Stone_b : Stone_w;
        Stone.name = isBlack ? "Player1" : "Player2";
        // Stone.name = $"{Stone.name}{now.Length}";

            Debug.Log("yjyjyjyjyj");

    // Instantiate
        if (inMap == true)
        { 

            Debug.Log("ffffff");

            // Cannot Spawn zizi on bush
            if (BushBoard[yGapNum + 6, xGapNum + 6, 0] == 1 
            && Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(BushBoard[yGapNum + 6, xGapNum + 6, 1]).gameObject.activeSelf == true){ return; } 

            Debug.Log("panel onclick add jun");
            Debug.Log(SKill_Dotori);


            AddListAndSpawn(); 

            Debug.Log("panel onclick add hoooo");
            Debug.Log(SKill_Dotori);


            Debug.Log("sjshshshshshsh");


        }        
    }


    private int Correction(float Namuji, float GapSize)
    {
        bool isNegative = false;

    // ++++ Namuji : negative ++++ //
        if (Namuji < 0)
        {
            Namuji *= (-1);
            isNegative = true;
        }

    // ++++ Namuji : psitive ++++ //
    // Namuji : 0% ~ 40%
        if (Namuji >= 0f && Namuji < GapSize * 0.4f)
        { 
            return 0; 
        }

    // Namuji : 40% ~ 60%
        else if (Namuji >= GapSize * 0.4f && Namuji < GapSize * 0.6f)
        { 
            inMap = false;              // Instantiate X
            return 0;
        }

    // Namuji : 60% ~ 100%
        else if (Namuji >= GapSize * 0.6f && Namuji < GapSize * 1f)
        { 
            if (isNegative == true){ return -1; }
            else { return 1; }
        }
        else
        {
            Debug.Log("what's wrong?");
            inMap = false;              // Instantiate X
            return 0;
        }
    }

    public int Dotori_P1 = 0;
    public int Dotori_P2 = 0;
    public int Leaf_P1 = 0;
    public int Leaf_P2 = 0;


    public void CutBush()
    {
        if (BushBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1)
        { 
            Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(BushBoard[yGapNum + 6 + 1, xGapNum + 6, 1]).gameObject.SetActive(false); 
            ZIZIBoard[yGapNum + 6 + 1, xGapNum + 6, 0] = 0;
        }
        if (BushBoard[yGapNum + 6 - 1, xGapNum + 6, 0] == 1)
        { 
            Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(BushBoard[yGapNum + 6 - 1, xGapNum + 6, 1]).gameObject.SetActive(false); 
            ZIZIBoard[yGapNum + 6 - 1, xGapNum + 6, 0] = 0;
        }
        if (BushBoard[yGapNum + 6, xGapNum + 6 + 1, 0] == 1)
        { 
            Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(BushBoard[yGapNum + 6, xGapNum + 6 + 1, 1]).gameObject.SetActive(false); 
            ZIZIBoard[yGapNum + 6, xGapNum + 6 + 1, 0] = 0;
        }
        if (BushBoard[yGapNum + 6, xGapNum + 6 - 1, 0] == 1)
        { 
            Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(BushBoard[yGapNum + 6, xGapNum + 6 - 1, 1]).gameObject.SetActive(false); 
            ZIZIBoard[yGapNum + 6, xGapNum + 6 - 1, 0] = 0;
        }
    }

    public void ItemOnclick()
    {
        if (ItemBoard[yGapNum + 6, xGapNum + 6, 0] == 2) // Dotori
        {
            if (isBlack == true && Dotori_P1 >= 3 || isBlack == false && Dotori_P2 >= 3 ){ return; } // Can get Item Max 3
            Dotori_P1 += isBlack ? 1 : 0;
            Dotori_P2 += isBlack ? 0 : 1;
            Debug.Log("Fuckfuckfuck");
            Map.transform.GetChild(0).transform.GetChild(1).transform.GetChild(ItemBoard[yGapNum + 6, xGapNum + 6, 1]).gameObject.SetActive(false);

            // (+) Game.transform.GetChild(2).transform.GetChild(ItemBoard[yGapNum + 6, xGapNum + 6, 1]).gameObject.SetActive(false);
            
            float ratio = (float)Screen.width / (float)1440;

            if (isBlack == true)
            {
                GameObject temp = Instantiate(dotoriIcon);
                temp.name = $"black_dotori + {Dotori_P1}";
                temp.transform.SetParent(ItemSlotUI_1P.transform);
                temp.transform.localPosition = new Vector3(-660f + (120 * (Dotori_P1)), 0f);
                temp.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                P1_Item.Add(temp);
            }
            else
            {
                GameObject temp = Instantiate(dotoriIcon);
                temp.name = $"white_dotori + {Dotori_P2}";
                temp.transform.SetParent(ItemSlotUI_2P.transform);
                temp.transform.localPosition = new Vector3(-660f + (120 * (Dotori_P2)), 0f);
                temp.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                P2_Item.Add(temp);
            }

            // Remove Item Information
            ItemBoard[yGapNum + 6, xGapNum + 6, 0] = 0;            
        }

        else if (ItemBoard[yGapNum + 6, xGapNum + 6, 0] == 3) // Leaf
        {
            if (isBlack == true && Leaf_P1 >= 3 || isBlack == false && Leaf_P2 >= 3 ){ return; } // Can get Item Max 3
            Leaf_P1 += isBlack ? 1 : 0;
            Leaf_P2 += isBlack ? 0 : 1;
            Map.transform.GetChild(0).transform.GetChild(2).transform.GetChild(ItemBoard[yGapNum + 6, xGapNum + 6, 1]).gameObject.SetActive(false);

            // (+) Game.transform.GetChild(3).transform.GetChild(ItemBoard[yGapNum + 6, xGapNum + 6, 1]).gameObject.SetActive(false);

            float ratio = (float)Screen.width / (float)1440;

            if (isBlack == true)
            {
                GameObject temp = Instantiate(leafIcon);
                temp.transform.SetParent(ItemSlotUI_1P.transform);
                temp.transform.localPosition = new Vector3(190f + (120 * (Leaf_P1)), 0f);
                temp.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                P1_Item.Add(temp);
            }
            else
            {
                GameObject temp = Instantiate(leafIcon);
                temp.transform.SetParent(ItemSlotUI_2P.transform);
                temp.transform.localPosition = new Vector3(190f + (120 * (Leaf_P2)), 0f);
                temp.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                P2_Item.Add(temp);
            }

            // Remove Item Information
            ItemBoard[yGapNum + 6, xGapNum + 6, 0] = 0;            
        }

        else { return; }
    }



// >> Game : Set & Record << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public GameObject Game;                           // Unity : Inspector
    public Transform ZIZI_Transform;                  // Unity : Inspector
    
    public GameObject Player1Win;                     // Unity : Inspector
    public GameObject Player2Win;                     // Unity : Inspector
    public bool skillCheckingDone = false;
    


    // ++Code : Add Parameter YJ
    public bool SKill_Dotori = false;
    public bool SKill_Leaf = false;

    public bool UsedItem = false;

    public void AddListAndSpawn()
    {
        Debug.Log("skill dotori ahahahhahaah" + SKill_Dotori);
        if (SKill_Dotori == true)
        {
            Time.timeScale = 1;
            Debug.Log("PlaySkill_    Dotori shitxoixoxoxoxoxoxo");
            // SkillUI.transform.localPosition = new Vector3(-1620f, 0f);

            if(isBlack == true && ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] == 1 || isBlack == false && ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] == 2)
            {
                Dotori_P1 -= isBlack ? 1 : 0;
                Dotori_P2 -= isBlack ? 0 : 1;

                if (isBlack == true)
                {
                    P1_Item.Remove(Curr_Item);
                }
                else
                {
                    P2_Item.Remove(Curr_Item);
                }

                PlaySkill_Dotori(yGapNum + 6, xGapNum + 6, ZIZIBoard[yGapNum + 6, xGapNum + 6, 0]);
                Debug.Log("PlaySkill_    Dotori fucking");

                Destroy(Curr_Item);
                UsedItem = true;
                changePlayer();
            }
            else { return; } 
        }

        else if (SKill_Leaf == true)
        {
            Time.timeScale = 1;
            //SkillUI.transform.localPosition = new Vector3(-1620f, 0f);

            if(isBlack == true && ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] == 1 || isBlack == false && ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] == 2)
            {
                Leaf_P1 -= isBlack ? 1 : 0;
                Leaf_P2 -= isBlack ? 0 : 1;

                PlaySkill_leaf(yGapNum + 6, xGapNum + 6);
                Debug.Log("PlaySkill_    leaf fucking");
                UsedItem = true;
                changePlayer();
                SKill_Leaf = false;
            }
            else { return; }  
        }  

        else if (ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] == 0)
        {
            Set_And_RecordPosition();
            ItemOnclick();
            CutBush();
            changePlayer();
        }

        else if (ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] != 0)
        {
            Debug.Log("Same Position");
        }

        else
        {
            Debug.Log("Whhhhat?????");
        }
        return;
    }



    public int ZIZI_Count = 0;

    public void Set_And_RecordPosition()
    {
    // Record current ZIZI Information, Instantiate ZIZI
        assignedList.Add(new List<int> {xGapNum + 6, yGapNum + 6});  
            
        GameObject ZIZILand = Instantiate(Resources.Load("SKIN_Prefab/"+"ZIZILand"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
        ZIZILand.transform.SetParent(ZIZI_Transform, false);
        // ZIZILand.transform.SetParent(Game.transform.GetChild(1).transform, false);
        
        ZIZIList.Add(ZIZILand);

        GameObject ZIZI_instance = Instantiate(Stone, new Vector3(0f, 0f, -0.01f), Quaternion.identity) as GameObject;
        ZIZI_instance.transform.SetParent(ZIZILand.transform, false);

        ZIZI_instance.tag = isBlack? "b_zizi" : "w_zizi"; 


    // Information of ZIZI Color in list
        ZIZIBoard[yGapNum + 6, xGapNum + 6, 0] = isBlack ? 1 : 2;  // 1 : Black, 2 : White
        ZIZIBoard[yGapNum + 6, xGapNum + 6, 1] = ZIZI_Count++;

        // Default Animation
            Play_Animation1();

    //add tag in list 

        
    // Check Win Condition
        WinCondition();

    // Check Animation Condition        
        Anim2Condition(); // Animation 2 : Nervous
    }

    public int Turn = 0;

    public void changePlayer()
    {
        // gepjo
        if(Turn >= 20)
        {
            Player1Win.SetActive(true);
        }

    // Check Did Player Used Item
        if (SKill_Dotori == true || SKill_Dotori == true)
        {
            Debug.Log("change player 1");
            Debug.Log(SKill_Dotori);
            if (UsedItem == false)
            { 
                Curr_Item.GetComponent<Animator>().SetBool("On", false);
                Curr_Item.GetComponent<Animator>().SetBool("Off", true);

            Debug.Log("change player 2");
            Debug.Log(SKill_Dotori);

            }
            else { UsedItem = false; 
                        Debug.Log("change player 3");
            Debug.Log(SKill_Dotori);
}
        }

            Debug.Log("change player 4");
            Debug.Log(SKill_Dotori);


        SKill_Dotori = false;
        Debug.Log("Dotori false : Change Player");
        SKill_Leaf = false;

        // Curr_Item.SetActive(true);

            Debug.Log("change player 5");
            Debug.Log(SKill_Dotori);



    // Change ZIZI Color for Next turn
        if (isBlack == true)  // Player 1
        {
            isBlack = false;
            // playerTurnIcon.transform.position = MainUI.transform.position + new Vector3(545f, 55.1f,-0.02f);
            time = fullTime;
            TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (Turn >= 10)
            {
                foreach (GameObject item in P2_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = true; }
                foreach (GameObject item in P1_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
                Turn++;
            }
        }

        else 
        {
            isBlack = true;
            // playerTurnIcon.transform.position = MainUI.transform.position + new Vector3(370f, 55.1f,-0.02f);
            time = fullTime;
            TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            if (Turn >= 10)
            {
                foreach (GameObject item in P2_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
                foreach (GameObject item in P1_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = true; }
                Turn++;
            }
        }

        // Can't Use Item Before turn 10
        if (Turn < 10)
        {
            foreach (GameObject item in P2_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
            foreach (GameObject item in P1_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
            Turn++;
        }
    }


// >> Check Condition << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public bool CheckCondition(int indexY, int indexX, bool StoneColor, 
                                Func<int, int, int, bool> Func1, Func<int, int, int, bool> Func2, Func<int, int, int, bool> Func3, Func<int, int, int, bool> Func4, int kMin) // make 5 : win
    {
        // return true : Win, return false : Pass

        int color;  // 1 : Black, 2 : White
        if (StoneColor == true){ color = 1; }
        else { color = 2; }

        bool TrueFlag = false;
        try 
        {
            for (int k = kMin; k <= 0; k++) // F
            {
                TrueFlag = Func1(indexY + k, indexX, color); // F
                if (TrueFlag == true) { return true; }
                TrueFlag = Func2(indexY, indexX + k, color); // F
                if (TrueFlag == true) { return true; }
                TrueFlag = Func3(indexY + k, indexX + k, color); // F
                if (TrueFlag == true) { return true; }
                TrueFlag = Func4(indexY + k, indexX - k, color); // F
                if (TrueFlag == true) { return true; }
            }
            return false;
        } 

        finally 
        {
            if(TrueFlag) {
                string board = "";
                for (int i = 0; i < 11 + 12; i++) {
                    string line = "";
                    for (int j = 0; j < 11 + 12; j++) {
                        line += ZIZIBoard[i, j, 0] + " ";
                    }
                    board += line + "\n";
                }
            }
        }
    }


// >> Check Win Condition : make 5 << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public bool checkCond_3 = false;
    public int StoneCount_Cond3 = 0;

    public void Check3Condition()
    {
        checkCond_3 = CheckCondition(yGapNum + 6, xGapNum + 6, isBlack, Check3_Y_Plus, Check3_X_Plus, Check3_XY_Plus, Check3_XY_Minus, -4);

        if (StoneCount_Cond3 == 5 && checkCond_3 == true && isBlack == true)
        {
            Debug.Log("black coondition 3");
        }
        else if (StoneCount_Cond3 == 5 && checkCond_3 == true && isBlack == false)
        {
            Debug.Log("White coondition 3");
        }
        else { Debug.Log("Pass"); }
    }

    public bool Check3_Y_Plus(int startPointY, int StartPointX, int color)//int i
    {
        StoneCount_Cond3 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX, 0] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Cond3 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 5, StartPointX, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }

        return true;
    }

    public bool Check3_X_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Cond3 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY, StartPointX + i, 0] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Cond3 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY, StartPointX + 5, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }

        return true;
    }

    public bool Check3_XY_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Cond3 = 0;
        
        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX + i, 0] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Cond3 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 5, StartPointX + 5, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }        
        
        return true;
    }

    public bool Check3_XY_Minus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Cond3 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX - i, 0] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Cond3 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 5, StartPointX - 5, 0] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Cond3 = 0; return false; }  

        return true;
    }


// >> Check Win Condition : make 5 << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public bool checkCond_Win = false;
    public int StoneCount_Win = 0;

    public void WinCondition()
    {
        checkCond_Win = CheckCondition(yGapNum + 6, xGapNum + 6, isBlack, Check_Y_Plus, Check_X_Plus, Check_XY_Plus, Check_XY_Minus, -4);

        if (StoneCount_Win == 5 && checkCond_Win == true && isBlack == true)
        {
            Debug.Log("Player1 Win!");
            Anim4Condition();   // Animation

            Player1Win.SetActive(true);
            // Player1Win.transform.parent.transform.parent.transform.SetAsLastSibling();
            mostTopCanvas.transform.SetAsLastSibling();
            GameOver(Player1Win);
        }
        else if (StoneCount_Win == 5 && checkCond_Win == true && isBlack == false)
        {
            Debug.Log("Player 2 Win!");
            Anim4Condition();   // Animation

            Player2Win.SetActive(true);
            mostTopCanvas.transform.SetAsLastSibling(); 
            GameOver(Player2Win);
        }
        else { Debug.Log("Pass"); }
    }


    public bool Check_Y_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Win = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX, 0] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
            else { StoneCount_Win = 0; return false; }
        }
        return true;
    }

    public bool Check_X_Plus (int startPointY, int StartPointX, int color)
    {
        StoneCount_Win = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY, StartPointX + i, 0] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
            else { StoneCount_Win = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Win = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX + i, 0] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
            else { StoneCount_Win = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Minus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Win = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX - i, 0] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
            else { StoneCount_Win = 0; return false; }
        }
        return true;
    }
  


// >> ZIZI : play Animation << //
// ------------------------------------------------------------------------------------------------------------------------ //


// Hop Animation : Default
public void Play_Animation1()
{   int cnt = 0;
        Debug.Log("-------------!!--------------");
    for(int i = 0; i < Game.transform.GetChild(1).childCount; i++)
    {
        StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim1"));
        Debug.Log(cnt++);
        // Code : Animation Played, but not active
    }
        Debug.Log("-------------!+++++!--------------");
}

IEnumerator Rand_timer(float _time, int _i, string _Anim)
{
    yield return new WaitForSeconds(_time);

    ZIZI_Transform.GetChild(_i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT(_Anim);
}

// Nervous Animation : Play By Function Anim2Condition()
public void Play_Animation2(string PlayerName) 
{
    for(int i = 0; i < Game.transform.GetChild(1).childCount; i++)
    {
        if (ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.name == PlayerName){ ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim2"); }//StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim2")); } //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim2"); }
        // Code : Animation Played, but not active
    }
}

// Dance Animation : Play By Function Anim3Condition()
public void Play_Animation3(string PlayerName)
{
    for(int i = 0; i < Game.transform.GetChild(1).childCount; i++)
    {
        if (ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.name == PlayerName){ StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim3")); } //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim3"); }
        // Code : Animation Played, but not active
    }
}

// Win Animation : Play By Function Anim4Condition()
public void Play_Animation4(string PlayerName)
{
    for(int i = 0; i < Game.transform.GetChild(1).childCount; i++)
    {
        if (ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.name == PlayerName){ StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim4")); } //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim4"); }
        // Code : Animation Played, but not active
    }
}



// ++ Code : Should Make And Add Animation


// Skill Dotori Animation(1) : Play By Function Destroy_Other_ZIZI()
public void Play_Anim_Dotori_1(int ZIZI_Index)
{
    ZIZI_Transform.GetChild(ZIZI_Index).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Skill_1"); 
    // Code : Animation Played, but not active
}


// Skill Dotori Animation(2) : Play By Function Destroy_Other_ZIZI()
public void Play_Anim_Dotori_2(int ZIZI_Index)
{
    ZIZI_Transform.GetChild(ZIZI_Index).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Skill_2"); 
    // Code : Animation Played, but not active
}


// >> Animation : Check Condition [ Play_Animation2 : Nervous ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public bool checkCond_Anim2 = false;
    public int StoneCount_Anim2 = 0;

    public void Anim2Condition()
    {
        checkCond_Anim2 = CheckCondition(yGapNum + 6, xGapNum + 6, isBlack, Anim2_Check_Y_Plus, Anim2_Check_X_Plus, Anim2_Check_XY_Plus, Anim2_Check_XY_Minus, -5);

          if (StoneCount_Anim2 == 5 && checkCond_Anim2 == true && isBlack == true)
        {
            Debug.Log("Player1 Play_Animation2! : Nervous");

            Play_Animation2("Player2(Clone)");
            Play_Animation2("Player2(Clone)_Leaf");
            
        }
        else if (StoneCount_Anim2 == 5 && checkCond_Anim2 == true && isBlack == false)
        {
            Debug.Log("Player 2 Play_Animation2! : Nervous");
            Play_Animation2("Player1(Clone)");
            Play_Animation2("Player1(Clone)_Leaf");

        }
        else { Debug.Log("Pass"); }
    }

    public bool Anim2_Check_Y_Plus(int startPointY, int StartPointX, int color)//int i
    {
        StoneCount_Anim2 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX, 0] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Anim2 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 4, StartPointX, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }

        return true;
    }

    public bool Anim2_Check_X_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Anim2 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY, StartPointX + i, 0] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Anim2 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY, StartPointX + 4, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }

        return true;
    }

    public bool Anim2_Check_XY_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Anim2 = 0;
        
        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX + i, 0] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Anim2 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 4, StartPointX + 4, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }        
        
        return true;
    }

    public bool Anim2_Check_XY_Minus(int startPointY, int StartPointX, int color)
    {
        StoneCount_Anim2 = 0;

        if (ZIZIBoard[startPointY, StartPointX, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }
        
        for (int i = 1; i < 4; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX - i, 0] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                else { StoneCount_Anim2 = 0; return false; }
        }
        
        if (ZIZIBoard[startPointY + 4, StartPointX - 4, 0] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
            else { StoneCount_Anim2 = 0; return false; }  

        return true;
    }




// >> Animation : Check Condition [ Play_Animation3 : Dance ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public bool checkCond_Anim3 = false;
    public int StoneCount_Anim3 = 0;

    public void Anim3Condition()
    {
        checkCond_Anim3 = Anim3_Check_XY();
        if (checkCond_Anim3 == true && isBlack == true)
        {
            Debug.Log("Player 1 Play_Animation3! : Dance");
            Play_Animation3("Player1(Clone)");
            Play_Animation3("Player1(Clone)_Leaf");
        }
        else if (checkCond_Anim3 == true && isBlack == false)
        {
            Debug.Log("Player 2 Play_Animation3! : Dance");
            Play_Animation3("Player2(Clone)");
            Play_Animation3("Player2(Clone)_Leaf");
        }
    }

    public bool Anim3_Check_XY()
    {
        if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
        else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
        else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
        else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
        else { return false; }
    }



// >> Animation : Check Condition [ Play_Animation4 : Win ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public void Anim4Condition()
    {
        if (isBlack == true)
        {
            Debug.Log("Player 1 Play_Animation4! : Win");
            Play_Animation4("Player1(Clone)");
            Play_Animation4("Player1(Clone)_Leaf");
        }
        else if (isBlack == false)
        {
            Debug.Log("Player 2 Play_Animation4! : Win");
            Play_Animation4("Player2(Clone)");
            Play_Animation4("Player2(Clone)_Leaf");
        }
    }    



// >> Skill : Dotori & Leaf << //
// ------------------------------------------------------------------------------------------------------------------------ //
    
    public GameObject Curr_Item;
    public void OnClickSkill(GameObject Item) // Item(Button Onclick) : Script : SkillClick : OnClickItem()
    {
        if(Curr_Item == null || Curr_Item != Item)
        {
            Curr_Item = Item;
        }

        Curr_Item.GetComponent<Animator>().SetBool("On", true);
        Curr_Item.GetComponent<Animator>().SetBool("Off", false);
        
        if(Curr_Item.CompareTag("dotori_item"))
        {
            Debug.Log("dotori_skillshitfuckshitfuck1");
            SKill_Dotori = true;
            Debug.Log("dotori_skillshitfuckshitfuck");
            Debug.Log(SKill_Dotori);


        }
        else if(Curr_Item.CompareTag ("leaf_item"))
        {
            SKill_Leaf = true;
            Debug.Log("leaf_skill");
        } 
        Debug.Log("smart dotori_skill");

    }

    public void SkillChiso()
    {
        Curr_Item.GetComponent<Animator>().SetBool("On",false);
        Curr_Item.GetComponent<Animator>().SetBool("Off", true);
        SKill_Dotori = false;
        Debug.Log("Dotori false : Skilchiso");
        SKill_Leaf = false;
    }


    public void PlaySkill_Dotori(int indexY, int indexX, int My_Color) // : Destroy Other ZIZI : Square 
    {
        int Other_color;  // 1 : Black, 2 : White
        if (My_Color == 1)
        {
            Dotori_P1 -= 1;
            Other_color = 2;
        }
        else
        {
            Other_color = 1; 
            Dotori_P2 -= 1;
        }

        Destroy_Other_ZIZI(indexY + 1, indexX, Other_color);
        Destroy_Other_ZIZI(indexY - 1, indexX, Other_color);
        Destroy_Other_ZIZI(indexY, indexX + 1, Other_color);
        Destroy_Other_ZIZI(indexY, indexX - 1, Other_color);

        Destroy_Other_ZIZI(indexY + 1, indexX + 1, Other_color);
        Destroy_Other_ZIZI(indexY + 1, indexX - 1, Other_color);
        Destroy_Other_ZIZI(indexY - 1, indexX + 1, Other_color);
        Destroy_Other_ZIZI(indexY - 1, indexX - 1, Other_color);
    }

    
    public void Destroy_Other_ZIZI(int y, int x, int OtherColor)
    {
        if(ZIZIBoard[y, x, 0] != 0) // If ZIZI exist in (x,y)
        {
            string PlayerName = ZIZI_Transform.GetChild(ZIZIBoard[y, x, 1]).transform.GetChild(0).gameObject.name;

            if (ZIZIBoard[y, x, 0] == OtherColor && PlayerName.Substring(PlayerName.IndexOf('_') + 1).Trim() != "Leaf")         // Can Destroy Other ZIZI + Leaf
            { 
                Play_Anim_Dotori_1(ZIZIBoard[y, x, 1]);     // Play Animation 1

                // Play Skill Anim
                ZIZI_Transform.GetChild(ZIZIBoard[y, x, 1]).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true); 

                // Delete ZIZI (SetActive False)
                ZIZI_Transform.GetChild(ZIZIBoard[y, x, 1]).transform.GetChild(0).gameObject.SetActive(false); 

                // Reset ZIZIBoard
                ZIZIBoard[y, x, 0] = 0;
            }
            else if (ZIZIBoard[y, x, 0] == OtherColor && PlayerName.Substring(PlayerName.IndexOf('_') + 1).Trim() == "Leaf")    // Cannot Destroy Other ZIZI + Leaf
            {
                Play_Anim_Dotori_2(ZIZIBoard[y, x, 1]);     // Play Animation 2
                return;
            }
                
            for(int i = 0; i < assignedList.Count; i++)
            {
                if (assignedList[i][0] == x && assignedList[i][1] == y)
                { 
                    assignedList.RemoveAt(i); 
                    return;
                }
            }
        }

        else { return; }
    }


    public void PlaySkill_leaf(int indexY, int indexX)
    {
        ZIZI_Transform.GetChild(ZIZIBoard[indexY, indexX, 1]).transform.GetChild(0).gameObject.name += "_Leaf";
        
            GameObject Leaf = Instantiate(Resources.Load("Item_Prefab/"+"leaf"), new Vector3(0, 0, -0.01f), Quaternion.identity) as GameObject;
            Leaf.transform.SetParent(ZIZI_Transform.GetChild(ZIZIBoard[indexY, indexX, 1]).transform, false);
            Leaf.transform.localPosition = new Vector3(0, 0, 0);
    }

//music on//
    public void OnclickMusic()
    {
       if (musicIsOn == true)
       {
        //mute = false;\
        // music = Music.GetComponent<
        Game.GetComponent<AudioSource>().mute  = true;
        musicIsOn = false;
       } 
       else
       {
        //mute = true;
        Game.GetComponent<AudioSource>().mute  = false;
        musicIsOn = true;

       }
        
    }
    

// >> Game Over << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public void GameOver(GameObject _player)
    {
        // Player1Win.SetActive(false);
        // Player2Win.SetActive(false);
        Debug.Log("GameOver");

        SKill_Dotori = false;
        Debug.Log("Dotori false : Game Over");
        SKill_Leaf = false;

        if ((GameResultBox.transform.localPosition != _player.transform.localPosition) && _player.activeSelf)
        {
            Time.timeScale = 0f;
            GameResultBox.transform.localPosition = _player.transform.localPosition;  //+ new Vector3(0f, -0f, -0.4f);
        }
        else
        {
            Time.timeScale = 0f;
            GameResultBox.transform.position = _player.transform.localPosition + new Vector3(-1620f, -1038.7f);
        }
    }
}
