using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;




public class GameSceneSystem : MonoBehaviour
{

// +++ Stone +++ //

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


// +++ Win Condition +++ //

    public int blackStoneCount = 0;
    public int whiteStoneCount = 0;

    // public GameObject Line;
    // GameObject[] Black_line;
    // GameObject[] White_line;


// +++ Position +++ //

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


// +++ Map List +++ //

    [SerializeField] public int[,] ColorBoard = new int[15+8, 15+8]; // ���
    public int mapGridNum_x;
    public int mapGridNum_y;
    
    public List<List<int>> assignedList = new List<List<int>>();    // �ߺ� �Ǻ�

    [Header("Timer")]

    public int setTime;
    public Text gameText;
    public float second = 5f;
    
    [Header("Gameplay Panel")]
    public GameObject GameplayUI;

    [Header("Gameplay Item System")]
    public GameObject ActualMapPosition;    // this needs to replace 'AssignedMapPosition' in last
    public GameObject firstPlayerGameplayItemSlotUI;
    public GameObject secondPlayerGameplayItemSlotUI;
    public GameObject leaf;
    public GameObject dotori;
    
    // [SerializeField] private int leafItemForPlayerOne = 0;
    // [SerializeField] private int dotoriItemForPlayerOne = 0;
    // [SerializeField] private int leafItemForPlayerTwo = 0;
    // [SerializeField] private int dotoriItemForPlayerTwo = 0;

    // [SerializeField] private GameObject[] leafItemSlotForPlayerOne;
    // [SerializeField] private GameObject[] DotoriItemSlotForPlayerOne;
    // [SerializeField] private GameObject[] leafItemSlotForPlayerTwo;
    // [SerializeField] private GameObject[] DotoriItemSlotForPlayerTwo;
    [SerializeField] public int[,] itemBoard = new int[15+8, 15+8];
    
    
    [Header("BushList Load Object")]

    public int[,] mapBushList;
    public int[,] newMapBushList = new int[15+8, 15+8];

    public GameObject bushSpawn;
    // public Game GameThing;


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
    

   




    void Start()
    {
        //Gameplay UI active
        GameplayUI.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(0f, 790f, -0.4f);




        //make if statement for determine whether is classic mod or original mod
    
        firstPlayerGameplayItemSlotUI.transform.position = ActualMapPosition.transform.GetChild(0).transform.position + new Vector3(0f, 813f, -0.4f);
        secondPlayerGameplayItemSlotUI.transform.position = ActualMapPosition.transform.GetChild(0).transform.position + new Vector3(0f, -793f, -0.4f);
        
        
        
        GameObject temp_bush = Instantiate(bushSpawn);
//        temp_bush.transform.parent.transform.parent = Game.transform;


        temp_bush.transform.SetParent(Game.transform.GetChild(1).transform, false);


        // mapBushList = temp_bush.GetComponent<MapBushSpawnSystem>().BushBoard.Clone() as int[,];
        // Debug.Log($"Clone 2,3 : {mapBushList[2,2]}");
        // Debug.Log($"Clone 2,3 : {temp_bush.GetComponent<MapBushSpawnSystem>().BushBoard[2,2]}");

        

        GameObject temp = Instantiate(leaf);
        temp.transform.SetParent(firstPlayerGameplayItemSlotUI.transform);
        temp.transform.position =  firstPlayerGameplayItemSlotUI.transform.position + new Vector3(-626, -30, -0.5f);




        // Set Stone Size > Small Stone
        rectTransform_b = Stone_b.GetComponent<RectTransform>();
        rectTransform_b.sizeDelta = new Vector2(100f, 100f);
        
        rectTransform_w = Stone_w.GetComponent<RectTransform>();
        rectTransform_w.sizeDelta = new Vector2(100f, 100f);


        // Set Initiate Color Board
        // mapGridNum_x = (int)((edgeSpot_x * (-1) * 2) / GapSize_x) + 1;    // : positive  // ��Ī�� ��츸 ���� ������ // 15
        // mapGridNum_y = (int)((edgeSpot_y * (-1) * 2) / GapSize_y) + 1;    // : positive  // ��Ī�� ��츸 ���� ������ // 15

        mapGridNum_x = 15;
        mapGridNum_y = 15;
        
        clearBoardAndAddItemRandomly();
    }





    void clearBoardAndAddItemRandomly() //this will reset colorboard by 0, and designate items randomly
    {
        
        for (int i = 0; i < mapGridNum_y + 8; i++)
        {
            for (int j = 0; j < mapGridNum_x + 8; j++)
            {
                ColorBoard[i, j] = 0; 


            }
        }
    }
    // void itemSpawn()
    // {


    //     for (int i = 0; i < mapGridNum_y + 8; i++)
    //     {
           
    //         for (int j = 0; j < mapGridNum_x + 8; j++)
    //         {
    //             Debug.Log("not inside");
    //             if (itemBoard[i, j] == 3)
    //             {
    //                 Debug.Log("working?");
    //                 Instantiate(leaf, new Vector3((j - 4) * GapSize_x, (i - 4) * GapSize_y, -0.01f), Quaternion.identity);
    //             }
    //             if (itemBoard[i, j] == 4)
    //             {
    //                 Debug.Log("working?");
    //                 Instantiate(dotori, new Vector3((j - 4) * GapSize_x, (i - 4) * GapSize_y, -0.01f), Quaternion.identity);
    //             }
    //         }
    //     }
    // }

    


    void Update()
    {
        Black = GameObject.FindGameObjectsWithTag("b_zizi");
        White = GameObject.FindGameObjectsWithTag("w_zizi");
        firstPlayerStoneStatus.text = "Player1 Stone Counting : " + Black.Length.ToString();
        secondPlayerStoneStatus.text = "Player2 Stone Counting : " + White.Length.ToString();

        Debug.Log($"{Black.Length}, {White.Length}");

        // ��ǥ�� ������ �Ǵ� ������Ʈ�� ��ġ �������� : in unity
        // edgePoint 1, 2, 3 : Game > Map Prefab

        edgeSpot_1 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(0).gameObject;
        edgeSpot_2 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(1).gameObject;
        edgeSpot_3 = GameObject.Find("Game").transform.GetChild(0).transform.GetChild(2).gameObject;

        edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().position.x;  // : by EventPosition
        edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().position.y;  // : by EventPosition

        GapSize_x = edgeSpot_2.GetComponent<RectTransform>().position.x - edgeSpot_x;
        GapSize_y = edgeSpot_3.GetComponent<RectTransform>().position.y - edgeSpot_y;

        Timer();


        // edgeSpot_1.GetComponent<RectTransform>().localPosition.x
        // edgeSpot_1.GetComponent<RectTransform>().localPosition.y
    }


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


    // �ʱ� �۾�            
        xGapNum = (int)((xPos - edgeSpot_x) / GapSize_x);  // int, Grid Index
        yGapNum = (int)((yPos - edgeSpot_y) / GapSize_y);  // int, Grid Index


    // ���� �۾� (inMap)
        xGapNum += Correction(xNamuji, GapSize_x); // xGapNum ���� : 0 ~ 14  // 0 : ���� �Ʒ� 
        yGapNum += Correction(yNamuji, GapSize_y); // yGapNum ���� : 0 ~ 14 

        /* Correction() : 
            ������ �������� �׸�����
            40% ���� : 0 return
            60% �̻� : 1 return,  GapNum += 1

            // inMap : Instantiate ����, #1    
            40% ~ 60% : inMap = false �� �ٲ۴� : Instantiate �ȵǵ��� �Ѵ�
        */            
            
            // inMap : Instantiate ����, #2
            // GapNum�� 0 �̸� 15 �̻��� ��� : ��, �� �ٱ����� �Ѿ�� �� ��� ���� �������� ���ϵ��� ����

            // mapGridNum = 15 
                if (xGapNum < 0 || xGapNum >= mapGridNum_x){ inMap = false; } // xGapNum ���� : 0 ~ 14,  xGapNum ������ �� ��      
                if (yGapNum < 0 || yGapNum >= mapGridNum_y){ inMap = false; } // yGapNum ���� : 0 ~ 14,  yGapNum ������

            // ��輱 �ٱ��� Ŭ������ ��� ���� (�������� ������ ���)
                if ((xNamuji < 0 && xGapNum == 0) && (yGapNum > 0 && yGapNum < mapGridNum_y)){ inMap = true; }
                if ((yNamuji < 0 && yGapNum == 0) && (xGapNum > 0 && xGapNum < mapGridNum_x)){ inMap = true; }


    // Set zizi Position
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
            inMap = false;              // ���� �������� ����
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
            inMap = false;              // ���� �������� ����
            return 0;
        }
    }


    public GameObject Game;                     // Unity : Inspector

    public bool stoneWinner = false;
    public GameObject Player1Win;                     // Unity : Inspector
    public GameObject Player2Win;                     // Unity : Inspector
    public int StoneCount = 0;

   
    public void Set_And_RecordPosition()
    {

    // ���� ������ ���� ��ǥ�� ���� ���� ���, �� ����
        assignedList.Add(new List<int> {xGapNum, yGapNum});  
            
        GameObject instance = Instantiate(Stone, new Vector3(x_correction, y_correction, -0.01f), Quaternion.identity) as GameObject;
        instance.tag = isBlack? "b_zizi" : "w_zizi"; 
        instance.transform.SetParent(Game.transform, false);

    // �� ��ǥ�� �� ���� ����
        ColorBoard[yGapNum + 4, xGapNum + 4] = isBlack ? 1 : 2;  // 1 : Black, 2 : White

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
    // ���� ������ ���� ���� ������ ������
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
        } //made by Sohui
    }


    public void AddListAndSpawn()
    {
    // zizi �ߺ� �Ǻ�
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
                Debug.Log(ColorBoard[yGapNum + 4, xGapNum + 4]);
            }
            else
            {
                Debug.Log("????????");
            }
        }
    }

    public bool winCondition(int indexY, int indexX, bool StoneColor) // 5���� �Ǹ� �̱�� ������ �Ǵ�
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
                        line += ColorBoard[i, j] + " ";
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
            if (ColorBoard[startPointY + i, StartPointX] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_X_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ColorBoard[startPointY, StartPointX + i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Plus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ColorBoard[startPointY + i, StartPointX + i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public bool Check_XY_Minus(int startPointY, int StartPointX, int color)
    {
        StoneCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (ColorBoard[startPointY + i, StartPointX - i] == color){ StoneCount += 1; }
            else { StoneCount = 0; return false; }
        }
        return true;
    }

    public void Timer()
    {
        second -= Time.deltaTime;
        gameText.text = "타이머 : " + second.ToString("F1");
        if (second <= 0)
        {
            changePlayer();
            
        }
    }


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


    public void OnClickReset()
    {
        assignedList.Clear();
        clearBoardAndAddItemRandomly();
        for(int j = 0; j < Black.Length; j++)
        {
            Destroy(Black[j]);
        }
        for(int j = 0; j < White.Length; j++)
        {
            Destroy(White[j]);
        }
        isBlack = true;
        playerTurnIcon.transform.position = GameplayUI.transform.position + new Vector3(545f, 55.1f,-0.02f);
        Time.timeScale = 1f;
        second = 5f;
        Player1Win.SetActive(false);
        Player2Win.SetActive(false);
        
        GameResultBox.transform.position = AssignedMapPosition.GetComponent<GameReadyHub>().MapPalette.transform.position + new Vector3(-1230f, 2000f, 0f);


        // for(int k = 0; k < Black_line.Length; k++)
        // {
        //     Destroy(Black_line[k]);
        // }

        // for(int l = 0; l < White_line.Length; l++)
        // {
        //     Destroy(White_line[l]);
        // }
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
}
