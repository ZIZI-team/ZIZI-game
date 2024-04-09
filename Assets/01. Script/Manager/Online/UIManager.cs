using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    [Header("Select Color Panel")]
    [SerializeField] private GameObject selectColorPanel;
    [SerializeField] private Image ziziImage;
    public Button[] colorPallate;
    private Button selectColor;

    [Header("Wait Panel")]
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private Image myZizi;
    [SerializeField] private Image opZizi;

    void Start()
    {
        foreach (Button button in colorPallate)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        selectColor = colorPallate[0];
        selectColor.interactable = false;

        NetworkManager.Instance.Connect();
    }

    private void OnButtonClick(Button clickedButton)
    {
        
        if (selectColor != clickedButton)
        {
            // ������ ���õǾ� �ִ� ��ư�� ���� ���¸� �����մϴ�.
            if (selectColor != null)
            {
                selectColor.interactable = false ; // ���� �����Ǿ����Ƿ� ��ư�� �ٽ� Ȱ��ȭ�մϴ�.
            }

            // ���� ���õ� ��ư�� ���� ���õ� ��ư���� �����մϴ�.
            selectColor = clickedButton;
            ChangemyZiziColor(selectColor.GetComponent<Image>().color);
            

            // ���� ���õ� ��ư�� ���õ� ���·� �����ϰ� �ٸ� ��ư���� ���� �����մϴ�.
            foreach (Button button in colorPallate)
            {
                button.interactable = (button != selectColor); // ���õ� ��ư�� Ȱ��ȭ�մϴ�.
            }
        }
        
    }

    public void ChangemyZiziColor(Color color)
    {
        ziziImage.color = color;
    }

    public void ChangeOpZiziColorc(Color color)
    {
        opZizi.color = color;
    }

    public void ClickGameStart()
    {
        selectColorPanel.SetActive(false);

        DataManager.Instance.gamedata.mycolor = ziziImage.color;
        NetworkManager.Instance.SendMyColor(DataManager.Instance.gamedata.mycolor.r, DataManager.Instance.gamedata.mycolor.g, DataManager.Instance.gamedata.mycolor.b);

        waitPanel.SetActive(true);

        myZizi.color = DataManager.Instance.gamedata.mycolor;
    }

}
