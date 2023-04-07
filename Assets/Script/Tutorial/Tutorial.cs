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
        MakeObjectList();
    }


// +-------------------------------------------------------------------------------+//

// Make Tutorial List

    public List<GameObject> TutorialList = new List<GameObject>();
    int ListIndex = 0;
    public GameObject Main; // inspector

    public void MakeTutorialList()
    {
        Main = GameObject.Find("Main");

        // TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(8).gameObject); // ZIZI

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject); // T1 [ 1 ]
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject); // T1-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject); // T1-2

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject); // T2 [ 4 ]
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).gameObject); // T2-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).gameObject); // T2-2
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).gameObject); // T2-3
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).gameObject); // T2-4
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(5).gameObject); // T2-5

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).gameObject); // T3 [ 10 ]
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).gameObject); // T3-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(2).gameObject); // T3-2
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(3).gameObject); // T3-3
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).gameObject); // T3-4
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject); // T3-5

        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).gameObject); // T4 [ 16 ]
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).gameObject); // T4-1
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).gameObject); // T4-2
    }

// Make Button List

    public List<Button> ButtonList = new List<Button>();

    public void MakeButtonList()
    {
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T1)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(6).gameObject.GetComponent<Button>());   // Next Buton (T2)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(6).gameObject.GetComponent<Button>());   // Next Buton (T3)
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).gameObject.GetComponent<Button>());   // Next Buton (T4)

        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Button>());      // bush PointA

        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Button>());      // dotori
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(6).transform.GetChild(1).gameObject.GetComponent<Button>());                            // my dotori

        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Button>());      // leaf
        ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(6).transform.GetChild(5).gameObject.GetComponent<Button>());                            // my leaf
    }

// Make Object List

    public List<GameObject> ObjectList = new List<GameObject>();

    public void MakeObjectList()
    {
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(0).gameObject); // My ZIZI 1 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(1).gameObject); // My ZIZI 2
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(2).gameObject); // My ZIZI 3 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(3).gameObject); // BIBI 1 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(4).gameObject); // BIBI 2 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(5).gameObject); // BIBI 3

        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(6).transform.GetChild(1).gameObject); // My dotori
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(6).transform.GetChild(5).gameObject); // My leaf

        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(7).transform.GetChild(6).gameObject); //leaf
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

// 1. Text :  (next button) �ؽ�Ʈ�� ��ư ���ֱ�, ���̵�� ����

// 1-1. Text : �� ���� ���� ��ġ�Ͽ� ���� ��������. (���� �ִϸ��̼�)) 
// 1-2. Text : ���� �ֺ��� ���� ������ ������ ������ϴ�. (next button)


// 2. Text : ���丮�� �߰��߾��! ���� ���� ���丮�� �ֿ�������.
// 2-1. Text : ���丮�� ������! (next button)
// 2-2. Text : ���丮�� ��� ���� �����ϴ� �������Դϴ�. (next button)

// 2-2-1. Text : �ڽ��� �� �� �ϳ��� �����ϸ� �ֺ��� ��� ���� ���� �� �ֽ��ϴ�. (next button)
// 2-3. // Text : ��� ȹ���� ���丮 �������� ����غ����? (next button) (������� ����)

// 2-3. Text : ���丮�� �������. (���丮 ������ ���� �ִϸ��̼�)
// 2-4. Text : �� ���� ���丮�� ���ƺ�����. (���� ���� �ִϸ��̼�)
// 2-5. Text : ���߾��! (Tip) ���丮�� ��븦 ������ �� �ֽ��ϴ�. (next button)


// 3. Text : �̹����� �������� �߰��߾��! ���� ���� �������� �ֿ�������.
// 3-1. Text : �������� ������! (next button)
// 3-2. Text : �������� ����� ���丮 ������ ����ϴ� �������Դϴ�. (next button)

// 3-3. // Text :  ��� ȹ���� ������ �������� ����غ����? (next button) (������� ����)

// 3-3. Text : �������� �������. (������ ������ ���� �ִϸ��̼�)
// 3-4. Text : �� ���� �������� ���ƺ�����. (���� ���� �ִϸ��̼�) (��� ���� �ִϸ��̼�)
// 3-5. Text : ���߾��! (Tip) �������� ���� ������ ����� ���丮 ������ ���� �ʽ��ϴ�. (next button)

// 4. �������� �ִ� 3���� ���� �� �ְ�, 5�� ���ĺ��� ��� �����մϴ�. (next button) (UI ���̶���Ʈ)
// 4-1. �� �Ͽ��� ���ѽð��� �ֽ��ϴ�. �ð��� ������ ���� �Ѿ�� �����ϼ���! (next button) 
// 4-2. 5���� ���� �Ϸķ� ������ ���ӿ��� �¸��մϴ�. (���� ���� ��ư ����)


// ��ŵ��ư : Ʃ�丮�� ��ŵ�ϱ�

// +-------------------------------------------------------------------------------+//
// 1. Text :  (next button) �ؽ�Ʈ�� ��ư ���ֱ�, ���̵�� ����
// Onclick : T1 / Next Button (T1)
    bool T1Flag = false;
    public void T1()
    {
        if (T1Flag == true){ return; }

        // (1)
        NextT(); // 1-1. Text

        // (2) Buton interactive false : Next Button (T1)
        ButtonList[0].interactable = false;
        
        // (3) Buton interactive true : bush PointA
        ButtonList[4].interactable = true;
        
        T1Flag = true;   
    }

// 1-1. Text : �� ���� ���� ��ġ�Ͽ� ���� ��������. (���� �ִϸ��̼�)) 
// Onclick : BushMap / BackGround / Bush / bush PointA
    public void T1_1()
    {
        // (1) SetActive True My ZIZI 1
        ObjectList[0].SetActive(true);

        // (2)
        GameObject.Find("bush PointA").SetActive(false);

        // (3)
        GameObject.Find("bush PointB_1").SetActive(false);
        GameObject.Find("bush PointB_2").SetActive(false);
        GameObject.Find("bush PointB_3").SetActive(false);
        GameObject.Find("bush PointB_4").SetActive(false);

        // (4)
        NextT(); // 1-2. Text

        // (5) Buton interactive false : bush PointA
        ButtonList[4].interactable = false;

        // (6) Buton interactive true : Next Button (T1)
        ButtonList[0].interactable = true;
        T1_2Flag = false;
    }

// 1-2. Text : ���� �ֺ��� ���� ������ ������ ������ϴ�. (next button)
// Onclick : T1 / Next Button (T1)
    bool T1_2Flag = true;
    public void T1_2()
    {
        if (T1_2Flag == true){ return; }

        // (1)
        NextT(); // 2. Text
        Main.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);

        // (2) Buton interactive false : Next Button (T1)       
        ButtonList[0].interactable = false;

        // (3) Buton interactive true : dotori        
        ButtonList[5].interactable = true;

        T1_2Flag = true;
    }

// +-------------------------------------------------------------------------------+//
// 2. Text : ���丮�� �߰��߾��! ���� ���� ���丮�� �ֿ�������.
// Onclick : BushMap / BackGround / Item / dotori
    public void T2()
    {
        // (1) SetActive True My ZIZI 2
        ObjectList[1].SetActive(true);

        // (2)
        GameObject.Find("dotori").SetActive(false);

        // (3) SetActive true : My Item dotori
        ObjectList[6].SetActive(true);

        // (4)
        GameObject.Find("bush PointC_1").SetActive(false);
        GameObject.Find("bush PointC_2").SetActive(false);
        GameObject.Find("bush PointC_3").SetActive(false);

        // (5)
        NextT(); // 2-1. Text

        // (6) Buton interactive false : dotori
        ButtonList[5].interactable = false;

        // (7) Buton interactive true : Next Button (T2)
        ButtonList[1].interactable = true;

        T2_1Flag = false;
    }

// 2-1. Text : ���丮�� �����! 
// Onclick : T2 / Next Button (T2)
    bool T2_1Flag = true;
    public void T2_1()
    {
        if (T2_1Flag == true){ return; }

        NextT(); // 2-2. Text

        T2_1Flag = true;
        
        // Buton interactive false : Next Button (T2)
        ButtonList[1].interactable = false;

        T2_2Flag = false;

        // Buton interactive true : Next Button (T2)
        ButtonList[1].interactable = true;
    }
    // �ڷ�ƾ

// 2-2. Text : ���丮�� ��븦 �����Ҽ��ִ�
// Onclick : T2 / Next Button (T2)
    bool T2_2Flag = true;
    public void T2_2()
    {
        if (T2_2Flag == true){ return; }

        // (1) SetActive true : juk
        ObjectList[3].SetActive(true); // BIBI 1
        // ObjectList[4].SetActive(true); // BIBI 2
        ObjectList[5].SetActive(true); // BIBI 3

        // (2)
        NextT(); // 2-3. Text

        // (3) Buton interactive false : Next Button (T2)
        ButtonList[1].interactable = false;

        // (4) Buton interactive true : my dotori
        ButtonList[6].interactable = true;

        T2_2Flag = true;
    }

// 2-3. Text : ������� �ִ�, ���� ���丮�� �����?
// Onclick : My Item dotori
    public void T2_3()
    {
        // (1) SetActive false : My Item dotori
        ObjectList[6].SetActive(false);

        // (2) Animation : Spot My ZIZI 2

        // (3)
        NextT(); // 2-4. Text

        // (4) Buton interactive false : my dotori
        ButtonList[6].interactable = false;

        // (5) Buton interactive true : My ZIZI 2
        ObjectList[1].GetComponent<Button>().interactable = true;
    }

// 2-4. Text : �� ���� ���� ���丮�� ���ƺ���!
// Onclick : My ZIZI 2
    public void T2_4()
    {
        // (1) SetActive false : juk 
        ObjectList[3].SetActive(false); // BIBI 1
        // ObjectList[4].SetActive(false); // BIBI 2
        ObjectList[5].SetActive(false); // BIBI 3

        // (2)
        NextT(); // 2-5. Text

        // (3) Buton interactive false : My ZIZI
        ObjectList[1].GetComponent<Button>().interactable = false;

        // (4) Buton interactive true : Next Button (T2)
        ButtonList[1].interactable = true;

        T2_5Flag = false;
   }

// 2-5. Text : ���߾�! ������ ���� ������� 5���� ������ ������ ���� ���� �� �ִ�
// Onclick : T2 / Next Button (T2)
    bool T2_5Flag = true;
    public void T2_5()
    {
        if (T2_5Flag == true){ return; }

        // (1)
        NextT(); // 3. Text
        Main.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);

        // (2) Buton interactive false : Next Button (T2)
        ButtonList[1].interactable = false;

        // (3) Buton interactive true : leaf
        ButtonList[7].interactable = true;

        T2_5Flag = true;
    }

// +-------------------------------------------------------------------------------+//
// 3. Text : �������� ���̳�? �������� �ֿ�����
// Onclick : BushMap / BackGround / Item / leaf
    public void T3()
    {
        // (1) SetActive True My ZIZI 3
        ObjectList[2].SetActive(true); // ZIZI 3

        // (2)
        GameObject.Find("leaf").SetActive(false);

        // (3) SetActive true : My Item leaf
        ObjectList[7].SetActive(true);

        // (4)
        GameObject.Find("bush PointD_1").SetActive(false);
        GameObject.Find("bush PointD_2").SetActive(false);
        
        // (5)
        NextT(); // 3-1. Text
 
        // (6) Buton interactive false : leaf
        ButtonList[7].interactable = false;

        // (7) Buton interactive true : Next Button (T3)
        ButtonList[2].interactable = true;
        
        T3_1Flag = false;
    }

// 3-1. Text : �������� �����! 
// Onclick : T3 / Next Button (T3)
    bool T3_1Flag = true;
    public void T3_1()
    {
        if (T3_1Flag == true){ return; }

        NextT(); // 3-2. Text

        T3_1Flag = true;

        // Buton interactive false : Next Button (T3)
        ButtonList[1].interactable = false;
        
        T3_2Flag = false;

        // Buton interactive true : Next Button (T3)
        ButtonList[1].interactable = true;
    }

// 3-2. Text : ���������� ����� ������ ����� �� �ִ�
// Onclick : T3 / Next Button (T3)
    bool T3_2Flag = true;
    public void T3_2()
    {
        if (T3_2Flag == true){ return; }

        // (1) SetActive true : juk
        ObjectList[3].SetActive(true); // BIBI 1
        
        // (2)
        NextT(); // 3-3. Text

        // (3) Buton interactive false : Next Button (T3)
        ButtonList[2].interactable = false;

        // (4) Buton interactive true : My Item leaf
        ButtonList[8].interactable = true;

        T3_2Flag = true;
    }

// 3-3. Text : ������ ���� ���δ�! �������� �����?
// Onclick : My Item leaf
    public void T3_3()
    {
        // (1) SetActive false : My Item leaf 
        ObjectList[7].SetActive(false);

        // (2) Animation : Spot My ZIZI 3

        // (3)
        NextT(); // 3-4. Text

        // (4) Buton interactive false : My Item leaf
        ButtonList[8].interactable = false;

        // (5) Buton interactive true : My ZIZI 3
        ObjectList[2].GetComponent<Button>().interactable = true;
    }

// 3-4. Text : �� ���� ���� �������� ���ƺ���!
// Onclick : My ZIZI 3
    public void T3_4()
    {
        // (1) SetActive true : Leaf
        ObjectList[8].SetActive(true);
        
        // (2) Animation : Spot JUK


        // (3) SetActive false : ZIZI 1, 2
        ObjectList[0].SetActive(false);
        ObjectList[1].SetActive(false);

        // (4) Animation : ZIZI 1, 2

        // (5)
        NextT(); // 3-5. Text

        // (6) Buton interactive false : My ZIZI 3
        ObjectList[2].GetComponent<Button>().interactable = true;

        // (7) Buton interactive true : Next Button (T3)
        ButtonList[2].interactable = true;

        T3_5Flag = false;
    }

// 3-5. Text : ���߾�! �������� ���� ������ �����̴�
// Onclick : T3 / Next Button (T3)
    bool T3_5Flag = true;
    public void T3_5()
    {
        if (T3_5Flag == true){ return; }

        // (1) Animation : DANCE ZIZI

        // (2)
        NextT(); // 4.
        Main.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);

        // (3) Buton interactive false : Next Button (T3)
        ButtonList[2].interactable = false;
        
        // (4) Buton interactive true : Next Button (T4)
        ButtonList[3].interactable = true;

        T3_5Flag = true;
        T4Flag = false;
    }

// +-------------------------------------------------------------------------------+//
// 4. �������� �ִ� 3���� ���� �� �ְ�, 5�� ���ĺ��� ��� �����ϴ�
// Onclick : T4 / Next Button (T4)
    bool T4Flag = true;
    public void T4()
    {
        if (T4Flag == true){ return; }

        // (1)
        NextT(); // 4-1.

        // (2) SetActive true : DANCE ZIZI

        // (3) Animation : Timer

        T4Flag = true;

        // Buton interactive false : Next Button (T4)
        ButtonList[3].interactable = false;

        T4_1Flag = false;

        // Buton interactive true : Next Button (T4)
        ButtonList[3].interactable = true;
    }

// 4-1. ���ѽð��� �����ϴ� �����ϵ��� ���� �ð��� ������ ���� �Ѿ��
// Onclick : T4 / Next Button (T4)
    bool T4_1Flag = true;
    public void T4_1()
    {
        if (T4_1Flag == true){ return; }

        NextT(); // 4-2.

        // (1) SetActive false : Skip Button
        Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);

        // (2) SetActive true : Start Button
        Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);

        // (3) SetActive false : Next Button (T4)
        Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(false);

        T4_1Flag = true;
    }

// 4-2. ���� ���� ����

// Onclick : T4 / Start Game

}