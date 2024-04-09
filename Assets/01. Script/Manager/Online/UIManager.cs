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
            Debug.Log("Update ����");
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
            // ������ ���õǾ� �ִ� ��ư�� ���� ���¸� �����մϴ�.
            if (selectColor != null)
            {
                selectColor.interactable = false ; // ���� �����Ǿ����Ƿ� ��ư�� �ٽ� Ȱ��ȭ�մϴ�.
            }

            // ���� ���õ� ��ư�� ���� ���õ� ��ư���� �����մϴ�.
            selectColor = clickedButton;
            ziziImage.color = selectColor.GetComponent<Image>().color;
            

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
