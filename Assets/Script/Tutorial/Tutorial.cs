using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Start From Onclick bush PointA

public class Tutorial : MonoBehaviour
{
    public List<GameObject> TutorialList = new List<GameObject>();
    int ListIndex = 0;

    public GameObject Main; // inspector
    void Start()
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

    public void NextT()
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
// 1. Text : 5�� ������ �ټ�����!
// Onclick : T1 / Next Button
    public void T1()
    {
        NextT();

        // Buton interactive false : Next
            // Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        
        // Buton interactive true : bush PointA
            // GameObject.Find("bush PointA").GetComponent<Button>().interactable = true;
    }

// 1-1. Text : �� �� ���� ������ ���ƺ���
// Onclick : BushMap / BackGround / Bush / bush PointA
    public void T1_1()
    {
        // SetActive True My ZIZI

        GameObject.Find("bush PointA").SetActive(false);

        GameObject.Find("bush PointB_1").SetActive(false);
        GameObject.Find("bush PointB_2").SetActive(false);
        GameObject.Find("bush PointB_3").SetActive(false);
        GameObject.Find("bush PointB_4").SetActive(false);

        NextT();
        // Buton interactive false : bush PointA
            // GameObject.Find("bush PointA").GetComponent<Button>().interactable = false;

        // Buton interactive true : Next
            // Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
    }

// 1-2. Text : (�νð� �������) �ν� ��ó�� ������ ������ ��ó�� �νø� ���� �� �ִ�
// Onclick : T1 / Next Button
    public void T1_2()
    {
        NextT();
        // Buton interactive false : Next        
            // Main.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;

        // Buton interactive true : dotori        
            // GameObject.Find("Item/doroti").GetComponent<Button>().interactable = true;
    }

// +-------------------------------------------------------------------------------+//
// 2. Text : ���丮�� ���̳�? ���丮�� �ֿ�����
// Onclick : BushMap / BackGround / Item / dotori
    public void T2()
    {
        // SetActive True My ZIZI

        GameObject.Find("Item/doroti").SetActive(false);

        // SetActive true : My Item dotori
        GameObject.Find("ItemBox/doroti").SetActive(true);

        GameObject.Find("bush PointC_1").SetActive(false);
        GameObject.Find("bush PointC_2").SetActive(false);
        GameObject.Find("bush PointC_3").SetActive(false);

        NextT();
        // Buton interactive false : dotori
            // GameObject.Find("Item/doroti").GetComponent<Button>().interactable = false;

        // Buton interactive true : Next
            // Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(6).gameObject.GetComponent<Button>().interactable = true;
    }

// 2-1. Text : ���丮�� �����! 
// Onclick : T2 / Next Button
    public void T2_1()
    {
        NextT();
    }

// 2-2. Text : ���丮�� ��븦 �����Ҽ��ִ�
// Onclick : T2 / Next Button
    public void T2_2()
    {
        // SetActive true : juk

        NextT();
        // Buton interactive false : Next
            // Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(6).gameObject.GetComponent<Button>().interactable = false;

        // Buton interactive true : my dotori
            // GameObject.Find("ItemBox/doroti").GetComponent<Button>().interactable = true;
    }

// 2-3. Text : ������� �ִ�, ���� ���丮�� �����?
// Onclick : My Item dotori
    public void T2_3()
    {
        // SetActive false : My Item dotori
        GameObject.Find("ItemBox/doroti").SetActive(false);

        // Animation : Spot My ZIZI

        NextT();
        // Buton interactive false : my dotori
            // GameObject.Find("ItemBox/doroti").GetComponent<Button>().interactable = true;

        // Buton interactive true : My ZIZI
    }

// 2-4. Text : �� ���� ���� ���丮�� ���ƺ���!
// Onclick : My ZIZI
    public void T2_4()
    {
        // SetActive false : juk 
        // SetActive false : bush

        NextT();
        // Buton interactive false : My ZIZI
        // Buton interactive true : Next
   }

// 2-5. Text : ���߾�! ������ ���� ������� 5���� ������ ������ ���� ���� �� �ִ�
// Onclick : T2 / Next Button
    public void T2_5()
    {
        NextT();
        // Buton interactive false : Next
        // Buton interactive true : leaf
    }

// +-------------------------------------------------------------------------------+//
// 3. Text : �������� ���̳�? �������� �ֿ�����
// Onclick : BushMap / BackGround / Item / leaf
    public void T3()
    {
        // SetActive True My ZIZI

        GameObject.Find("leaf").SetActive(false);
        // SetActive true : My Item leaf

        GameObject.Find("bush PointD_1").SetActive(false);
        GameObject.Find("bush PointD_2").SetActive(false);

        NextT();
        // Buton interactive false : leaf
        // Buton interactive true : Next
    }

// 3-1. Text : �������� �����! 
// Onclick : T3 / Next Button
    public void T3_1()
    {
        NextT();
    }

// 3-2. Text : ���������� ����� ������ ����� �� �ִ�
// Onclick : T3 / Next Button
    public void T3_2()
    {
        // SetActive true : juk
        
        NextT();
        // Buton interactive false : Next
        // Buton interactive true : My Item leaf
    }

// 3-3. Text : ������ ���� ���δ�! �������� �����?
// Onclick : My Item leaf
    public void T3_3()
    {
        // SetActive false : My Item leaf 
        // Animation : Spot My ZIZI

        NextT();
        // Buton interactive false : My Item leaf
        // Buton interactive true : My ZIZI
    }

// 3-4. Text : �� ���� ���� �������� ���ƺ���!
// Onclick : My ZIZI
    public void T3_4()
    {
        // SetActive false : bush

        // Animation : Spot JUK
        // SetActive false : juk 

        NextT();
        // Buton interactive false : My ZIZI
        // Buton interactive true : Next
    }

// 3-5. Text : ���߾�! �������� ���� ������ �����̴�
// Onclick : T3 / Next Button
    public void T3_5()
    {
        // Animation : DANCE ZIZI
        NextT();
    }

// +-------------------------------------------------------------------------------+//
// 4. �������� �ִ� 3���� ���� �� �ְ�, 5�� ���ĺ��� ��� �����ϴ�
// Onclick : T4 / Next Button
    public void T4()
    {
        NextT();
        // SetActive true : DANCE ZIZI
        // Animation : Timer
    }

// 4-1. ���ѽð��� �����ϴ� �����ϵ��� ���� �ð��� ������ ���� �Ѿ��
// Onclick : T4 / Next Button
    public void T4_1()
    {
        NextT();
    }

// 4-2. ���� �����ϱ�
// Onclick : T4 / Start Game
    public void T4_2()
    {
        NextT();
        // Buton interactive false : Next
        // Buton interactive true : Start
    }
}