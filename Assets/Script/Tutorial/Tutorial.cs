using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Start From Onclick bush PointA

public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro;

    // textMeshPro.text = "Hello, World!";

    // string playerName = "John";
    // int playerScore = 100;
    // textMeshPro.text = $"Player: {playerName}\nScore: {playerScore}";



    public GameObject Main; // inspector
    void Start()
    {
        Main = GameObject.Find("Main");
        MakeButtonList();
        MakeObjectList();

        textMeshPro = Main.transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        // Buton interactive false : Next Button
        ButtonList[0].interactable = false;

        StartCoroutine(DelayCoroutine1());
    }
    IEnumerator DelayCoroutine1()
    {
        yield return new WaitForSeconds(0.8f);
        StartTutorial();
    }


// +-------------------------------------------------------------------------------+//    

// Make Button List

    public List<Button> ButtonList = new List<Button>();

    public void MakeButtonList()
    {
        // Next Buton
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(2).gameObject.GetComponent<Button>());  

        // bush PointA
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Button>());      
    
        // dotori
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Button>());      
    
        // my dotori
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<Button>());                            
    
        // leaf    
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Button>());      
    
        // my leaf
            ButtonList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject.GetComponent<Button>());                            
    }

// Make Object List

    public List<GameObject> ObjectList = new List<GameObject>();

    public void MakeObjectList()
    {
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).gameObject); // My ZIZI 1 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).gameObject); // My ZIZI 2
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).gameObject); // My ZIZI 3 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).gameObject); // BIBI 1 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(4).gameObject); // BIBI 2 
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(5).gameObject); // BIBI 3

        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).gameObject); // My dotori
        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject); // My leaf

        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(3).transform.GetChild(6).gameObject); //leaf on ZIZI

        ObjectList.Add(Main.transform.GetChild(1).transform.GetChild(9).gameObject); // Spot
    }

    
// +-------------------------------------------------------------------------------+//


    public void StartTutorial()
    {
        // Fade SetActive false
        Main.transform.GetChild(1).transform.GetChild(8).gameObject.SetActive(false);

        StartCoroutine(DelayCoroutine2());
    }
    IEnumerator DelayCoroutine2()
    {
        yield return new WaitForSeconds(0.5f);
        T1();
    }

    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("TutorialPlayed", 1);
        SceneManager.LoadScene("TitleScene");
    }


// +-------------------------------------------------------------------------------+//

// 스킵버튼 : 튜토리얼 스킵하기

// +-------------------------------------------------------------------------------+//
    
// 1. 페이드 후 T1 자동으로 실행
    public void T1()
    {
        // Text 1-1. 
        textMeshPro.text = "빈 공간 위를 터치하여\n말을 놓으세요.";

        // Buton interactive true : bush PointA
        ButtonList[1].interactable = true;

        // Animation Play (Spot Bush Point A)
        ObjectList[9].transform.GetChild(0).gameObject.SetActive(true);
    }


// 1-1. Text : 빈 공간 위를 터치하여 말을 놓으세요. (스폿 애니메이션)) 
// Onclick : BushMap / BackGround / Bush / bush PointA
    public void T1_1()
    {
        // Animation Off (Spot Bush Point A)
        ObjectList[9].transform.GetChild(0).gameObject.SetActive(false);

        // SetActive True My ZIZI 1
        ObjectList[0].SetActive(true);

        // Bush Cut
        GameObject.Find("bush PointA").SetActive(false);
        GameObject.Find("bush PointB_1").SetActive(false);
        GameObject.Find("bush PointB_2").SetActive(false);
        GameObject.Find("bush PointB_3").SetActive(false);
        GameObject.Find("bush PointB_4").SetActive(false);

        // Text 1-2.
        textMeshPro.text = "덤불 주변에 말을 놓으면\n덤불이 사라집니다.";

        // Buton interactive false : bush PointA
        ButtonList[1].interactable = false;

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;


            T1_2Flag = false;
    }


// 1-2. Text : 덤불 주변에 말을 놓으면 덤불이 사라집니다. (next button)
// Onclick : Next Button
    bool T1_2Flag = true;
    public void T1_2_Next()
    {
        if (T1_2Flag == true){ return; }

            // Text 2.
            textMeshPro.text = "도토리를 발견했어요!\n말을 놓아 도토리를 주워보세요.";

            // Buton interactive false : Next Button    
            ButtonList[0].interactable = false;

            // Buton interactive true : dotori        
            ButtonList[2].interactable = true;

            // Animation Play (Dotori)
            ObjectList[9].transform.GetChild(1).gameObject.SetActive(true);

        T1_2Flag = true;
    }


// +-------------------------------------------------------------------------------+//
// 2. Text : 도토리를 발견했어요! 말을 놓아 도토리를 주워보세요.
// Onclick : BushMap / BackGround / Item / dotori
    public void T2()
    {

        // Animation Off (Dotori)
        ObjectList[9].transform.GetChild(1).gameObject.SetActive(false);

        // SetActive True My ZIZI 2
        ObjectList[1].SetActive(true);

        // Bush Cut
        GameObject.Find("bush PointC_1").SetActive(false);
        GameObject.Find("bush PointC_2").SetActive(false);
        GameObject.Find("bush PointC_3").SetActive(false);

        // SetActive false dotori
        GameObject.Find("dotori").SetActive(false);

        // SetActive true : My dotori
        ObjectList[6].SetActive(true);

        // Text 2-1.
        textMeshPro.text = "도토리를 얻었어요!";

        // Animation Play (My Dotori)
        ObjectList[9].transform.GetChild(2).gameObject.SetActive(true);

        // Buton interactive false : dotori
        ButtonList[2].interactable = false;

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;


            T2_1Flag = false;
    }


// 2-1. Text : 도토리를 얻었어요! (next button)
// Onclick : Next Button
    bool T2_1Flag = true;
    public void T2_1_Next()
    {
        if (T2_1Flag == true){ return; }

            // Animation Off (My Dotori)
            ObjectList[9].transform.GetChild(2).gameObject.SetActive(false);

            // Text 2-2.
            textMeshPro.text = "도토리는 상대 말을\n제거하는 아이템입니다.";

            // Buton interactive false : Next Button 
            ButtonList[0].interactable = false;

        T2_1Flag = true;        

        StartCoroutine(DelayCoroutine_T2_1());
    }
    IEnumerator DelayCoroutine_T2_1()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button 
        ButtonList[0].interactable = true;
        T2_2Flag = false; 
    }

 
// 2-2. Text : 도토리는 상대 말을 제거하는 아이템입니다. (next button)
// Onclick : Next Button 
    bool T2_2Flag = true;
    public void T2_2_Next()
    {
        if (T2_2Flag == true){ return; }

            // Text 2-3.
            textMeshPro.text = "자신의 말 중 하나를 선택하면\n주변의 상대 말을 없앨 수 있습니다.";

            // Buton interactive false : Next Button 
            ButtonList[0].interactable = false;

        T2_2Flag = true;

        StartCoroutine(DelayCoroutine_T2_2());
    }
    IEnumerator DelayCoroutine_T2_2()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T2_3Flag = false;
    }


// 2-3. Text : 자신의 말 중 하나를 선택하면 주변의 상대 말을 없앨 수 있습니다. (next button)
// Onclick : Next Button 
    bool T2_3Flag = true;
    public void T2_3_Next()
    {
        if (T2_3Flag == true){ return; }

            // Text 2-4.
            textMeshPro.text = "방금 획득한 도토리 아이템을\n사용해볼까요?";

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T2_3Flag = true;
        
        StartCoroutine(DelayCoroutine_T2_3());
    }
    IEnumerator DelayCoroutine_T2_3()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T2_4Flag = false; 
    }


// 2-4. Text : 방금 획득한 도토리 아이템을 사용해볼까요? (next button) (상대편이 생김)
    bool T2_4Flag = true;
    public void T2_4_Next()
    {
        if (T2_4Flag == true){ return; }

            // Text 2-5.
            textMeshPro.text = "도토리를 집어보세요.";
            
            // Animation Play (My Dotori)
            ObjectList[9].transform.GetChild(2).gameObject.SetActive(true);

            // SetActive true : juk
            ObjectList[3].SetActive(true); // BIBI 1
            ObjectList[5].SetActive(true); // BIBI 3

            // Buton interactive false : Next Button 
            ButtonList[0].interactable = false;

        T2_4Flag = true; 
        
        // Buton interactive true : my dotori
        ButtonList[3].interactable = true;     
    }


// 2-5. Text : 도토리를 집어보세요. (도토리 아이콘 스폿 애니메이션)
// Onclick : My Item dotori
    public void T2_5()
    {
        // Animation Off (My Dotori)
        ObjectList[9].transform.GetChild(2).gameObject.SetActive(false);

        // SetActive false : my dotori
        ObjectList[6].SetActive(false);

        // Animation : Spot My ZIZI 2

        // Text 2-6.
        textMeshPro.text = "말 위에 도토리를 놓아보세요.";

        // Animation Play (ZIZI 2)
        ObjectList[9].transform.GetChild(3).gameObject.SetActive(true);

        // (4) Buton interactive false : my dotori
        ButtonList[3].interactable = false;

        // (5) Buton interactive true : My ZIZI 2
        ObjectList[1].GetComponent<Button>().interactable = true;
    }


// 2-6. Text : 말 위에 도토리를 놓아보세요. (지지 스폿 애니메이션)
// Onclick : My ZIZI 2
    public void T2_6()
    {
        // Animation Off (ZIZI 2)
        ObjectList[9].transform.GetChild(3).gameObject.SetActive(false);

        // SetActive false : juk 
        ObjectList[3].SetActive(false); // BIBI 1
        ObjectList[5].SetActive(false); // BIBI 3

        // Animation Play (skill) : BIBI 1, BIBI 3
        ObjectList[9].transform.GetChild(4).gameObject.SetActive(true);
        ObjectList[9].transform.GetChild(5).gameObject.SetActive(true);

        // Text 2-7.
        textMeshPro.text = "잘했어요!\n(Tip) 도토리로 상대를 공격할 수 있습니다.";

        // Buton interactive false : My ZIZI
        ObjectList[1].GetComponent<Button>().interactable = false;

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;

            T2_7Flag = false;
   }


// 2-7. Text : 잘했어요! (Tip) 도토리로 상대를 공격할 수 있습니다. (next button)
// Onclick : Next Button
    bool T2_7Flag = true;
    public void T2_7_Next()
    {
        if (T2_7Flag == true){ return; }

            // Animation Off (skill) : BIBI 1, BIBI 3
            ObjectList[9].transform.GetChild(4).gameObject.SetActive(false);
            ObjectList[9].transform.GetChild(5).gameObject.SetActive(false);

            // Text 3.
            textMeshPro.text = "이번에는 나뭇잎을 발견했어요!\n말을 놓아 나뭇잎을 주워보세요.";

            // Animation Play (leaf)
            ObjectList[9].transform.GetChild(6).gameObject.SetActive(true);

            // Buton interactive false : Next Button 
            ButtonList[0].interactable = false;

            // Buton interactive true : leaf
            ButtonList[4].interactable = true;

        T2_7Flag = true;
    }


// +-------------------------------------------------------------------------------+//
// 3. Text : 이번에는 나뭇잎을 발견했어요! 말을 놓아 나뭇잎을 주워보세요.
// Onclick : BushMap / BackGround / Item / leaf
    public void T3()
    {
        // Animation Off (leaf)
        ObjectList[9].transform.GetChild(6).gameObject.SetActive(false);

        // SetActive True My ZIZI 3
        ObjectList[2].SetActive(true); // ZIZI 3

        // Bush Cut
        GameObject.Find("bush PointD_1").SetActive(false);
        GameObject.Find("bush PointD_2").SetActive(false);

        // SetActive false : leaf item
        GameObject.Find("leaf").SetActive(false);

        // SetActive true : my leaf
        ObjectList[7].SetActive(true);

        // Text 3-1.
        textMeshPro.text = "나뭇잎을 얻었어요!";

        // Animation Play (My leaf)
        ObjectList[9].transform.GetChild(7).gameObject.SetActive(true);

        // Buton interactive false : leaf
        ButtonList[4].interactable = false;

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        

            T3_1Flag = false;
    }


// 3-1. Text : 나뭇잎을 얻었어요! (next button)
// Onclick : Next Button
    bool T3_1Flag = true;
    public void T3_1_Next()
    {
        if (T3_1Flag == true){ return; }

            // Animation Off (My leaf)
            ObjectList[9].transform.GetChild(7).gameObject.SetActive(false);

            // Text 3-2.
            textMeshPro.text = "나뭇잎은 상대의 도토리 공격을\n방어하는 아이템입니다.";

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T3_1Flag = true;

        StartCoroutine(DelayCoroutine_T3_1());
    }
    IEnumerator DelayCoroutine_T3_1()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T3_2Flag = false;
    }


// 3-2. Text : 나뭇잎은 상대의 도토리 공격을 방어하는 아이템입니다. (next button)
    bool T3_2Flag = true;
    public void T3_2_Next()
    {
        if (T3_2Flag == true){ return; }

            // Text 3-3.
            textMeshPro.text = "방금 획득한 나뭇잎 아이템을\n사용해볼까요?";

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T3_2Flag = true;

        StartCoroutine(DelayCoroutine_T3_2());
    }
    IEnumerator DelayCoroutine_T3_2()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T3_3Flag = false;
    }


// 3-3. // Text :  방금 획득한 나뭇잎 아이템을 사용해볼까요? (next button) (상대편이 생김)
// Onclick : Next Button
    bool T3_3Flag = true;
    public void T3_3_Next()
    {
        if (T3_3Flag == true){ return; }

            // SetActive true : juk
            ObjectList[3].SetActive(true); // BIBI 1
            
            // Text 3-4.
            textMeshPro.text = "나뭇잎을 집어보세요.";

            // Animation Play (My leaf)
            ObjectList[9].transform.GetChild(7).gameObject.SetActive(true);

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

            // Buton interactive true : My leaf
            ButtonList[5].interactable = true;

        T3_3Flag = true;
    }


// 3-4. Text : 나뭇잎을 집어보세요. (나뭇잎 아이콘 스폿 애니메이션)
// Onclick : My Item leaf
    public void T3_4()
    {

        // Animation Off (My leaf)
        ObjectList[9].transform.GetChild(7).gameObject.SetActive(false);

        // SetActive false : My leaf 
        ObjectList[7].SetActive(false);

        // Animation : Spot My ZIZI 3

        // Text 3-5.
        textMeshPro.text = "말 위에 나뭇잎을 놓아보세요.";

        // Animation Play (ZIZI 3)
        ObjectList[9].transform.GetChild(8).gameObject.SetActive(true);

        // Buton interactive false : My leaf
        ButtonList[5].interactable = false;

        // Buton interactive true : My ZIZI 3
        ObjectList[2].GetComponent<Button>().interactable = true;
    }


// 3-5. Text : 말 위에 나뭇잎을 놓아보세요. (지지 스폿 애니메이션) (상대 공격 애니메이션)
// Onclick : My ZIZI 3
    public void T3_5()
    {
        // SetActive true : Leaf
        ObjectList[8].SetActive(true);

        // Animation Off (ZIZI 3))
        ObjectList[9].transform.GetChild(8).gameObject.SetActive(false);


        StartCoroutine(DelayCoroutine3());        
    }
    IEnumerator DelayCoroutine3()
    {
        // Delay
        yield return new WaitForSeconds(0.8f);

        // Animation : Spot JUK

        // SetActive false : ZIZI 1, 2
        ObjectList[0].SetActive(false);
        ObjectList[1].SetActive(false);

        // Animation Play (skill) BIBI 1, ZIZI 1, ZIZI 2
        ObjectList[9].transform.GetChild(9).gameObject.SetActive(true);
        ObjectList[9].transform.GetChild(10).gameObject.SetActive(true);
        ObjectList[9].transform.GetChild(11).gameObject.SetActive(true);

        // Animation : ZIZI 1, 2

        // Text 3-6.
        textMeshPro.text = "잘했어요!\n(Tip) 나뭇잎을 붙인 지지는\n상대의 도토리 공격을 받지 않습니다.";

        // Buton interactive false : My ZIZI 3
        ObjectList[2].GetComponent<Button>().interactable = true;

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;

        T3_6Flag = false;
    }


// 3-6. Text : 잘했어요! (Tip) 나뭇잎을 붙인 지지는 상대의 도토리 공격을 받지 않습니다. (next button)
// Onclick : Next Button
    bool T3_6Flag = true;
    public void T3_6_Next()
    {
        if (T3_6Flag == true){ return; }

            // Animation Play (skill) BIBI 1, ZIZI 1, ZIZI 2
            ObjectList[9].transform.GetChild(9).gameObject.SetActive(false);
            ObjectList[9].transform.GetChild(10).gameObject.SetActive(false);
            ObjectList[9].transform.GetChild(11).gameObject.SetActive(false);

            // Animation : DANCE ZIZI

            // Text 4.
            textMeshPro.text = "아이템은 최대 3개씩 모을 수 있고,\n5턴 이후부터 사용 가능합니다.";

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T3_6Flag = true;

        StartCoroutine(DelayCoroutine_T3_6());
    }
    IEnumerator DelayCoroutine_T3_6()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T4Flag = false;
    }


// +-------------------------------------------------------------------------------+//
// 4. 아이템은 최대 3개씩 모을 수 있고, 5턴 이후부터 사용 가능합니다. (next button) (UI 하이라이트)
// Onclick : Next Button
    bool T4Flag = true;
    public void T4_Next()
    {
        if (T4Flag == true){ return; }

            // Text 4-1.
            textMeshPro.text = "한 턴에는 제한시간이 있습니다.\n시간이 지나면 턴이 넘어가니 주의하세요!";

            // SetActive true : DANCE ZIZI

            // Animation : Timer

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T4Flag = true;

        StartCoroutine(DelayCoroutine_T4());
    }
    IEnumerator DelayCoroutine_T4()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T4_1Flag = false;
    }


// 4-1. 한 턴에는 제한시간이 있습니다. 시간이 지나면 턴이 넘어가니 주의하세요! (next button) 
// Onclick : Next Button
    bool T4_1Flag = true;
    public void T4_1_Next()
    {
        if (T4_1Flag == true){ return; }

            // Text 4-2.
            textMeshPro.text = "5개의 말을 일렬로 놓으면\n게임에서 승리합니다.";

            // SetActive true : DANCE ZIZI

            // Animation : Timer

            // Buton interactive false : Next Button
            ButtonList[0].interactable = false;

        T4_1Flag = true;

        StartCoroutine(DelayCoroutine_T4_1());
    }
    IEnumerator DelayCoroutine_T4_1()
    {
        // Delay
        yield return new WaitForSeconds(0.5f);

        // Buton interactive true : Next Button
        ButtonList[0].interactable = true;
        T4_2Flag = false;
    }


// 4-2. 5개의 말을 일렬로 놓으면 게임에서 승리합니다. (게임 시작 버튼 생김)
// Onclick : Next Button
    bool T4_2Flag = true;
    public void T4_2_Next()
    {
        if (T4_2Flag == true){ return; }

            // Text 4-2.
            textMeshPro.text = "시작 버튼을 눌러\n게임을 시작해보세요!";

            // SetActive false : Skip Button
            Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);

            // SetActive true : Start Button
            Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);

            // SetActive false : Next Button
            Main.transform.GetChild(1).transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);

        T4_2Flag = true;
    }

// 4-3. 게임 시작 ㄱㄱ

// Onclick : T4 / Start Game

}