using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : Singleton<UIManager>
{
    #region Input Region
    [Header("Waiting Player Panel")]
    public GameObject waitingPlayerPanel;

    [Header("Select Color Panel")]
    public GameObject selectColorPanel;
    [SerializeField] private Image ziziImage;
    public Button[] colorPallate;

    private Button selectButton;
    public int sendSelectButtonindex = -1;
    public Button gamePlayButton;

    [Header("Wait Select Color")]
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private Image myZizi;
    public Image opZizi;
    [SerializeField] private TMP_Text opText;

    [Header("Count Down Panel")]
    [SerializeField] private GameObject countDownPanel;
    [SerializeField] private TMP_Text countDownText;

    private bool readresevedData = false;

    
    #endregion

    void Start()
    {
        gamePlayButton.interactable = false;
        foreach (Button button in colorPallate)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }


    }
    void Update()
    {

        if (DataManager.Instance.gamedata.isMaxRoomTriger)
        {
            changeUIAToB(waitingPlayerPanel, selectColorPanel);
            DataManager.Instance.gamedata.isMaxRoomTriger = false;
        }

        isreadygame();
    }

    private void isreadygame()
    {
        if (readresevedData)
        {
            Debug.Log("Update Ω√¿€");
            if (DataManager.Instance.gamedata.opcolor.a == 1f)
            {
                Debug.Log("reseved Data");
                opZizi.color = DataManager.Instance.gamedata.opcolor;
                opText.text = "Ready";

                readresevedData = false;

                StartCoroutine(ScenesManager.Instance.ChangeSceneToA("GameOnline", 3));

                CountBeforeGameStart();

            }
        }
    }


    #region OnlineGameReadyScene UIScript
    
    public void InitWaitingPlayerPanel()
    {

        changeUIAToB(selectColorPanel, waitingPlayerPanel);

        gamePlayButton.interactable = false;

        ziziImage.color = new Color(255, 255, 255, 0.75f);

        if (sendSelectButtonindex != -1)
        {
            colorPallate[sendSelectButtonindex].interactable = true;
        }
        sendSelectButtonindex = -1;

        if (selectButton != null)
        {
            selectButton.interactable = true;
        }
        selectButton = null;
    }

    private void OnButtonClick(Button clickedButton)
    {
        gamePlayButton.interactable = true;
        if (selectButton != clickedButton)
        {
            if(selectButton != null)
            {
                selectButton.interactable = true;
            }

            selectButton = clickedButton;
            ziziImage.color = selectButton.GetComponent<Image>().color;

            for(int i=0; i < colorPallate.Length; i++)
            {
                if(colorPallate[i] == selectButton)
                {
                    colorPallate[i].interactable = false;
                    NetworkManager.Instance.SendButtoninterable(i, false);
                }
                else
                {
                    colorPallate[i].interactable = true;
                }
            }
            if (sendSelectButtonindex != -1)
            {
                colorPallate[sendSelectButtonindex].interactable = false;
            }
        }
        
    }
    public void ClickGameStart()
    {
        selectColorPanel.SetActive(false);

        DataManager.Instance.gamedata.mycolor = ziziImage.color;
        Color Sendcolor = DataManager.Instance.gamedata.mycolor;
        NetworkManager.Instance.SendMyColor(Sendcolor.r, Sendcolor.g, Sendcolor.b, Sendcolor.a);

        waitPanel.SetActive(true);

        myZizi.color = DataManager.Instance.gamedata.mycolor;
        readresevedData = true;
        if (DataManager.Instance.gamedata.opcolor.a == 1f) { opZizi.color = DataManager.Instance.gamedata.opcolor; }
    }

    public void CountBeforeGameStart()
    {
        countDownPanel.SetActive(true);
        countDownText.transform.DOScale(new Vector3(10, 10, 10), 1).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            countDownText.transform.DOScale(new Vector3(6, 6, 6), 0);
            countDownText.text = "2";

            countDownText.transform.DOScale(new Vector3(10, 10, 10), 1).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                countDownText.transform.DOScale(new Vector3(6, 6, 6), 0);
                countDownText.text = "1";

                countDownText.transform.DOScale(new Vector3(10, 10, 10), 1);
            }
            );
        }
        );
    }

    #endregion

    #region OnlineGameScene UIScrpt



    #endregion



    void changeUIAToB(GameObject a, GameObject b)
    {
        a.SetActive(false);
        b.SetActive(true);
    }
}
