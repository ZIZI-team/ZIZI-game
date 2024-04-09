using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    [Header("Waiting Player Panel")]
    public GameObject waitingPlayerPanel;

    [Header("Select Color Panel")]
    public GameObject selectColorPanel;
    [SerializeField] private Image ziziImage;
    public Button[] colorPallate;
    private Button selectColor;

    [Header("Wait Select Color")]
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private Image myZizi;
    public Image opZizi;

    private bool readresevedData = false;

    void Start()
    {
        foreach (Button button in colorPallate)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        selectColor = colorPallate[0];
        selectColor.interactable = false;
    }
    void Update()
    {
        if (readresevedData)
        {
            Debug.Log("Update 시작");
            if (DataManager.Instance.gamedata.opcolor.a == 1f)
            {
                Debug.Log("reseved Data");
                opZizi.color = DataManager.Instance.gamedata.opcolor;
                readresevedData = false;
            }
        }
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
            ziziImage.color = selectColor.GetComponent<Image>().color;
            

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

    public void ClickGameStart()
    {
        selectColorPanel.SetActive(false);

        DataManager.Instance.gamedata.mycolor = ziziImage.color;
        NetworkManager.Instance.SendMyColor(DataManager.Instance.gamedata.mycolor.r, DataManager.Instance.gamedata.mycolor.g, DataManager.Instance.gamedata.mycolor.b, DataManager.Instance.gamedata.mycolor.a);

        waitPanel.SetActive(true);

        myZizi.color = DataManager.Instance.gamedata.mycolor;

        if (DataManager.Instance.gamedata.opcolor.a != 1f)
        {
            readresevedData = true;
        }
        else
        {
            opZizi.color = DataManager.Instance.gamedata.opcolor;
        }
    }

}
