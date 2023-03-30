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


    // 1. �����Ƹ� ��ã�� Ž���� ������ ���� 5�� ������ �ټ�����!
    // 2. ���� ������ ���ƺ���
    // 3. (�νð� �������) ���� �������!
    // 4. ���丮�� ���̳�? ���丮�� �ֿ�����
    // 5. ���丮�� �����! ���丮�� ��븦 �����Ҽ��ִ�
    // 6. ������� �ִ�, ���丮�� ���ƺ���
    // 7. ���߾�! ������ ���� ������� 5���� ������ ������ ���� ���� �� �ִ�
    // 8. �������� �ֿ�����
    // 9. �������� �����! ���������� ���丮 ������ ���� �� �ִ�
    // 10. �������� ���̸� ���� ������ ���� �� �ִ�
    // 11. �������� �ִ� 3���� ���� �� �ְ�, 5�� ���ĺ��� ��� �����ϴ�
    // 12. Ÿ�̸Ӱ� �����ϴ� �����ϵ��� ����
    // 13. ���� �����ϱ�
}
