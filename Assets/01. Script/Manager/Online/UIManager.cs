using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Tilemaps;


public class UIManager : Singleton<UIManager>
{
    #region Input Region
    [Header("Waiting Player Panel")]
    public GameObject waitingPlayerPanel; // 플레이어 대기 패널

    [Header("Select Color Panel")]
    public GameObject selectColorPanel; // 색상 선택 패널
    [SerializeField] private Image ziziImage; // zizi 이미지
    public Button[] colorPallate; // 색상 팔레트 버튼 배열

    private Button selectButton; // 선택된 버튼
    public int sendSelectButtonindex = -1; // 선택된 버튼 인덱스
    public Button gamePlayButton; // 게임 플레이 버튼

    [Header("Wait Select Color")]
    [SerializeField] private GameObject waitPanel; // 대기 패널
    [SerializeField] private Image myZizi; // 나의 zizi 이미지
    public Image opZizi; // 상대방 zizi 이미지
    [SerializeField] private TMP_Text opReadyCondition; // 상대방 텍스트

    [Header("Count Down Panel")]
    [SerializeField] private GameObject countDownPanel; // 카운트 다운 패널
    [SerializeField] private TMP_Text countDownText; // 카운트 다운 텍스트

    [Header("End Game Panel")]
    [SerializeField] private GameObject winnerPanel; // 승리 패널
    [SerializeField] private GameObject pausedPanel; // 일시정지 패널

    [Header("Item Prefabs")]
    [SerializeField] private GameObject dotori; // 도토리 프리팹
    [SerializeField] private GameObject leaf; // 잎사귀 프리팹

    private bool readresevedData = false; // 데이터 수신 여부



    #endregion

    void Start()
    {
        // 게임 플레이 버튼을 비활성화
        gamePlayButton.interactable = false;

        // 색상 팔레트의 각 버튼에 클릭 이벤트 리스너 추가
        foreach (Button button in colorPallate)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void Update()
    {
        // 최대 인원 방 트리거가 활성화되면 UI를 변경
        if (DataManager.Instance.gamedata.isMaxRoomTriger)
        {
            changeUIAToB(waitingPlayerPanel, selectColorPanel);
            DataManager.Instance.gamedata.isMaxRoomTriger = false;
        }

        // 게임 준비 상태 확인
        isreadygame();
    }


    #region OnlineGameReadyScene UIScript


    /// <summary>
    /// 게임 준비 상태를 확인하는 함수
    /// </summary>
    private void isreadygame()
    {
        // 수신된 데이터가 있을 경우
        if (readresevedData)
        {
            Debug.Log("Update 시작");
            // 상대방 색상이 설정되었는지 확인
            if (DataManager.Instance.gamedata.opcolor.a == 1f)
            {
                Debug.Log("reseved Data");
                // 상대방 zizi 색상 설정
                opZizi.color = DataManager.Instance.gamedata.opcolor;
                // 상대방 준비 상태 표시
                opReadyCondition.text = "Ready";

                // 데이터 수신 플래그 초기화
                readresevedData = false;

                // 온라인 게임 씬으로 전환
                StartCoroutine(ScenesManager.Instance.goToOnlineGameScene());

                // 게임 시작 전 카운트
                CountBeforeGameStart();
            }
        }
    }

    /// <summary>
    /// 대기 중인 플레이어 패널을 초기화하는 함수
    /// </summary>
    public void InitWaitingPlayerPanel()
    {
        // UI를 색상 선택 패널에서 대기 패널로 전환
        changeUIAToB(selectColorPanel, waitingPlayerPanel);

        // 게임 플레이 버튼을 비활성화
        gamePlayButton.interactable = false;

        // zizi 이미지의 색상을 반투명으로 설정
        ziziImage.color = new Color(255, 255, 255, 0.75f);

        // 선택된 버튼 인덱스가 유효하면 해당 버튼을 활성화
        if (sendSelectButtonindex != -1)
        {
            colorPallate[sendSelectButtonindex].interactable = true;
        }
        sendSelectButtonindex = -1;

        // 선택된 버튼이 있으면 해당 버튼을 활성화
        if (selectButton != null)
        {
            selectButton.interactable = true;
        }
        selectButton = null;
    }


    /// <summary>
    /// 버튼 클릭 시 호출되는 함수
    /// </summary>
    /// <param name="clickedButton">클릭된 버튼</param>
    private void OnButtonClick(Button clickedButton)
    {
        // 게임 플레이 버튼을 활성화
        gamePlayButton.interactable = true;

        // 선택된 버튼이 클릭된 버튼과 다른 경우
        if (selectButton != clickedButton)
        {
            // 이전에 선택된 버튼이 있으면 활성화
            if (selectButton != null)
            {
                selectButton.interactable = true;
            }

            // 클릭된 버튼을 선택된 버튼으로 설정
            selectButton = clickedButton;
            ziziImage.color = selectButton.GetComponent<Image>().color;

            // 색상 팔레트의 각 버튼을 순회하며 선택된 버튼은 비활성화, 나머지는 활성화
            for (int i = 0; i < colorPallate.Length; i++)
            {
                if (colorPallate[i] == selectButton)
                {
                    colorPallate[i].interactable = false;
                    NetworkManager.Instance.SendButtoninterable(i, false); // 선택된 버튼 상태를 네트워크로 전송
                }
                else
                {
                    colorPallate[i].interactable = true;
                }
            }

            // 이전에 선택된 버튼 인덱스가 유효하면 해당 버튼을 비활성화
            if (sendSelectButtonindex != -1)
            {
                colorPallate[sendSelectButtonindex].interactable = false;
            }
        }
    }

    /// <summary>
    /// 게임 시작 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void ClickGameStart()
    {
        // 색상 선택 패널 비활성화
        selectColorPanel.SetActive(false);

        // 나의 색상을 데이터 매니저에 저장하고 네트워크로 전송
        DataManager.Instance.gamedata.mycolor = ziziImage.color;
        Color Sendcolor = DataManager.Instance.gamedata.mycolor;
        NetworkManager.Instance.SendMyColor(Sendcolor.r, Sendcolor.g, Sendcolor.b, Sendcolor.a);

        // 대기 패널 활성화
        waitPanel.SetActive(true);

        // 나의 zizi 색상 설정
        myZizi.color = DataManager.Instance.gamedata.mycolor;
        readresevedData = true;

        // 상대방의 색상이 설정되어 있으면 상대방 zizi 색상 설정
        if (DataManager.Instance.gamedata.opcolor.a == 1f)
        {
            opZizi.color = DataManager.Instance.gamedata.opcolor;
        }
    }

    /// <summary>
    /// 게임 시작 전 카운트 다운을 수행하는 함수
    /// </summary>
    public void CountBeforeGameStart()
    {
        // 카운트 다운 패널 활성화
        countDownPanel.SetActive(true);

        // 카운트 다운 텍스트 애니메이션 설정 (3에서 2로)
        countDownText.transform.DOScale(new Vector3(10, 10, 10), 1).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            countDownText.transform.DOScale(new Vector3(6, 6, 6), 0);
            countDownText.text = "2";

            // 카운트 다운 텍스트 애니메이션 설정 (2에서 1로)
            countDownText.transform.DOScale(new Vector3(10, 10, 10), 1).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                countDownText.transform.DOScale(new Vector3(6, 6, 6), 0);
                countDownText.text = "1";

                // 카운트 다운 텍스트 애니메이션 설정 (1로 유지)
                countDownText.transform.DOScale(new Vector3(10, 10, 10), 1);
            });
        });
    }


    #endregion

    #region OnlineGameScene UIScrpt

    /// <summary>
    /// 승리 패널을 인스턴스화하는 함수
    /// </summary>
    public void InstantiateWinnerPanel()
    {
        // 승리 패널을 Canvas 오브젝트의 자식으로 인스턴스화
        Instantiate(winnerPanel, GameObject.Find("Canvas").transform);
    }

    /// <summary>
    /// 일시 정지 패널을 인스턴스화하는 함수
    /// </summary>
    public void InstantiatePausedPanel()
    {
        // 일시 정지 패널을 Canvas 오브젝트의 자식으로 인스턴스화
        Instantiate(pausedPanel, GameObject.Find("Canvas").transform);
    }

    #endregion

    #region Item UI
    /// <summary>
    /// 아이템을 획득하는 함수
    /// </summary>
    /// <param name="tilebase">타일 베이스</param>
    /// <param name="cellPos">타일 위치</param>
    public void GetItem(TileBase tilebase, Vector3Int cellPos)
    {
        //---변수 선언---
        string itemName = null;
        GameObject targetobject = null;

        //---tilebase Null 체크---
        if (tilebase == null)
        {
            Debug.LogError("Tilebase is null");
            return;
        }

        //---아이템 이름 및 오브젝트 설정---
        if (tilebase.name == "dotori")
        {
            itemName = "Dotori";
            targetobject = dotori;
        }
        else if (tilebase.name == "leaf")
        {
            itemName = "Leaf";
            targetobject = leaf;
        }

        //---targetobject Null 체크---
        if (targetobject == null)
        {
            Debug.LogError("Target object is null");
            return;
        }

        //---아이템 부모 오브젝트 설정---
        string setparentName = DataManager.Instance.gamedata.myP == DataManager.Instance.gamedata.turnData ? "My" : "Op";
        GameObject parentObject = GameObject.Find("Canvas").transform.Find(setparentName + "Inbantroy").gameObject;
        if (parentObject == null)
        {
            Debug.LogError("Parent object not found: " + setparentName + "Inbantroy");
            return;
        }

        GameObject instanceItme = Instantiate(targetobject, parentObject.transform);
        Transform itemParentTransform = GameObject.Find(setparentName + " " + itemName)?.transform;
        if (itemParentTransform == null)
        {
            Debug.LogError("Item parent not found: " + setparentName + " " + itemName);
            return;
        }
        instanceItme.transform.SetParent(itemParentTransform);

        //---아이템 개수 확인---
        int itemCount = instanceItme.transform.parent.childCount;

        //---카메라 Null 체크---
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found");
            return;
        }

        //---RectTransform Null 체크---
        RectTransform rectTransform = instanceItme.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not found on instance item");
            return;
        }

        //---아이템 이동---
        instanceItme.transform.position = mainCamera.WorldToScreenPoint(new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0));
        rectTransform.DOAnchorPos(new Vector2(100 + (50 * itemCount), 0), 2);

        //---인벤토리 데이터 업데이트---
        DataManager.Instance.UpdateInbantoryData(setparentName, itemName, itemCount - 1, true);
    }



    #endregion

    void changeUIAToB(GameObject a, GameObject b)
    {
        a.SetActive(false);
        b.SetActive(true);
    }
}
