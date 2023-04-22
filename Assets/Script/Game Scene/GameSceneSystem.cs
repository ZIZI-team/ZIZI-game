using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameSceneSystem : MonoBehaviour
{

// +++++ UI +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    float ratio = (float)Screen.width / (float)1440;

    [Header("Timer")]
    public Text TimerText;                           // inspector
    public GameObject TimerHand;                     // inspector

    float time = 20f;
    float fullTime = 20f;
    
    [Header("Main UI")]
    public GameObject MainUI;                       // inspector

    [Header("PlayerTurn Function")]
    public GameObject playerTurnIcon;               // inspector

    [Header("Pause Menu")]
    public GameObject PausePanel;                     // inspector

    [Header("Game Result Panel")]
    public GameObject PlayerWinPanel;               // Inspector


// +++++ Map +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    public GameObject Map;

    [SerializeField] public int[,] ZIZIBoard = new int[11+ 12, 11+ 12]; // ZIZI : B & W

    List<GameObject> BushList = new List<GameObject>();
    List<GameObject> ZIZIList = new List<GameObject>();

    public List<int> ItemIndex = new List<int>();

    public GameObject ButtonMap;

    public int mapGridNum_x;
    public int mapGridNum_y;
    
    GameObject Rock; // Find
    GameObject Bush; // Find

    public GameObject Game;                           // Unity : Inspector
    public Transform ZIZI_Transform;                  // Unity : Inspector
    

// +++++ Stone +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    GameObject Stone; 

    public GameObject Stone_b, Stone_w;     // Unity : Inspector
    // RectTransform rectTransform_b;
    // RectTransform rectTransform_w;

    public GameObject Stone_b_Color;
    public GameObject Stone_w_Color;    

    public GameObject MyTurn_1P;
    public GameObject MyTurn_2P;

    private bool isBlack = true;
    
    public int Turn = 0;


// +++++ Item List +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    [Header("Gameplay Item System")]
    public GameObject ItemSlotUI_1P;
    public GameObject ItemSlotUI_2P;
    public GameObject dotoriIcon;
    public GameObject leafIcon;
    public List<GameObject> P1_Item = new List<GameObject>();
    public List<GameObject> P2_Item = new List<GameObject>();

    public bool Skill_Dotori = false;
    public bool Skill_Leaf = false;


// +++++ Win Condition +++++ //
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

    public int blackStoneCount = 0;
    public int whiteStoneCount = 0;
    public bool musicIsOn = false;


// ------------------------------------------------------------------------------------------------------------------------ //

    void Start()
    {              
        // 1. Set Map Grid & Stone (Initiate state)
            Map = GameObject.Find("Game").transform.GetChild(0).gameObject;
            mapGridNum_x = 11;
            mapGridNum_y = 11;            

            Stone_b_Color.GetComponent<Image>().color = Stone_b.GetComponent<Image>().color;
            Stone_w_Color.GetComponent<Image>().color = Stone_w.GetComponent<Image>().color;    

            // Stone_b.SetActive(true);        
            // Stone_w.SetActive(true);        

        #region // Screen Ratio
        //     panel = Map.transform.GetChild(1).gameObject.GetComponent<RectTransform>();

        //     // Panel Size
        //     panelSize = panel.rect.size;

        //     // Get Canvas Scaler component
        //     CanvasScaler canvasScaler = Map.transform.GetChild(1).gameObject.GetComponentInParent<CanvasScaler>();

        //     // Calculate Size by bCanvas Scaler match mode
        //     if (canvasScaler.matchWidthOrHeight == 0)
        //     {
        //         // Width Match
        //         float scaleFactor = Screen.width / canvasScaler.referenceResolution.x;
        //         panelSize *= scaleFactor;
        //     }
        //     else
        //     {
        //         // Height Match 
        //         float scaleFactor = Screen.height / canvasScaler.referenceResolution.y;
        //         panelSize *= scaleFactor;
        //     }
        #endregion
        
        // 2. Make Initiate Board (Record Initiate Information), BushBoard
            for (int i = 0; i < Map.transform.GetChild(0).transform.GetChild(4).childCount; i++)
            { 
                if (Map.transform.GetChild(0).transform.GetChild(4).transform.GetChild(i).gameObject.activeSelf == true)
                { BushList.Add(Map.transform.GetChild(0).transform.GetChild(4).transform.GetChild(i).gameObject); } 
            } 
            Reset_Item();

    }

    void Reset_Item()
    {   
        #region Reset Panel

            PausePanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f) + new Vector3(2000f, 0f, 0f);
            PlayerWinPanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f) + new Vector3(3800f, 0f, 0f);

            // ZIZIBoard 6 : No Rock, Under Bush
            Rock = Map.transform.GetChild(0).transform.GetChild(0).gameObject;
            Bush = Map.transform.GetChild(0).transform.GetChild(4).gameObject;
            ButtonMap = Map.transform.GetChild(0).transform.GetChild(1).gameObject;

            Debug.Log(Rock.transform.childCount);
            Debug.Log(Bush.transform.childCount);
            Debug.Log(ButtonMap.transform.childCount);            

            int GridIndexMax = mapGridNum_x * mapGridNum_y;

        #endregion

        #region Reset Bush, Dotori, Leaf, ZIZI, Button

            isBlack = true;
            Stone = Stone_b;

            Selected_Item = Stone_b;

            for (int i = 0; i < BushList.Count; i++){ BushList[i].SetActive(true); } // Bush
            for (int i = 0; i < Map.transform.GetChild(0).transform.GetChild(2).childCount; i++){ Destroy(Map.transform.GetChild(0).transform.GetChild(2).transform.GetChild(i).gameObject); } // Dotori
            for (int i = 0; i < Map.transform.GetChild(0).transform.GetChild(3).childCount; i++){ Destroy(Map.transform.GetChild(0).transform.GetChild(3).transform.GetChild(i).gameObject); } // Leaf
            for (int i = 0; i < ZIZIList.Count; i++){ Destroy(ZIZIList[i]); } // ZIZI

            for (int i = 0; i < ButtonMap.transform.childCount; i++){ ButtonMap.transform.GetChild(i).transform.GetChild(0).gameObject.name = "Button"; } // ButtonMap name

            //for (int i = 0; i < Rock.)

            // ItemList = new List<GameObject>();
            ZIZIList = new List<GameObject>();
        
            Dotori_P1 = 0;
            Dotori_P2 = 0;
            Leaf_P1 = 0;
            Leaf_P2 = 0;

        // Reset Item

            for (int j = 0; j < P1_Item.Count; j++){ Destroy(P1_Item[j]); }
            for (int j = 0; j < P2_Item.Count; j++){ Destroy(P2_Item[j]); }

            P1_Item = new List<GameObject>();
            P2_Item = new List<GameObject>();

            Skill_Dotori = false;
            Skill_Leaf = false;

            Turn = 0;

        #endregion

        #region Set ZIZIBoard
        
            // Reset ZIZIBoard
            for (int i = 0; i < mapGridNum_y + 12; i++){ for (int j = 0; j < mapGridNum_x + 12; j++){ ZIZIBoard[i, j] = 0; ZIZIBoard[i, j] = 0; } }
            
            // ZIZIBoard 6 : if Bush on the Rock (Cannot Spon ZIZI)
            for(int i = 0; i < 6; i++){ for(int j = 0; j < mapGridNum_x + 12; j++){ ZIZIBoard[i, j] = 6; } }
            for(int i = 6; i < mapGridNum_y + 6; i++){ for(int j = 0; j < 6; j++){ ZIZIBoard[i, j] = 6; } for(int j = mapGridNum_x + 6; j < mapGridNum_x + 12; j++){ ZIZIBoard[i, j] = 6; } }
            for(int i = mapGridNum_y + 6; i < mapGridNum_y + 12; i++){ for(int j = 0; j < mapGridNum_x + 12; j++){ ZIZIBoard[i, j] = 6; } }
                        
            for (int i = 0; i < GridIndexMax; i++)
            { 
                if (Rock.transform.GetChild(i).gameObject.activeSelf == false){ ZIZIBoard[(int)(i / mapGridNum_x) + 6, i % mapGridNum_x + 6] = 6; }  //  Make ZIZIBoard 6 if [ No Rock ]                
                if (Bush.transform.GetChild(i).gameObject.activeSelf == true){ ZIZIBoard[(int)(i / mapGridNum_x) + 6, i % mapGridNum_x + 6] = 6; ItemIndex.Add(i); }  //  Make ZIZIBoard 6 if [ under bush ] 
            }
        
        #endregion

        #region Make Item Under Bush 

            // None : 1
            // Dotori : 2
            // Leaf : 3

            List<int> ItemPercentage = new List<int>(){3, 2, 3, 3, 2, 3, 2, 3, 2, 2, 2, 3};
            int RandomIndex = 0;
            int ItemSetIndex = 0;
            
            for (int i = 0; i < 12; i++)
            {                
                RandomIndex = Random.Range(0, ItemIndex.Count);
                ItemSetIndex = ItemIndex[RandomIndex];
                ItemIndex.RemoveAt(RandomIndex);

                if (ItemPercentage[0] == 2)
                {
                    GameObject Dotori = Instantiate(Resources.Load("Item_Prefab/"+"dotori"), Rock.transform.GetChild(ItemSetIndex).transform.position, Quaternion.identity) as GameObject;
                    Dotori.transform.SetParent(Map.transform.GetChild(0).transform.GetChild(2).transform);
                    Dotori.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 121.8f, ratio * 121.8f, 1f);
                    Dotori.name += "_" + ItemSetIndex.ToString();
                }
                else if (ItemPercentage[0] == 3)
                {
                    GameObject Leaf = Instantiate(Resources.Load("Item_Prefab/"+"leaf"), Rock.transform.GetChild(ItemSetIndex).transform.position, Quaternion.identity) as GameObject;
                    Leaf.transform.SetParent(Map.transform.GetChild(0).transform.GetChild(3).transform);
                    Leaf.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 121.8f, ratio * 121.8f, 1f);
                    Leaf.name += "_" + ItemSetIndex.ToString();
                }

                ItemPercentage.RemoveAt(0);
            }
            
            ItemIndex = new List<int>();

        #endregion
    }

    void Update()
    {
        #region // Set Stone Size > Small Stone

            // rectTransform_b = Stone_b.GetComponent<RectTransform>();
            // rectTransform_b.sizeDelta = new Vector2(120f, 120f);
            
            // rectTransform_w = Stone_w.GetComponent<RectTransform>();
            // rectTransform_w.sizeDelta = new Vector2(120f, 120f);

        #endregion

        Timer();
    }

    public void Timer()
    {
        time -= Time.deltaTime;
        TimerText.text = "TIME : " + time.ToString("F1");
        TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 360f*(fullTime-time)/fullTime);

        if (time <= fullTime * 0.2f)
        {
            TimerHand.transform.parent.gameObject.GetComponent<Animator>().SetBool("TimerEnd", true);
            TimerHand.transform.parent.gameObject.GetComponent<Animator>().SetBool("Entry", false);
        }
        if (time <= 0)
        {
            TimerHand.transform.parent.gameObject.GetComponent<Animator>().SetBool("TimerEnd", false);
            TimerHand.transform.parent.gameObject.GetComponent<Animator>().SetBool("Entry", true);
            changePlayer(); 
        }
    }

    public void changePlayer()
    {
        #region Check Did Player Used Item

            Skill_Dotori = false;
            Skill_Leaf = false;

            // Selected_Item.SetActive(true);

        #endregion

        #region Timer, Button, Change Rock Color

            // Reset Timer
                time = fullTime;
                TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            // Curr : Player 1, Next : Player 2
                if (isBlack == true)  
                {
                    isBlack = false;
                    Stone = Stone_w;
                    MyTurn_1P.SetActive(false);
                    MyTurn_2P.SetActive(true);

                    // Item Slot Button (Player 2) Interactive false
                        if (Turn >= 10)
                        {
                            foreach (GameObject item in P2_Item) // true
                            { 
                                item.transform.GetChild(0).GetComponent<Button>().interactable = true;                       // Item
                                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = true; // chiso
                            }
                            foreach (GameObject item in P1_Item) // false
                            { 
                                item.transform.GetChild(0).GetComponent<Button>().interactable = false;                       // Item
                                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = false; // chiso
                            }
                        }
                }

            // Curr : Player 2, Next : Player 1
                else 
                {
                    isBlack = true;
                    Stone = Stone_b;
                    MyTurn_1P.SetActive(true);
                    MyTurn_2P.SetActive(false);

                    // Item Slot Button (Player 1) Interactive false
                        if (Turn >= 10)
                        {
                            foreach (GameObject item in P2_Item) // false
                            { 
                                item.transform.GetChild(0).GetComponent<Button>().interactable = false;                       // Item
                                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = false; // chiso
                            }
                            foreach (GameObject item in P1_Item) // true
                            { 
                                item.transform.GetChild(0).GetComponent<Button>().interactable = true;                       // Item
                                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = true; // chiso
                            }
                        }
                }

            // Can't Use Item Before turn 10
                if (Turn < 10)
                {
                    foreach (GameObject item in P2_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
                    foreach (GameObject item in P1_Item){ item.transform.GetChild(0).GetComponent<Button>().interactable = false; }
                    Turn++;
                }

        
        #endregion
    }


// >> For UI << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region OnClickReset() : RESET Func. / CONTINUE(GameOver)
    
        public void OnClickReset()
        {
            Reset_Item();

            isBlack = true;

            time = fullTime;
            TimerHand.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            Time.timeScale = 1f;
        }

    #endregion
    

    #region OnClickPause() : PAUSE Func.

        public void OnClickPause()
        {
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", false);
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", true);
            PausePanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f);
            StartCoroutine(Wait0());
        }
        IEnumerator Wait0()
        {
            yield return new WaitForSeconds(0.5f);
            Time.timeScale = 0f;
        }

    #endregion
    
    #region OnClickMainMenu() : HOME Func.

        public void OnClickMainMenu() 
        {
            Time.timeScale = 1f;
            StartCoroutine(Wait1());
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", true);
            PlayerWinPanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", true);
            
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", false);
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", false);
        }
        IEnumerator Wait1()
        {
            yield return new WaitForSeconds(0.3f);            
            SceneManager.LoadScene("TitleScene");
        }
    
    #endregion

    #region OnClickContinue() : BACK(Pause) Func.

        public void OnClickContinue() 
        {
            Time.timeScale = 1f;
            StartCoroutine(Wait2());
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", true);
            PlayerWinPanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", true);

            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", false);
            PausePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", false);
        }
        IEnumerator Wait2()
        {
            yield return new WaitForSeconds(0.3f);

            PausePanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f) + new Vector3(2000f, 0f, 0f);
            PlayerWinPanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f) + new Vector3(3800f, 0f, 0f);
        }

    #endregion

    #region 

    #endregion


// >> Game : Spon << //
// ------------------------------------------------------------------------------------------------------------------------ //

    // Rock?óê OnclickRock ?ä§?Å¨Î¶ΩÌä∏ ?ó∞Í≤∞ÌïòÍ∏?
    #region OnclickRock() : Spon ZIZI on Rock
 
        int RockIndex;
        int ListX;
        int ListY;

        public void OnclickRock(GameObject ButtonMap_Button)
        {
            // gameObject : ButtonMap Button

            // Get This Rock's Child Index
                for (int i = 0; i < ButtonMap.transform.childCount; i++){ if (ButtonMap.transform.GetChild(i).transform.GetChild(0) == ButtonMap_Button.transform){ RockIndex = i; break; } }  
                ListX = RockIndex % mapGridNum_x + 6;
                ListY = (int)(RockIndex / mapGridNum_x) + 6;

            if (ButtonMap_Button.name == "Button_ZIZI")
            {
                
                ZIZI_index = RockIndex;
                OnclickZIZI( Rock.transform.GetChild(RockIndex).transform.GetChild(0).transform.GetChild(0).gameObject ); // ZIZILand > ZIZI
                return;
            }

            else if (ButtonMap_Button.name == "Button")
            {
                if (Skill_Dotori == true || Skill_Leaf == true){ return; }

                GameObject ZIZILand = Instantiate(Resources.Load("SKIN_Prefab/"+"ZIZILand"), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
                ZIZILand.transform.SetParent(Rock.transform.GetChild(RockIndex).transform, false);

                GameObject ZIZI_instance = Instantiate(Stone, new Vector3(0f, 0f, -0.01f), Quaternion.identity) as GameObject;
                ZIZI_instance.transform.SetParent(ZIZILand.transform, false);

                ZIZI_instance.name = isBlack? "Player1" : "Player2"; 
                ZIZI_instance.name += "_" + RockIndex.ToString() + "_Happy";
                ZIZIList.Add(ZIZILand);

            // Record Color on ZIZIBoard
                ZIZIBoard[ListY, ListX] = isBlack ? 1 : 2;  // 1 : Black, 2 : White

            // Rock Button interactable false
                // ButtonMap_Button.GetComponent<Button>().interactable = false;

                ButtonMap_Button.name += "_ZIZI";
                
                CutBush(RockIndex);
                WinCondition();
                
                changePlayer();          
            }  
            
        }

    #endregion
    #region CutBush()
    
        public void CutBush(int RockIndex)
        {
            if (RockIndex % mapGridNum_x != mapGridNum_x - 1){ Bush.transform.GetChild(RockIndex + 1).gameObject.SetActive(false); }
            if (RockIndex % mapGridNum_x != 0){ Bush.transform.GetChild(RockIndex - 1).gameObject.SetActive(false); }
            if ((int)(RockIndex / mapGridNum_x) != 0){ Bush.transform.GetChild(RockIndex - mapGridNum_x).gameObject.SetActive(false); }
            if ((int)(RockIndex / mapGridNum_x) != mapGridNum_y - 1){ Bush.transform.GetChild(RockIndex + mapGridNum_x).gameObject.SetActive(false); }
        }
    
    #endregion

    // Item?óê ?ä§?Å¨Î¶ΩÌä∏ ?ó∞Í≤∞ÌïòÍ∏?
    #region OnclickMapItem()
    
        public int Dotori_P1 = 0;
        public int Dotori_P2 = 0;
        public int Leaf_P1 = 0;
        public int Leaf_P2 = 0;

        public void OnclickMapItem(GameObject Clicked_MapItem)
        {
            // Skill Item Animation (Down)
            Selected_Item.GetComponent<Animator>().SetBool("On", false);
            Selected_Item.GetComponent<Animator>().SetBool("Off", true);            

            // gameObject : Item on map
            string[] ItemNameList = Clicked_MapItem.name.Split("_");

                // instantiate ZIZI

                    int itemIndex = int.Parse(ItemNameList[1]);
                    ListX = itemIndex % mapGridNum_x + 6;
                    ListY = (int)(itemIndex / mapGridNum_x) + 6;

                    GameObject ZIZILand = Instantiate(Resources.Load("SKIN_Prefab/"+"ZIZILand"), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
                    ZIZILand.transform.SetParent(Rock.transform.GetChild(itemIndex).transform, false);

                    GameObject ZIZI_instance = Instantiate(Stone, new Vector3(0f, 0f, -0.01f), Quaternion.identity) as GameObject;
                    ZIZI_instance.transform.SetParent(ZIZILand.transform, false);

                    ZIZI_instance.name = isBlack? "Player1" : "Player2"; 
                    ZIZI_instance.name += "_" + itemIndex.ToString() + "_Happy";
                    ZIZIList.Add(ZIZILand);

                // Record Color on ZIZIBoard
                    ZIZIBoard[ListY, ListX] = isBlack ? 1 : 2;  // 1 : Black, 2 : White

                    ButtonMap.transform.GetChild(itemIndex).transform.GetChild(0).gameObject.name += "_ZIZI";
                    CutBush(itemIndex);

                    WinCondition();
                    

            if (ItemNameList[0] == "dotori(Clone)")
            {
                Clicked_MapItem.SetActive(false); // Can also Remove Item After count >= 3

                if (isBlack == true && Dotori_P1 >= 3 || isBlack == false && Dotori_P2 >= 3 ){ changePlayer(); return; } // Can get Item Max 3
                Dotori_P1 += isBlack ? 1 : 0;
                Dotori_P2 += isBlack ? 0 : 1;

                if (isBlack == true)
                {
                    GameObject ItemForSlot = Instantiate(dotoriIcon, ItemSlotUI_1P.transform.position + new Vector3((-665f + (120 * (Dotori_P1)))*ratio, 0f), Quaternion.identity) as GameObject;
                    ItemForSlot.transform.SetParent(ItemSlotUI_1P.transform);
                    ItemForSlot.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                    P1_Item.Add(ItemForSlot);
                }
                else
                {
                    GameObject ItemForSlot = Instantiate(dotoriIcon, ItemSlotUI_2P.transform.position + new Vector3((-665f + (120 * (Dotori_P2)))*ratio, 0f), Quaternion.identity) as GameObject;
                    ItemForSlot.transform.SetParent(ItemSlotUI_2P.transform);
                    ItemForSlot.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                    P2_Item.Add(ItemForSlot);
                }
            }
            else if (ItemNameList[0] == "leaf(Clone)")
            {
                Clicked_MapItem.SetActive(false); // Can also Remove Item After count >= 3

                if (isBlack == true && Leaf_P1 >= 3 || isBlack == false && Leaf_P2 >= 3 ){ changePlayer(); return; } // Can get Item Max 3
                Leaf_P1 += isBlack ? 1 : 0;
                Leaf_P2 += isBlack ? 0 : 1;                

                if (isBlack == true)
                {
                    GameObject ItemForSlot = Instantiate(leafIcon, ItemSlotUI_1P.transform.position + new Vector3((190f + (120 * (Leaf_P1)))*ratio, 0f), Quaternion.identity) as GameObject;
                    ItemForSlot.transform.SetParent(ItemSlotUI_1P.transform);
                    ItemForSlot.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                    P1_Item.Add(ItemForSlot);
                }
                else
                {
                    GameObject ItemForSlot = Instantiate(leafIcon, ItemSlotUI_2P.transform.position + new Vector3((190f + (120 * (Leaf_P2)))*ratio, 0f), Quaternion.identity) as GameObject;
                    ItemForSlot.transform.SetParent(ItemSlotUI_2P.transform);
                    ItemForSlot.GetComponent<RectTransform>().sizeDelta = new Vector3(ratio * 180f, ratio * 180f, 1f);
                    P2_Item.Add(ItemForSlot);
                }
            }
            else { return; }

            changePlayer();
        }

    #endregion

    
    #region GameOver()

        public GameObject ZIZIonPanel; //inspector

        public void GameOver()
        {
            PlayerWinPanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("close", false);
            PlayerWinPanel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("open", true);

            PlayerWinPanel.transform.localPosition = Game.transform.position - new Vector3(Screen.width/2, Screen.height/2, 0f);

            Skill_Dotori = false;
            Skill_Leaf = false;

            if (isBlack == true){ ZIZIonPanel.GetComponent<Image>().color = Stone_b.GetComponent<Image>().color; }
            else { ZIZIonPanel.GetComponent<Image>().color = Stone_w.GetComponent<Image>().color; }

            StartCoroutine(Wait0());
        }

    #endregion


// >> Skill : Dotori & Leaf << //
// ------------------------------------------------------------------------------------------------------------------------ //

    // Item?óê ?ä§?Å¨Î¶ΩÌä∏ ?ó∞Í≤∞ÌïòÍ∏?
    #region OnClickSlotItem(), SkillChiso()

        public GameObject Selected_Item;

        public void OnClickSlotItem(GameObject Clicked_SlotItem) // Item(Button Onclick) : Script : SkillClick : OnClickItem()
        {
            // gameObject : Item on slot
            Selected_Item = Clicked_SlotItem;

            // Skill Item Animation (UP)
            Clicked_SlotItem.GetComponent<Animator>().SetBool("On", true);
            Clicked_SlotItem.GetComponent<Animator>().SetBool("Off", false);
            
            if(Clicked_SlotItem.name == "dotori_skill(Clone)"){ Skill_Dotori = true; }
            else if(Clicked_SlotItem.name == "leaf_skill(Clone)"){ Skill_Leaf = true; } 
        }

        public void SkillChiso(GameObject Clicked_SlotChiso)
        {
            // gameObject : Item on slot - chiso button

            // Skill Item Animation (Down)
            Clicked_SlotChiso.GetComponent<Animator>().SetBool("On",false);
            Clicked_SlotChiso.GetComponent<Animator>().SetBool("Off", true);

            Skill_Dotori = false;
            Skill_Leaf = false;
        }

    #endregion
    
    #region PlaySkill_Dotori() 

        string[] WaitingZIZIname;

        public void PlaySkill_Dotori(GameObject Clicked_ZIZI) 
        {
            int JukColor = 0;
            if (isBlack == true){ JukColor = 2; } 
            else { JukColor = 1; }

            Selected_Item.SetActive(false);
            GameObject MamMADotori = Instantiate(Resources.Load("Item_Prefab/"+"dotori"), new Vector3(0f, 40f, 0f), Quaternion.identity) as GameObject;
            MamMADotori.transform.SetParent(Clicked_ZIZI.transform, false);

            StartCoroutine(WaitMamMA(MamMADotori, JukColor));
        }
        IEnumerator WaitMamMA(GameObject Dotori, int JukColor)
        {
            yield return new WaitForSeconds(0.4f);

            // Animation Juk, Destroy Juk    
            if ((int)(ZIZI_index / mapGridNum_x) != 0 && ZIZIBoard[ListY - 1, ListX] == JukColor)
            { 
                Rock.transform.GetChild(ZIZI_index - mapGridNum_x).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);  // Animation
                StartCoroutine(WaitSecond(ZIZI_index - mapGridNum_x));
            }
            if (ZIZI_index % mapGridNum_x != mapGridNum_x - 1 && ZIZIBoard[ListY, ListX + 1] == JukColor)
            { 
                Rock.transform.GetChild(ZIZI_index + 1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);  // Animation
                StartCoroutine(WaitSecond(ZIZI_index + 1));
            }
            if ((int)(ZIZI_index / mapGridNum_x) != mapGridNum_y - 1 && ZIZIBoard[ListY + 1, ListX] == JukColor)
            { 
                Rock.transform.GetChild(ZIZI_index + mapGridNum_x).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);  // Animation
                StartCoroutine(WaitSecond(ZIZI_index + mapGridNum_x));
            }
            if (ZIZI_index % mapGridNum_x != 0 && ZIZIBoard[ListY, ListX - 1] == JukColor)
            { 
                Rock.transform.GetChild(ZIZI_index - 1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);  // Animation
                StartCoroutine(WaitSecond(ZIZI_index - 1));
            }

            yield return new WaitForSeconds(0.3f);
            Destroy(Dotori);  
        }
        IEnumerator WaitSecond(int index)
        {
            yield return new WaitForSeconds(0.3f);

            GameObject WaitingZIZI = Rock.transform.GetChild(index).transform.GetChild(0).gameObject;
            WaitingZIZIname = WaitingZIZI.transform.GetChild(0).gameObject.name.Split("_");
            if (WaitingZIZIname[2] != "Leaf"){ Destroy(WaitingZIZI); }
            ButtonMap.transform.GetChild(index).transform.GetChild(0).gameObject.name = "Button";
        }
    
    #endregion

    #region PlaySkill_Leaf() 

        public void PlaySkill_Leaf(GameObject Clicked_ZIZI)
        {
            Selected_Item.SetActive(false);

            string[] ForNewName;
            ForNewName = Clicked_ZIZI.name.Split("_");
            Clicked_ZIZI.name = ForNewName[0] + "_" + ForNewName[1] + "_Leaf";

            GameObject Leaf = Instantiate(Resources.Load("Item_Prefab/"+"leaf"), new Vector3(0f, 40f, 0f), Quaternion.identity) as GameObject;
            Leaf.transform.SetParent(Clicked_ZIZI.transform, false);

            ButtonMap.transform.GetChild(ZIZI_index).transform.GetChild(0).gameObject.name = "Button_Leaf";
        }

    #endregion

    // ZIZI?óê OnclickZIZI  ?ä§?Å¨Î¶ΩÌä∏ ?ó∞Í≤∞ÌïòÍ∏?
    #region OnclickZIZI() : Skill or not

        GameObject Skill_ZIZI;
        int ZIZI_index;

        public void OnclickZIZI(GameObject Clicked_ZIZI)
        {
            // gameObject : zizi on map
                Skill_ZIZI = Clicked_ZIZI;
                string[] ZIZIname = Skill_ZIZI.name.Split("_");

            // Debug.Assert(Skill_Dotori == true, "Skill_Dotori : " + Skill_Dotori.ToString());
            // Debug.Assert(Skill_Leaf == true, "Skill_Leaf : " + Skill_Leaf.ToString());

            // Skill dotori
                if (Skill_Dotori == true)
                {
                    if (isBlack == true && ZIZIname[0] == "Player1"){ PlaySkill_Dotori(Skill_ZIZI); }
                    else if (isBlack == false && ZIZIname[0] == "Player2"){ PlaySkill_Dotori(Skill_ZIZI); }
                    else 
                    { 
                        // Skill Item Animation (Down)
                        Selected_Item.GetComponent<Animator>().SetBool("On", false);
                        Selected_Item.GetComponent<Animator>().SetBool("Off", true);     
                        Skill_Dotori = false;       
                        return;
                    }
                }

            // Skill leaf
                else if (Skill_Leaf == true)
                {
                    if (isBlack == true && ZIZIname[0] == "Player1"){ PlaySkill_Leaf(Skill_ZIZI); }
                    else if (isBlack == false && ZIZIname[0] == "Player2"){ PlaySkill_Leaf(Skill_ZIZI); }
                    else 
                    { 
                        // Skill Item Animation (Down)
                        Selected_Item.GetComponent<Animator>().SetBool("On", false);
                        Selected_Item.GetComponent<Animator>().SetBool("Off", true);            
                        Skill_Leaf = false;       
                        return;
                    }
                }
            // else : Return
                else return;  
            
            changePlayer();
        }
    
    #endregion



// >> Check Condition << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region CheckCondition() : Common Function

        public bool CheckCondition(int indexY, int indexX, bool StoneColor, 
        Func<int, int, int, bool> Func1, Func<int, int, int, bool> Func2, Func<int, int, int, bool> Func3, Func<int, int, int, bool> Func4, int kMin) // make 5 : win
        {
            // return true : Win, return false : Pass

            int color;  // 1 : Black, 2 : White
            if (StoneColor == true){ color = 1; }
            else { color = 2; }

            bool TrueFlag = false;
            try 
            {
                for (int k = kMin; k <= 0; k++) // F
                {
                    TrueFlag = Func1(indexY + k, indexX, color); // F
                    if (TrueFlag == true) { return true; }
                    TrueFlag = Func2(indexY, indexX + k, color); // F
                    if (TrueFlag == true) { return true; }
                    TrueFlag = Func3(indexY + k, indexX + k, color); // F
                    if (TrueFlag == true) { return true; }
                    TrueFlag = Func4(indexY + k, indexX - k, color); // F
                    if (TrueFlag == true) { return true; }
                }
                return false;
            } 

            finally 
            {
                if(TrueFlag) {
                    string board = "";
                    for (int i = 0; i < 11 + 12; i++) {
                        string line = "";
                        for (int j = 0; j < 11 + 12; j++) {
                            line += ZIZIBoard[i, j] + " ";
                        }
                        board += line + "\n";
                    }
                }
            }
        }

    #endregion

// >> Check Win Condition : make 5 << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region Check3Condition()

        public bool checkCond_3 = false;
        public int StoneCount_Cond3 = 0;

        public void Check3Condition()
        {
            checkCond_3 = CheckCondition(ListY, ListX, isBlack, Check3_Y_Plus, Check3_X_Plus, Check3_XY_Plus, Check3_XY_Minus, -4);

            if (StoneCount_Cond3 == 5 && checkCond_3 == true && isBlack == true)
            {
                Debug.Log("black coondition 3");
            }
            else if (StoneCount_Cond3 == 5 && checkCond_3 == true && isBlack == false)
            {
                Debug.Log("White coondition 3");
            }
            else { Debug.Log("coondition 3 Pass"); }
        }

    #endregion

        #region Check3_Y_Plus()
        public bool Check3_Y_Plus(int startPointY, int StartPointX, int color)//int i
        {
            StoneCount_Cond3 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Cond3 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 5, StartPointX] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }

            return true;
        }
        #endregion

        #region Check3_X_Plus()
        public bool Check3_X_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Cond3 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY, StartPointX + i] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Cond3 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY, StartPointX + 5] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }

            return true;
        }
        #endregion

        #region Check3_XY_Plus()
        public bool Check3_XY_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Cond3 = 0;
            
            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX + i] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Cond3 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 5, StartPointX + 5] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }        
            
            return true;
        }
        #endregion

        #region Check3_XY_Minus()
        public bool Check3_XY_Minus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Cond3 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX - i] == color){ StoneCount_Cond3 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Cond3 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 5, StartPointX - 5] == 0){ StoneCount_Cond3 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Cond3 = 0; return false; }  

            return true;
        }
        #endregion


// >> Check Win Condition : make 5 << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region WinCondition()

        public bool checkCond_Win = false;
        public int StoneCount_Win = 0;

        public void WinCondition()
        {
            checkCond_Win = CheckCondition(ListY, ListX, isBlack, Check_Y_Plus, Check_X_Plus, Check_XY_Plus, Check_XY_Minus, -4);

            if (StoneCount_Win == 5 && checkCond_Win == true && isBlack == true)
            {
                Debug.Log("Player1 Win!");
                Anim4Condition();   // Animation

                // mostTopCanvas.transform.SetAsLastSibling();
                GameOver();
            }
            else if (StoneCount_Win == 5 && checkCond_Win == true && isBlack == false)
            {
                Debug.Log("Player 2 Win!");
                Anim4Condition();   // Animation

                // mostTopCanvas.transform.SetAsLastSibling(); 
                GameOver();
            }
            else { Debug.Log("Pass"); }
        }
    #endregion

        #region Check_Y_Plus()
        public bool Check_Y_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Win = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
                else { StoneCount_Win = 0; return false; }
            }
            return true;
        }
        #endregion

        #region Check_X_Plus()
        public bool Check_X_Plus (int startPointY, int StartPointX, int color)
        {
            StoneCount_Win = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ZIZIBoard[startPointY, StartPointX + i] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
                else { StoneCount_Win = 0; return false; }
            }
            return true;
        }
        #endregion

        #region Check_XY_Plus()
        public bool Check_XY_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Win = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX + i] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
                else { StoneCount_Win = 0; return false; }
            }
            return true;
        }
        #endregion

        #region Check_XY_Minus()
        public bool Check_XY_Minus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Win = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX - i] == color){ StoneCount_Win += 1; } // ZIZI : Existed / Same Color (# 5)
                else { StoneCount_Win = 0; return false; }
            }
            return true;
        }
        #endregion  


// >> ZIZI : play Animation << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region Animationn

        // Hop Animation : Default
        public void Play_Animation1()
        {   int cnt = 0;
            for(int i = 0; i < ZIZIList.Count; i++)
            {
                StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim1"));
                Debug.Log(cnt++);
            }
        }
        IEnumerator Rand_timer(float time, int index, string AnimPara)
        {
            yield return new WaitForSeconds(time);

            ZIZIList[index].GetComponent<ZIZIAnim_Game>().AnimT(AnimPara);
        }

        // Nervous Animation : Play By Function Anim2Condition()
        public void Play_Animation2(string PlayerName) 
        {
            for(int i = 0; i < ZIZIList.Count; i++)
            {
                if (ZIZIList[i].name.Split("_")[0] == PlayerName){ ZIZIList[i].GetComponent<ZIZIAnim_Game>().AnimT("Anim2"); }

                //StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim2")); } 
                //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim2"); }

                // Code : Animation Played, but not active
            }
        }

        // Dance Animation : Play By Function Anim3Condition()
        public void Play_Animation3(string PlayerName)
        {
            for(int i = 0; i < ZIZIList.Count; i++)
            {
                if (ZIZIList[i].name.Split("_")[0] == PlayerName){ StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim3")); } 
                
                //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim3"); }

                // Code : Animation Played, but not active
            }
        }

        // Win Animation : Play By Function Anim4Condition()
        public void Play_Animation4(string PlayerName)
        {
            for(int i = 0; i < ZIZIList.Count; i++)
            {
                if (ZIZIList[i].name.Split("_")[0] == PlayerName){ StartCoroutine(Rand_timer(Random.Range(0.1f, 0.7f), i, "Anim4")); } 
                
                //ZIZI_Transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<ZIZIAnim_Game>().AnimT("Anim4"); }
                
                // Code : Animation Played, but not active
            }
        }

    #endregion  

// >> Animation : Check Condition [ Play_Animation2 : Nervous ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region Anim2Condition()
    
        public bool checkCond_Anim2 = false;
        public int StoneCount_Anim2 = 0;

        public void Anim2Condition()
        {
            checkCond_Anim2 = CheckCondition(ListY, ListX, isBlack, Anim2_Check_Y_Plus, Anim2_Check_X_Plus, Anim2_Check_XY_Plus, Anim2_Check_XY_Minus, -5);

            if (StoneCount_Anim2 == 5 && checkCond_Anim2 == true && isBlack == true)
            {
                Debug.Log("Player1 Play_Animation2! : Nervous");
                Play_Animation2("Player2");
            }
            else if (StoneCount_Anim2 == 5 && checkCond_Anim2 == true && isBlack == false)
            {
                Debug.Log("Player 2 Play_Animation2! : Nervous");
                Play_Animation2("Player1");
            }
            else { Debug.Log("Play_Animation2 Pass"); }
        }

    #endregion

        #region Anim2_Check_Y_Plus()
        public bool Anim2_Check_Y_Plus(int startPointY, int StartPointX, int color)//int i
        {
            StoneCount_Anim2 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Anim2 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 4, StartPointX] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }

            return true;
        }
        #endregion

        #region Anim2_Check_X_Plus()
        public bool Anim2_Check_X_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Anim2 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY, StartPointX + i] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Anim2 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY, StartPointX + 4] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }

            return true;
        }
        #endregion

        #region Anim2_Check_XY_Plus()
        public bool Anim2_Check_XY_Plus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Anim2 = 0;
            
            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX + i] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Anim2 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 4, StartPointX + 4] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }        
            
            return true;
        }
        #endregion

        #region Anim2_Check_XY_Minus()
        public bool Anim2_Check_XY_Minus(int startPointY, int StartPointX, int color)
        {
            StoneCount_Anim2 = 0;

            if (ZIZIBoard[startPointY, StartPointX] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }
            
            for (int i = 1; i < 4; i++)
            {
                if (ZIZIBoard[startPointY + i, StartPointX - i] == color){ StoneCount_Anim2 += 1; } // ZIZI : Existed / Same Color (# 4)
                    else { StoneCount_Anim2 = 0; return false; }
            }
            
            if (ZIZIBoard[startPointY + 4, StartPointX - 4] == 0){ StoneCount_Anim2 += 1; } // ZIZI : no Existed / 0
                else { StoneCount_Anim2 = 0; return false; }  

            return true;
        }
        #endregion


// >> Animation : Check Condition [ Play_Animation3 : Dance ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    // public bool checkCond_Anim3 = false;
    // public int StoneCount_Anim3 = 0;

    // public void Anim3Condition()
    // {
    //     checkCond_Anim3 = Anim3_Check_XY();
    //     if (checkCond_Anim3 == true && isBlack == true)
    //     {
    //         Debug.Log("Player 1 Play_Animation3! : Dance");
    //         Play_Animation3("Player1");
    //     }
    //     else if (checkCond_Anim3 == true && isBlack == false)
    //     {
    //         Debug.Log("Player 2 Play_Animation3! : Dance");
    //         Play_Animation3("Player2");
    //     }
    // }

    // public bool Anim3_Check_XY()
    // {
    //     if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
    //     else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
    //     else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
    //     else if (ItemBoard[yGapNum + 6 + 1, xGapNum + 6, 0] == 1){ return true; }
    //     else { return false; }
    // }


// >> Animation : Check Condition [ Play_Animation4 : Win ] << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region Anim4Condition()

        public void Anim4Condition()
        {
            if (isBlack == true)
            {
                Debug.Log("Player 1 Play_Animation4! : Win");
                // Play_Animation4("Player1");
            }
            else if (isBlack == false)
            {
                Debug.Log("Player 2 Play_Animation4! : Win");
                // Play_Animation4("Player2");
            }
        }    
    
    #endregion


// >> Music << //
// ------------------------------------------------------------------------------------------------------------------------ //

    #region OnclickMusic()

        public void OnclickMusic()
        {
            if (musicIsOn == true)
            {
                //mute = false;
                // music = Music.GetComponent<
                Game.GetComponent<AudioSource>().mute  = true;
                musicIsOn = false;
            } 
            else
            {
                //mute = true;
                Game.GetComponent<AudioSource>().mute  = false;
                musicIsOn = true;

            }
        }

    #endregion

}

