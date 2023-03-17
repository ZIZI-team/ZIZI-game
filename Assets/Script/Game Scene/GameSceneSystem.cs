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
    public Text gameText;
    public float second = 5f;
    
    [Header("Gameplay Panel")]
    public GameObject GameplayUI;


    [Header("PlayerTurn Function")]
    public GameObject playerTurnIcon;

    [Header("Pause Menu")]
    public GameObject AssignedMapPosition;  // This object is for 'Ready Game', and this needs be replaced by 'ActualMapPosition' later
    public GameObject PauseBox;
    bool pauseIsOnSight = false;

    [Header("Each Players Stone Spawn Status")]
    public Text firstPlayerStoneStatus;
    public Text secondPlayerStoneStatus;
    public int player1StoneCounting = 0;
    public int player2StoneCounting = 0;

    [Header("Game Result Panel")]
    public GameObject GameResultBox;
    public GameObject mostTopCanvas;                  // This object is declared for 'ClickCanvas'





// +++++ Map +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    public GameObject Map;

    [SerializeField] public int[,] ZIZIBoard = new int[11+8, 11+8]; // ZIZI : B & W
    [SerializeField] public int[,] RockBoard = new int[11+8, 11+8]; 
    [SerializeField] public int[,] BushBoard = new int[11+8, 11+8]; 
    [SerializeField] public int[,] ItemBoard = new int[11+8, 11+8]; 

    List<GameObject> ItemList = new List<GameObject>();
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

    public GameObject[] Black;
    public GameObject[] White;

    [SerializeField] private bool isBlack = true;
 
    public GameObject[] now;
    public string stone_name;

    // public Text b_num, w_num;





// +++++ Item List +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //


    // [Header("Gameplay Item System")]
    // public GameObject ActualMapPosition;    // this needs to replace 'AssignedMapPosition' in last
    // public GameObject firstPlayerGameplayItemSlotUI;
    // public GameObject secondPlayerGameplayItemSlotUI;
    // public GameObject leaf;
    // public GameObject dotori;
    
    // // [SerializeField] private int leafItemForPlayerOne = 0;
    // // [SerializeField] private int dotoriItemForPlayerOne = 0;
    // // [SerializeField] private int leafItemForPlayerTwo = 0;
    // // [SerializeField] private int dotoriItemForPlayerTwo = 0;

    // // [SerializeField] private GameObject[] leafItemSlotForPlayerOne;
    // // [SerializeField] private GameObject[] DotoriItemSlotForPlayerOne;
    // // [SerializeField] private GameObject[] leafItemSlotForPlayerTwo;
    // // [SerializeField] private GameObject[] DotoriItemSlotForPlayerTwo;
    // // [SerializeField] public int[,] itemBoard = new int[11+8, 11+8];
    
    
    // // [Header("BushList Load Object")]
    // // public int[,] mapBushList;
    // // public int[,] newMapBushList = new int[11+8, 11+8];

    // public GameObject bushSpawn;
    // // public Game GameThing;
    




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

    // public GameObject Line;
    // GameObject[] Black_line;
    // GameObject[] White_line;




// ------------------------------------------------------------------------------------------------------------------------ //


    void Start()
    {

// >> [1] Set UI Panel
        GameplayUI.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(0f, 790f, -0.4f);


// >> [2] Set Map Grid & Stone (Initiate state)

        // 1. Calculate Grid Size with edgePoint 1, 2, 3 : Game > Map Prefab

            Map = GameObject.Find("Game").transform.GetChild(0).gameObject;

            edgeSpot_1 = Map.transform.GetChild(1).gameObject;
            edgeSpot_2 = Map.transform.GetChild(2).gameObject;
            edgeSpot_3 = Map.transform.GetChild(3).gameObject;

            edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().position.x;  // : by EventPosition
            edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().position.y;  // : by EventPosition

            GapSize_x = edgeSpot_2.GetComponent<RectTransform>().position.x - edgeSpot_x;
            GapSize_y = edgeSpot_3.GetComponent<RectTransform>().position.y - edgeSpot_y;


        // 2. Stone List & Tag

            Black = GameObject.FindGameObjectsWithTag("b_zizi");
            White = GameObject.FindGameObjectsWithTag("w_zizi");

            firstPlayerStoneStatus.text = "Player1 Stone Counting : " + Black.Length.ToString();
            secondPlayerStoneStatus.text = "Player2 Stone Counting : " + White.Length.ToString();   

        // 3. Make Initiate Board (Record Initiate Information)
            Reset_Item();   
    }


    void Reset_Item()
    {    
        // mapGridNum_x = (int)((edgeSpot_x * (-1) * 2) / GapSize_x) + 1;    // : positive  // 10
        // mapGridNum_y = (int)((edgeSpot_y * (-1) * 2) / GapSize_y) + 1;    // : positive  // 10

        mapGridNum_x = 11;
        mapGridNum_y = 11;
        for (int i = 0; i < mapGridNum_y + 8; i++) // will reset ZIZIBoard by 0, and designate items randomly
        {
            for (int j = 0; j < mapGridNum_x + 8; j++)
            {
                ZIZIBoard[i, j] = 0;
                RockBoard[i, j] = 0;
                BushBoard[i, j] = 0;
                ItemBoard[i, j] = 0;
            }
        }

        for(int i = 0; i < Game.transform.GetChild(2).childCount + Game.transform.GetChild(3).childCount; i++){ Destroy(ItemList[i]); }
        for(int i = 0; i < ZIZIList.Count; i++){ Destroy(ZIZIList[i]); }

        ItemList = new List<GameObject>();
        ZIZIList = new List<GameObject>();
        
        GameObject Rock = Map.transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject Bush = Map.transform.GetChild(0).transform.GetChild(1).gameObject;
        
        int RockNum = Map.transform.GetChild(0).transform.GetChild(0).childCount;
        int BushNum = Map.transform.GetChild(0).transform.GetChild(1).childCount;

        

        for(int i = 0; i < RockNum; i++){ Get_BoardItem_Index(Rock.transform.GetChild(i).gameObject, RockBoard); } // Record Rock in Board
        for(int i = 0; i < BushNum; i++){ Get_BoardItem_Index(Bush.transform.GetChild(i).gameObject, BushBoard, true); } // Record Bush in Board

        // Debug.Log("==================================");
        // Debug.Log(RockBoard[0,0]);
        // Debug.Log(BushBoard[0,0]);
    }

    void Get_BoardItem_Index(GameObject BoardItem, int[,] Board, bool IsBushBoard = false)
    {
        RectTransform BoardItemRect = BoardItem.GetComponent<RectTransform>();

        xPos = BoardItemRect.position.x;
        yPos = BoardItemRect.position.y;
        
        xNamuji = (xPos - edgeSpot_x) % GapSize_x; // xNamuji = 0.0f ~ < GapSize
        yNamuji = (yPos - edgeSpot_y) % GapSize_y; // yNamuji = 0.0f ~ < GapSize
       
    // initiate working 
        xGapNum = (int)((xPos - edgeSpot_x) / GapSize_x);  // int, Grid Index
        yGapNum = (int)((yPos - edgeSpot_y) / GapSize_y);  // int, Grid Index

    // correction working (inMap)
        xGapNum += Correction(xNamuji, GapSize_x); // xGapNum : 0 ~ 10  // 0 : 
        yGapNum += Correction(yNamuji, GapSize_y); // yGapNum : 0 ~ 10

    // Push Item To List
        Board[yGapNum, xGapNum] = 1;


    // Make Item (Dotori, Leaf) if Board is BushBoard
        if(IsBushBoard == true)
        { 
            ItemBoard[yGapNum, xGapNum] = Random.Range(1, 4);
            
                x_correction = xGapNum * GapSize_x + edgeSpot_x - Screen.width/2;  
                y_correction = yGapNum * GapSize_y + edgeSpot_y - Screen.height/2;

            if(ItemBoard[yGapNum, xGapNum] == 2)
            {
                GameObject Dotori = Instantiate(Resources.Load("Item_Prefab/"+"dotori"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
                Dotori.transform.SetParent(Game.transform.GetChild(2).transform, false);
                ItemList.Add(Dotori);
            }
            else if(ItemBoard[yGapNum, xGapNum] == 3)
            {
                GameObject Leaf = Instantiate(Resources.Load("Item_Prefab/"+"leaf"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
                Leaf.transform.SetParent(Game.transform.GetChild(3).transform, false);
                ItemList.Add(Leaf);
            }
            // None : 1
            // Dotori : 2
            // Leaf : 3
        }
    }

    
    void Update()
    {
        // Set Stone Size > Small Stone
        rectTransform_b = Stone_b.GetComponent<RectTransform>();
        rectTransform_b.sizeDelta = new Vector2(120f, 120f);
        
        rectTransform_w = Stone_w.GetComponent<RectTransform>();
        rectTransform_w.sizeDelta = new Vector2(120f, 120f);


        Debug.Log($"{Black.Length}, {White.Length}");

        Timer();

        // edgeSpot_1.GetComponent<RectTransform>().localPosition.x
        // edgeSpot_1.GetComponent<RectTransform>().localPosition.y
    }




// >> For UI << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public void Timer()
    {
        second -= Time.deltaTime;
        gameText.text = "타이머 : " + second.ToString("F1");
        if (second <= 0){ changePlayer(); }
    }


    public void OnClickReset()
    {
        assignedList.Clear();
        Reset_Item();
        for(int j = 0; j < Black.Length; j++){ Destroy(Black[j]); }
        for(int j = 0; j < White.Length; j++){ Destroy(White[j]); }

        isBlack = true;
        playerTurnIcon.transform.position = GameplayUI.transform.position + new Vector3(545f, 55.1f,-0.02f);
        Time.timeScale = 1f;
        second = 5f;
        Player1Win.SetActive(false);
        Player2Win.SetActive(false);
        
        GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-1230f, 2000f, 0f);
    }

    public void OnClickPause()
    {
        if (pauseIsOnSight == false)
        {
            pauseIsOnSight = true;
            PauseBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position;

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            pauseIsOnSight = false;   
            PauseBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-1230f, 0f, 0f);
        }
    }


    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
        GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-1230f, 2000f, 0f);
    }




// >> Game (Click Panel) << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public void PanelOnclick()
    {
        inMap = true;

    // Get Mouse Position
        // InputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InputPos = Input.mousePosition;     // EventSystem : Position

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
        x_correction = xGapNum * GapSize_x + edgeSpot_x - Screen.width/2;  // Grid Position for zizi 
        y_correction = yGapNum * GapSize_y + edgeSpot_y - Screen.height/2;  // Grid Position for zizi 


    // Set Stone Condition
        Stone = isBlack ? Stone_b : Stone_w;
        stone_name = isBlack ? "b_zizi_pure" : "zizi_pure";
        now = isBlack ? Black : White;
        Stone.name = $"{stone_name}{now.Length}";

    // Instantiate
        if (inMap == true){ AddListAndSpawn(); }        
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
            Debug.Log("40%");
            return 0; 
        }

    // Namuji : 40% ~ 60%
        else if (Namuji >= GapSize * 0.4f && Namuji < GapSize * 0.6f)
        { 
            Debug.Log("nononono");
            inMap = false;              // Instantiate X
            return 0;
        }

    // Namuji : 60% ~ 100%
        else if (Namuji >= GapSize * 0.6f && Namuji < GapSize * 1f)
        { 
            Debug.Log("60%");

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



// >> Game : Set & Record << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public GameObject Game;                           // Unity : Inspector

    public bool stoneWinner = false;
    public GameObject Player1Win;                     // Unity : Inspector
    public GameObject Player2Win;                     // Unity : Inspector
    public int StoneCount = 0;

    public void AddListAndSpawn()
    {
    // Check Same Position of ZIZI list
        if (assignedList.Count == 0) 
        {
            Set_And_RecordPosition();
            changePlayer();
        }
        else
        {
            bool isDuplicated = false;
            for(int i = 0; i < assignedList.Count; i++)
            {
                if (assignedList[i][0] == xGapNum && assignedList[i][1] == yGapNum)
                {
                    Debug.Log("Same Position");
                    isDuplicated = true;
                    break;
                }
            }
            if(isDuplicated == false)
            {
                Set_And_RecordPosition();
                changePlayer();
                Debug.Log(ZIZIBoard[yGapNum + 4, xGapNum + 4]);
            }
            else
            {
                Debug.Log("????????");
            }
        }
    }

    public void Set_And_RecordPosition()
    {
    // Record current ZIZI Information, Instantiate ZIZI
        assignedList.Add(new List<int> {xGapNum, yGapNum});  
            
        GameObject ZIZILand = Instantiate(Resources.Load("SKIN_Prefab/"+"ZIZILand"), new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
        ZIZILand.transform.SetParent(Game.transform.GetChild(1).transform, false);
        ZIZIList.Add(ZIZILand);

        GameObject ZIZI_instance = Instantiate(Stone, new Vector3(0f, 0f, -0.01f), Quaternion.identity) as GameObject;
        ZIZI_instance.transform.SetParent(ZIZILand.transform, false);

        ZIZI_instance.tag = isBlack? "b_zizi" : "w_zizi"; 


    // Information of ZIZI Color in list
        ZIZIBoard[yGapNum + 4, xGapNum + 4] = isBlack ? 1 : 2;  // 1 : Black, 2 : White

    // Win Condition
        stoneWinner = winCondition(yGapNum + 4, xGapNum + 4, isBlack);

        if (StoneCount == 5 && stoneWinner == true && isBlack == true)
        {
            Debug.Log("Player1 Win!");
            Player1Win.SetActive(true);
            // Player1Win.transform.parent.transform.parent.transform.SetAsLastSibling();
            mostTopCanvas.transform.SetAsLastSibling();
            GameOver();
        }
        else if (StoneCount == 5 && stoneWinner == true && isBlack == false)
        {
            Debug.Log("Player 2 Win!");
            Player2Win.SetActive(true);
            mostTopCanvas.transform.SetAsLastSibling(); 
            GameOver();
        }
        else { Debug.Log("Pass"); }
    }

    
    public void changePlayer()
    {
    // Change ZIZI Color for Next turn
        if (isBlack) 
        {
            isBlack = false;
            playerTurnIcon.transform.position = GameplayUI.transform.position + new Vector3(370f, 55.1f,-0.02f);
            second = 5f;
        }
        else 
        {
            isBlack = true;
            playerTurnIcon.transform.position = GameplayUI.transform.position + new Vector3(545f, 55.1f,-0.02f);
            second = 5f;
        }
    }



// >> Win Condition << //
// ------------------------------------------------------------------------------------------------------------------------ //


    public bool winCondition(int indexY, int indexX, bool StoneColor) // make 5 : win
    {
        // return true : Win, return false : Pass

        int color;  // 1 : Black, 2 : White
        if (StoneColor == true){ color = 1; }
        else { color = 2; }

        bool winflag = false;
        try {

            for (int k = -4; k <= 0; k++)
            {
                winflag = Check_Y_Plus(indexY + k, indexX, color);
                if (winflag == true) { return true; }
                winflag = Check_X_Plus(indexY, indexX + k, color);
                if (winflag == true) { return true; }
                winflag = Check_XY_Plus(indexY + k, indexX + k, color);
                if (winflag == true) { return true; }
                winflag = Check_XY_Minus(indexY + k, indexX - k, color);
                if (winflag == true) { return true; }
            }
            return false;
        } finally {
            if(winflag) {
                string board = "";
                for (int i = 0; i < 23; i++) {
                    string line = "";
                    for (int j = 0; j < 23; j++) {
                        line += ZIZIBoard[i, j] + " ";
                    }
                    board += line + "\n";
                }
                Debug.Log("Game ended\n" + board);
            }
        }
    }

    public bool Check_Y_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_X_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY, StartPointX + i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX + i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Minus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ZIZIBoard[startPointY + i, StartPointX - i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }
  


// >> Game Over << //
// ------------------------------------------------------------------------------------------------------------------------ //

    public void GameOver()
    {
        // Player1Win.SetActive(false);
        // Player2Win.SetActive(false);
        if (GameResultBox.transform.position != AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position)
        {
            Time.timeScale = 0f;
            GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(0f, -425f, -0.4f);
        }
        else
        {
            Time.timeScale = 0f;
            GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-1230f, 2000f, 0.04f);
        }
    }
}
