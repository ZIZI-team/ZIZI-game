using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class GameSceneSystem : MonoBehaviour
{

// +++ Stone +++ //

    GameObject Stone; 
    public GameObject Stone_b, Stone_w;     // Unity : Inspector
    
    public GameObject[] Black;
    public GameObject[] White;

    [SerializeField] private bool isBlack = true;
 
    public GameObject[] now;
    public string stone_name;

    // public Text b_num, w_num;


// +++ Win Condition +++ //

    public int blackStoneCount = 0;
    public int whiteStoneCount = 0;
    int stoneWinner;

    // public GameObject Line;
    // GameObject[] Black_line;
    // GameObject[] White_line;


// +++ Position +++ //

    // float GapSize;

    float GapSize_x;
    float GapSize_y;


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

    float edgeSpot_x;
    float edgeSpot_y;

    bool inMap = true; // is Mouse position is in map


// +++ Map List +++ //

    [SerializeField] public int[,] ColorBoard = new int[15+8, 15+8]; // 기록
    public int mapGridNum_x;
    public int mapGridNum_y;
    
    public List<List<int>> assignedList = new List<List<int>>();    // 중복 판별


    void Start()
    {
        // 좌표의 기준이 되는 오브젝트의 위치 가져오기 : in unity
        // edgeSpot_1 = GameObject.Find("ZIZIPoint");
        // edgeSpot_x = edgeSpot_1.transform.position.x;  // : negative
        // edgeSpot_y = edgeSpot_1.transform.position.y;  // : negative

        // edgeSpot_x = Screen.width  / 2 + edgeSpot_1.GetComponent<RectTransform>().position.x;  // : by EventPosition
        // edgeSpot_y = Screen.height / 2 + edgeSpot_1.GetComponent<RectTransform>().position.y;  // : by EventPosition

        edgeSpot_x = edgeSpot_1.GetComponent<RectTransform>().position.x;  // : by EventPosition
        edgeSpot_y = edgeSpot_1.GetComponent<RectTransform>().position.y;  // : by EventPosition

        // GapSize = edgeSpot_2.GetComponent<RectTransform>().position.y - edgeSpot_y;

        GapSize_x = edgeSpot_2.GetComponent<RectTransform>().position.x - edgeSpot_x;
        GapSize_y = edgeSpot_3.GetComponent<RectTransform>().position.y - edgeSpot_y;


        // edgeSpot_1.GetComponent<RectTransform>().localPosition.x
        // edgeSpot_1.GetComponent<RectTransform>().localPosition.y

/*

필요한 것 : zizi point position -- > Position 변경

이미지 : 0, 0 가운데
mouse : EventPosition : 오른쪽 아래 0, 0
Panel Onclick


조건 : Map Image, Map Grid ** : Image


*/




        // mapGridNum_x = (int)((edgeSpot_x * (-1) * 2) / GapSize_x) + 1;    // : positive  // 대칭일 경우만 식이 성립함 // 15
        // mapGridNum_y = (int)((edgeSpot_y * (-1) * 2) / GapSize_y) + 1;    // : positive  // 대칭일 경우만 식이 성립함 // 15

        mapGridNum_x = 15;
        mapGridNum_y = 15;

        for (int i = 0; i < mapGridNum_y + 8; i++)
        {
            for (int j = 0; j < mapGridNum_x + 8; j++)
            {
                ColorBoard[i, j] = 0;
            }
        }
    }


    void Update()
    {
        Black = GameObject.FindGameObjectsWithTag("b_zizi");
        White = GameObject.FindGameObjectsWithTag("w_zizi");
        if (stoneWinner == 1)
        {
            Debug.Log("YongJoo is the winner");
            GameOver();
        }
        else if (stoneWinner == 2)
        {
            Debug.Log("Sohui cheated!!!!!!!!!!!!!!");
            GameOver();
        }
        else
        {
            
        }
    }


    public void PanelOnclick()
    {

        Debug.Log("ScreenW : " + Screen.width / 2 );
        Debug.Log("ScreenH : " + Screen.height / 2 );

        // Debug.Log("edge x : " + edgeSpot.GetComponent<RectTransform>().localPosition.x);
        // Debug.Log("edge y : " + edgeSpot.GetComponent<RectTransform>().localPosition.y);

        Debug.Log("edgeSpot_x : " + edgeSpot_x);
        Debug.Log("edgeSpot_y : " + edgeSpot_y);

        Debug.Log("click!!!");

        inMap = true;
        Debug.Log(inMap);

    // Get Mouse Position
        // InputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InputPos = Input.mousePosition;     // EventSystem : Position


        xPos = InputPos.x;
        yPos = InputPos.y;
        
        Debug.Log("xPos : " + xPos);
        Debug.Log("yPos : " + yPos);

        xNamuji = (xPos - edgeSpot_x) % GapSize_x; // xNamuji = 0.0f ~ < GapSize
        yNamuji = (yPos - edgeSpot_y) % GapSize_y; // yNamuji = 0.0f ~ < GapSize

    // 초기 작업            
        xGapNum = (int)((xPos - edgeSpot_x) / GapSize_x);  // int, Grid Index
        yGapNum = (int)((yPos - edgeSpot_y) / GapSize_y);  // int, Grid Index



        Debug.Log("xNamuji : " + xNamuji);
        Debug.Log("yNamuji : " + yNamuji);

        Debug.Log("xGapNum : " + xGapNum);
        Debug.Log("yGapNum : " + yGapNum);




    // 보정 작업 (inMap)
        xGapNum += Correction(xNamuji, GapSize_x); // xGapNum 정상 : 0 ~ 14  // 0 : 왼쪽 아래 
        yGapNum += Correction(yNamuji, GapSize_y); // yGapNum 정상 : 0 ~ 14 

        /* Correction() : 
            측정된 나머지가 그리드의
            40% 이하 : 0 return
            60% 이상 : 1 return,  GapNum += 1

            // inMap : Instantiate 관리, #1    
            40% ~ 60% : inMap = false 로 바꾼다 : Instantiate 안되도록 한다
        */            
            
            // inMap : Instantiate 관리, #2
            // GapNum이 0 미만 15 이상인 경우 : 즉, 맵 바깥으로 넘어가게 될 경우 말을 생성하지 못하도록 만듦

            // mapGridNum = 15 
                if (xGapNum < 0 || xGapNum >= mapGridNum_x){ inMap = false; } // xGapNum 정상 : 0 ~ 14,  xGapNum 비정상 일 때      
                if (yGapNum < 0 || yGapNum >= mapGridNum_y){ inMap = false; } // yGapNum 정상 : 0 ~ 14,  yGapNum 비정상

            // 경계선 바깥을 클릭했을 경우 보정 (나머지가 음수일 경우)
                if ((xNamuji < 0 && xGapNum == 0) && (yGapNum > 0 && yGapNum < mapGridNum_y)){ inMap = true; }
                if ((yNamuji < 0 && yGapNum == 0) && (xGapNum > 0 && xGapNum < mapGridNum_x)){ inMap = true; }


    // Set zizi Position
        x_correction = xGapNum * GapSize_x + edgeSpot_x - Screen.width/2;  // Grid Position for zizi 
        y_correction = yGapNum * GapSize_y + edgeSpot_y - Screen.height/2;  // Grid Position for zizi 


        Debug.Log("xPos : " + xPos);
        Debug.Log("yPos : " + yPos);

        Debug.Log("xNamuji : " + xNamuji);
        Debug.Log("yNamuji : " + yNamuji);

        Debug.Log("xGapNum : " + xGapNum);
        Debug.Log("yGapNum : " + yGapNum);

        Debug.Log("x_correction : " + x_correction);
        Debug.Log("y_correction : " + y_correction);

        Debug.Log(inMap);


    // Set Stone Condition
        Stone = isBlack ? Stone_b : Stone_w;
        stone_name = isBlack ? "b_zizi_pure" : "zizi_pure";
        now = isBlack ? Black : White;
        Stone.name = $"{stone_name}{now.Length}";

    // Instantiate
        if (inMap == true){ AddListAndSpawn(); }        

    // Win Condition
        stoneWinner = winCondition(ColorBoard);
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
            inMap = false;              // 말이 생성되지 않음
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
            inMap = false;              // 말이 생성되지 않음
            return 0;
        }
    }


    public GameObject Game;                     // Unity : Inspector

    public void Set_And_RecordPosition()
    {

    // 현재 놓여진 말의 좌표에 대한 정보 기록, 말 생성
        assignedList.Add(new List<int> {xGapNum, yGapNum});  
        Debug.Log(assignedList);
            
        GameObject instance = Instantiate(Stone, new Vector3(x_correction, y_correction, -0.2f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(Game.transform, false);

    // 각 좌표의 말 색상 정보
        ColorBoard[yGapNum + 4, xGapNum + 4] = isBlack ? 1 : 2;  // 1 : Black, 2 : White

        Debug.Log("Board add : " + ColorBoard[yGapNum + 4, xGapNum + 4]);
        Debug.Log(ColorBoard);
    }


    public void changePlayer()
    {
    // 말을 놓으면 다음 말의 색상을 변경함
        if (isBlack) { isBlack = false; } //made by Sohui
        else { isBlack = true; } //made by Sohui
    }


    public void AddListAndSpawn()
    {
    // zizi 중복 판별
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

    
    public int winCondition(int[ , ] _winStatus)
    {
        for (int i = 0; i < _winStatus.GetLength(0); i++)
        {
            for (int j = 0; j < _winStatus.GetLength(1); j++)
            {
                if (_winStatus[i, j] == 1)
                {
                    blackStoneCount += 1;
                    if (blackStoneCount == 5)
                    {
                        return 1;
                    }
                }
                else
                {
                    blackStoneCount = 0;
                }
                if(_winStatus[i, j] == 2)
                {
                    whiteStoneCount += 1;
                    if (blackStoneCount == 5)
                    {
                        return 2;
                    }
                }
                else
                {
                    whiteStoneCount = 0;
                    
                }
            }
        }
        for (int i = 0; i < _winStatus.GetLength(0); i++)
        {
            for (int j = 0; j < _winStatus.GetLength(1); j++)
            {
                if (_winStatus[j, i] == 1)
                {
                    blackStoneCount += 1;
                    if (blackStoneCount == 5)
                    {
                        return 1;
                    }
                }
                else
                {
                    blackStoneCount = 0;
                }
                if(_winStatus[i, j] == 2)
                {
                    whiteStoneCount += 1;
                    if (blackStoneCount == 5)
                    {
                        return 2;
                    }
                }
                else
                {
                    whiteStoneCount = 0;
                }
            }
        }
        for (int i = 0; i < _winStatus.GetLength(0); i++)
        {
            for (int j = 0; j < _winStatus.GetLength(1); j++)
            {
                if (i + 4 < _winStatus.GetLength(0) && j + 4 < _winStatus.GetLength(1) 
                && _winStatus[i, j] == 1
                && _winStatus[i + 1, j + 1] == 1
                && _winStatus[i + 2, j + 2] == 1
                && _winStatus[i + 3, j + 3] == 1
                && _winStatus[i + 4, j + 4] == 1)
                {
                    return 1;
                }
                if (i + 4 < _winStatus.GetLength(0) && j + 4 < _winStatus.GetLength(1)
                && _winStatus[i, j] == 2
                && _winStatus[i + 1, j + 1] == 2
                && _winStatus[i + 2, j + 2] == 2
                && _winStatus[i + 3, j + 3] == 2
                && _winStatus[i + 4, j + 4] == 2)
                {
                    return 2;
                }
                if (i + 4 < _winStatus.GetLength(0) && j + 4 < _winStatus.GetLength(1)
                && _winStatus[i, j] == 1
                && _winStatus[i + 1, j - 1] == 1
                && _winStatus[i + 2, j - 2] == 1
                && _winStatus[i + 3, j - 3] == 1
                && _winStatus[i + 4, j - 4] == 1)
                {
                    return 1;
                }
                if (i + 4 < _winStatus.GetLength(0) && j + 4 < _winStatus.GetLength(1)
                && _winStatus[i, j] == 2
                && _winStatus[i + 1, j - 1] == 2
                && _winStatus[i + 2, j - 2] == 2
                && _winStatus[i + 3, j - 3] == 2
                && _winStatus[i + 4, j - 4] == 2)
                {
                    return 2;
                }

            }
        }
        return 0;

        
    }
    public void GameOver()
    {
        
    }


    public void Reset()
    {
        for(int i = 1; i < Black.Length; i++)
        {
            Destroy(Black[i]);
        }

        for(int j = 1; j < White.Length; j++)
        {
            Destroy(White[j]);
        }

        // for(int k = 0; k < Black_line.Length; k++)
        // {
        //     Destroy(Black_line[k]);
        // }

        // for(int l = 0; l < White_line.Length; l++)
        // {
        //     Destroy(White_line[l]);
        // }
    }
}
