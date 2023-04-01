using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Start From Onclick bush PointA

public class Tutorial : MonoBehaviour
{

    void Start()
    {
        MakeTutorialList();
        MakeButtonList();
    }


// +-------------------------------------------------------------------------------+//

// Make Tutorial List

    public List<GameObject> TutorialList = new List<GameObject>();
    int ListIndex = 0;
    public GameObject Main; // inspector

    public void MakeTutorialList()
    {
        Main = GameObject.Find("Main");

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(7).gameObject); // ZIZI

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject); // T1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject); // T1-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject); // T1-2

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject); // T2
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).gameObject); // T2-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).gameObject); // T2-2
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).gameObject); // T2-3
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).gameObject); // T2-4
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(5).gameObject); // T2-5

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).gameObject); // T3
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).gameObject); // T3-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(2).gameObject); // T3-2
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(3).gameObject); // T3-3
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).gameObject); // T3-4
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject); // T3-5

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).gameObject); // T4
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).gameObject); // T4-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).gameObject); // T4-2
    }

// Make Button List
    public List<Button> ButtonList = new List<Button>();

    public void MakeButtonList()
    {
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T1)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T2)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T3)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T4)

        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Button>());      // bush PointA
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Button>());      // dotori
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(6).transform.GetChild(1).gameObject.GetComponent<Button>());                            // my dotori

    }

    
// +-------------------------------------------------------------------------------+//

    public void NextT() // Go to next Tutorial List
    {
        if (ListIndex == TutorialList.Count - 1)
        {
            SkipTutorial();
            return;
        }
        TutorialList[ListIndex++].SetActive(false);
        TutorialList[ListIndex].SetActive(true);
    }

    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("TutorialPlayed", 1);
        SceneManager.LoadScene("TitleScene");
    }


// +-------------------------------------------------------------------------------+//
// 1. Text : 5개 지지를 줄세우자!
// Onclick : T1 / Next Button (T1)
    bool T1Flag = false;
    public void T1()
    {
        if (T1Flag == true){ return; }

        // (1)
        NextT();

        // (2) Buton interactive false : Next Button (T1)
        ButtonList[0].interactable = false;
        
        // (3) Buton interactive true : bush PointA
        ButtonList[4].interactable = true;
        
        T1Flag = true;
        T1_2Flag = false;
    }

// 1-1. Text : 빈 땅 위에 지지를 놓아보자
// Onclick : BushMap / BackGround / Bush / bush PointA
    public void T1_1()
    {
        // (1) SetActive True My ZIZI

        // (2)
        GameObject.Find("bush PointA").SetActive(false);

        // (3)
        GameObject.Find("bush PointB_1").SetActive(false);
        GameObject.Find("bush PointB_2").SetActive(false);
        GameObject.Find("bush PointB_3").SetActive(false);
        GameObject.Find("bush PointB_4").SetActive(false);

        // (4)
        NextT();

        // (5) Buton interactive false : bush PointA
        ButtonList[4].interactable = false;

        // (6) Buton interactive true : Next Button (T1)
        ButtonList[0].interactable = true;
    }

// 1-2. Text : (부시가 사라지며) 부시 근처에 지지를 놓으면 근처의 부시를 없앨 수 있다
// Onclick : T1 / Next Button (T1)
    bool T1_2Flag = true;
    public void T1_2()
    {
        if (T1_2Flag == true){ return; }

        // (1)
        NextT();

        // (2) Buton interactive false : Next Button (T1)       
        ButtonList[0].interactable = false;

        // (3) Buton interactive true : dotori        
        ButtonList[5].interactable = true;

        T1_2Flag = true;
    }

// +-------------------------------------------------------------------------------+//
// 2. Text : 도토리가 보이네? 도토리를 주워보자
// Onclick : BushMap / BackGround / Item / dotori
    public void T2()
    {
        // (1) SetActive True My ZIZI

        // (2)
        GameObject.Find("Item/doroti").SetActive(false);

        // (3) SetActive true : My Item dotori
        GameObject.Find("ItemBox/doroti").SetActive(true);

        // (4)
        GameObject.Find("bush PointC_1").SetActive(false);
        GameObject.Find("bush PointC_2").SetActive(false);
        GameObject.Find("bush PointC_3").SetActive(false);

        // (5)
        NextT();

        // (6) Buton interactive false : dotori
        ButtonList[5].interactable = false;

        // (7) Buton interactive true : Next Button (T2)
        ButtonList[1].interactable = true;

        T2_1Flag = false;
    }

// 2-1. Text : 도토리를 얻었다! 
// Onclick : T2 / Next Button (T2)
    bool T2_1Flag = true;
    public void T2_1()
    {
        if (T2_1Flag == true){ return; }

        NextT();

        T2_1Flag = true;
        T2_2Flag = false;
    }

// 2-2. Text : 도토리로 상대를 공격할수있다
// Onclick : T2 / Next Button (T2)
    bool T2_2Flag = true;
    public void T2_2()
    {
        if (T2_2Flag == true){ return; }

        // (1) SetActive true : juk

        // (2)
        NextT();

        // (3) Buton interactive false : Next Button (T2)
        ButtonList[1].interactable = false;

        // (4) Buton interactive true : my dotori
        ButtonList[6].interactable = true;

        T2_2Flag = true;
    }

// 2-3. Text : 상대편이 있다, 얻은 도토리를 집어볼까?
// Onclick : My Item dotori
    public void T2_3()
    {
        // (1) SetActive false : My Item dotori
        GameObject.Find("ItemBox/doroti").SetActive(false);

        // (2) Animation : Spot My ZIZI

        // (3)
        NextT();

        // (4) Buton interactive false : my dotori
        ButtonList[6].interactable = true;

        // (5) Buton interactive true : My ZIZI
    }

// 2-4. Text : 내 지지 위에 도토리를 놓아보자!
// Onclick : My ZIZI
    public void T2_4()
    {
        // (1) SetActive false : juk 
        // (2) SetActive false : bush

        // (3)
        NextT();

        // (4) Buton interactive false : My ZIZI
        // (5) Buton interactive true : Next Button (T2)
        ButtonList[1].interactable = true;

        T2_5Flag = false;
   }

// 2-5. Text : 잘했어! 공격을 통해 상대편이 5개의 지지를 모으는 것을 막을 수 있다
// Onclick : T2 / Next Button (T2)
    bool T2_5Flag = true;
    public void T2_5()
    {
        if (T2_5Flag == true){ return; }

        // (1)
        NextT();

        // (2) Buton interactive false : Next Button (T2)
        ButtonList[1].interactable = false;

        // (3) Buton interactive true : leaf

        T2_5Flag = true;
    }

// +-------------------------------------------------------------------------------+//
// 3. Text : 나뭇잎이 보이네? 나뭇잎을 주워보자
// Onclick : BushMap / BackGround / Item / leaf
    public void T3()
    {
        // (1) SetActive True My ZIZI

        // (2)
        GameObject.Find("leaf").SetActive(false);

        // (3) SetActive true : My Item leaf

        // (4)
        GameObject.Find("bush PointD_1").SetActive(false);
        GameObject.Find("bush PointD_2").SetActive(false);
        
        // (5)
        NextT();
 
        // (6) Buton interactive false : leaf

        // (7) Buton interactive true : Next Button (T3)
        ButtonList[2].interactable = true;

    }

// 3-1. Text : 나뭇잎을 얻었다! 
// Onclick : T3 / Next Button (T3)
    public void T3_1()
    {
        NextT();
    }

// 3-2. Text : 나뭇잎으로 상대의 공격을 방어할 수 있다
// Onclick : T3 / Next Button (T3)
    public void T3_2()
    {
        // (1) SetActive true : juk
        
        // (2)
        NextT();

        // (3) Buton interactive false : Next Button (T3)
        ButtonList[2].interactable = false;

        // (4) Buton interactive true : My Item leaf

    }

// 3-3. Text : 주위의 적이 보인다! 나뭇잎을 집어볼까?
// Onclick : My Item leaf
    public void T3_3()
    {
        // (1) SetActive false : My Item leaf 
        // (2) Animation : Spot My ZIZI

        // (3)
        NextT();

        // (4) Buton interactive false : My Item leaf

        // (5) Buton interactive true : My ZIZI

    }

// 3-4. Text : 내 지지 위에 나뭇잎을 놓아보자!
// Onclick : My ZIZI
    public void T3_4()
    {
        // (1) SetActive false : bush

        // (2) Animation : Spot JUK
        // (3) SetActive false : juk 

        // (4)
        NextT();

        // (5) Buton interactive false : My ZIZI

        // (6) Buton interactive true : Next Button (T3)
        ButtonList[2].interactable = true;
    }

// 3-5. Text : 잘했어! 나뭇잎을 붙인 지지는 무적이다
// Onclick : T3 / Next Button (T3)
    public void T3_5()
    {
        // (1) Animation : DANCE ZIZI

        // (2)
        NextT();
    }

// +-------------------------------------------------------------------------------+//
// 4. 아이템은 최대 3개씩 모을 수 있고, 5턴 이후부터 사용 가능하다
// Onclick : T4 / Next Button (T4)
    public void T4()
    {
        // (1)
        NextT();

        // (2) SetActive true : DANCE ZIZI

        // (3) Animation : Timer

    }

// 4-1. 제한시간이 존재하니 조심하도록 하자 시간이 지나면 턴이 넘어간다
// Onclick : T4 / Next Button (T4)
    public void T4_1()
    {
        NextT();
    }

// 4-2. 게임 시작하기
// Onclick : T4 / Start Game
    public void T4_2()
    {
        // (1)
        NextT();

        // (2) Buton interactive false : Next Button (T4)
        ButtonList[3].interactable = false;

            // (3) Buton interactive true : Start

    }
}