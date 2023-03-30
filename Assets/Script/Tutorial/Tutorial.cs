using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> TutorialList = new List<GameObject>();
    int ListIndex = 0;

    public GameObject Main;
    void Start()
    {
        Main = GameObject.Find("Main");
        
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(0).gameObject);
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(1).gameObject);
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(2).gameObject);
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(3).gameObject);
        TutorialList.Add(Main.transform.GetChild(1).transform.GetChild(4).gameObject);
    }

    public void NextTutorial()
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
        SceneManager.LoadScene("TitleScene");
    }

    void Update()
    {
        
    }


    // 1. 지숭아를 되찾는 탐험을 떠나기 위해 5개 지지를 줄세우자!
    // 2. 땅에 지지를 놓아보자
    // 3. (부시가 사라지며) 땅이 사라졌다!
    // 4. 도토리가 보이네? 도토리를 주워보자
    // 5. 도토리를 얻었다! 도토리로 상대를 공격할수있다
    // 6. 상대편이 있다, 도토리를 놓아보자
    // 7. 잘했어! 공격을 통해 상대편이 5개의 지지를 모으는 것을 막을 수 있다
    // 8. 나뭇잎을 주워보자
    // 9. 나뭇잎을 얻었다! 나뭇잎으로 도토리 공격을 막을 수 있다
    // 10. 나뭇잎을 붙이면 나의 지지를 막을 수 있다
    // 11. 아이템은 최대 3개씩 모을 수 있고, 5턴 이후부터 사용 가능하다
    // 12. 타이머가 존재하니 조심하도록 하자
    // 13. 게임 시작하기
}
