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
            // 이전에 선택되어 있던 버튼의 선택 상태를 해제합니다.
            if (selectColor != null)
            {
                selectColor.interactable = false ; // 선택 해제되었으므로 버튼을 다시 활성화합니다.
            }

            // 새로 선택된 버튼을 현재 선택된 버튼으로 설정합니다.
            selectColor = clickedButton;
            ChangemyZiziColor(selectColor.GetComponent<Image>().color);
            

            // 새로 선택된 버튼을 선택된 상태로 설정하고 다른 버튼들은 선택 해제합니다.
            foreach (Button button in colorPallate)
            {
                button.interactable = (button != selectColor); // 선택된 버튼만 활성화합니다.
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
