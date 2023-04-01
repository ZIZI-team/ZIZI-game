using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class ReadyGame_Local2P : MonoBehaviour
{    

    // : Skin
    private List<GameObject> MySkin = new List<GameObject>();   // Start : Add
    public int MySkinIndex = 0;

    public GameObject SkinPalette;                      // Start : Find
    public GameObject Selected_Skin;                    // Start : Instantiate

    // : Block
    public GameObject Block_1P;                         // Block Same color of 1P (using panel)

    // : Color
    public List<GameObject> ColorPalette_Object;        // ShowColorPalette() : Instantiate
    public List<Color> Skincolor_list;                  // GetSKINColor() : GetComponent

    public GameObject ColorPalette;                     // Start : Find
    public GameObject ColorPrefab;                      // Start : Find

    public Color Selected_Color;                        // GetSKINColor() : initiate, SelectColor_prefab()



// ------------------------------------------------------------------------------------------------------------------------ //

    void Start()
    {

    // Declare Color Palette Block
        Block_1P = GameObject.Find("1PColor").transform.GetChild(1).gameObject;        

    // SKIN 
        // Declare Skin Palette (Parent)
        SkinPalette = GameObject.Find("2P/2PSkin");

        // Declare Color Palette (Parent)
        ColorPalette = GameObject.Find("2P/2PColor");

        // Declare Color (Child)
        ColorPrefab = Resources.Load<GameObject>("Color_Prefab/"+"ColorPrefab");

        // MY SKIN (Own)
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN1"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN2"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN1"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN3"));

        Initiate_SKIN();
    }

    bool InitiateFlag = false;
    public void Initiate_SKIN()
    {
        InitiateFlag = true;

        // Set initiate SKIN Prefab
            Selected_Skin = Instantiate(MySkin[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Selected_Skin.transform.SetParent(SkinPalette.transform, false);
            
        ShowSkinColor();

        // Block Skin(1P) by Initiate Selected Skin(2P)
        BlockColor_1P();
        GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().BlockColor_2P();
    }

    public void ShowSkinColor()
    {

    // Reset Color Palette (Child List)
        if(ColorPalette_Object.Count > 0){
            Transform[] childList = ColorPalette.GetComponentsInChildren<Transform>();
            for (int i = 0; i < ColorPalette_Object.Count; i++){ Destroy(childList[i+2].gameObject); }
        }
        ColorPalette_Object = new List<GameObject>();

    // Show new Color Palette
        Skincolor_list = new List<Color>();
        Skincolor_list = MySkin[MySkinIndex].GetComponent<SKIN>().Color_List.ToList();
        for (int i = 0; i < Skincolor_list.Count; i++)
        {
            if (i < 3){ ColorPalette_Object.Add(Instantiate(ColorPrefab, new Vector3(-216 + i*216, 9, 0), Quaternion.identity) as GameObject); }
            else if (i >= 3){ ColorPalette_Object.Add(Instantiate(ColorPrefab, new Vector3(-216 + (i-3)*216, -177, 0), Quaternion.identity) as GameObject); }

            ColorPalette_Object[i].GetComponent<Image>().color = Skincolor_list[i];
            ColorPalette_Object[i].transform.SetParent(ColorPalette.transform, false);
        }

    // Set Initiate Final Skin Color
        // If current 2P MySkin is same 1P MySkin : default skin index --> 1

            if (InitiateFlag == false)
            {            
                for(int i = 0; i < Skincolor_list.Count; i++)
                {
                    if (Skincolor_list[i] == GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().Selected_Color){ continue; }
                    else { Selected_Color = Skincolor_list[i]; break; }
                }
            }
            // Initiate_SKIN : 2P index must be 1
            else if (InitiateFlag == true)
            {
                Selected_Color = Skincolor_list[1];
                InitiateFlag = false;
            }

        else { Selected_Color = Skincolor_list[0]; }

        Selected_Skin.GetComponent<Image>().color = Selected_Color;
        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Final_Skin_2P = Selected_Skin;
        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().DidYouSelect_Skin2 = true;
    }


    // Unity Prefab : Color Prefab Onclick
    public void SelectColor_prefab(GameObject colorPrefab)
    {
    // Change Final Skin Color
        Selected_Color = colorPrefab.GetComponent<Image>().color;    
        Selected_Skin.GetComponent<Image>().color = Selected_Color;

        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Final_Skin_2P = Selected_Skin;
        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().DidYouSelect_Skin2 = true;
        //Debug.Log("ChangeColor : " + Selected_Color + "/ " + Selected_Skin.name);     

    // Block Skin(1P) by Initiate Selected Skin(2P)
        BlockColor_1P();                
    }


    public void BlockColor_1P()
    {
    // Block Skin(1P) by Selected Skin(2P)

        // Find 2P Color in Current 1P Color Palette And Block
        for(int i = 0; i < GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().ColorPalette_Object.Count; i++)
        {
            // Debug.Log(GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().ColorPalette_Object.Count);

            if(GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().ColorPalette_Object[i].GetComponent<Image>().color == Selected_Color)
            {
                Block_1P.transform.SetAsLastSibling();
                Block_1P.SetActive(true);

                if (i < 3){ Block_1P.GetComponent<RectTransform>().localPosition = new Vector3(-216 + i*216, 9, 0); }
                else if (i >= 3){ Block_1P.GetComponent<RectTransform>().localPosition = new Vector3(-216 + (i-3)*216, -177, 0); }
                break;
            }
        }   
    }


    // Unity : UPButton 2P Onclick
    public void SkinIndexUp()
    {
        Destroy(Selected_Skin);
        if (MySkinIndex == MySkin.Count - 1){ MySkinIndex = 0; }
        else { MySkinIndex++; }

        Selected_Skin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Selected_Skin.transform.SetParent(SkinPalette.transform, false);
        ShowSkinColor();
        
    // ReBlock Skin(2P) by Selected Skin(1P)
        GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().Block_2P.SetActive(false);
        GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().BlockColor_2P();

    // ReBlock Skin(1P) by Selected Skin(2P) : Initiate 2P  
        Block_1P.SetActive(false);
        BlockColor_1P();    
    }


    // Unity : DownButton 2P Onclick
    public void SkinIndexDown()
    {
        Destroy(Selected_Skin); 
        if (MySkinIndex == 0){ MySkinIndex = 2; }
        else { MySkinIndex--; }

        Selected_Skin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Selected_Skin.transform.SetParent(SkinPalette.transform, false);
        ShowSkinColor();

    // ReBlock Skin(2P) by Selected Skin(1P)
        GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().Block_2P.SetActive(false);
        GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().BlockColor_2P();

    // ReBlock Skin(1P) by Selected Skin(2P) : Initiate 2P  
        Block_1P.SetActive(false);
        BlockColor_1P();
    }



    //// No Use, Set aside ////
    // ------------------------------------------------------------------------------------------------- //

    // : Final Skin, Instantiate
    public GameObject ZIZI_Parent;                      // Start : Find  
    public GameObject ZIZIPAPA;
    public GameObject Final_Skin;                       // SelectSkin() : newSkin
    public Color Final_SkinColor;                       // SelectSkin() : Selected_Color   


    // Unity : Select Button 2P Onclick
    public void SelectSkin()
    {
        ZIZI_Parent = Resources.Load<GameObject>("SKIN_Prefab/"+"ZIZILand");

        // Destroy Final Skin
        if (Final_Skin != null){ Destroy(Final_Skin); }

        // Select Object : Parent Object For Instantiate
        GameObject SelectPanel = GameObject.Find("2P/2PSelect");

        // Final Skin
        ZIZIPAPA = Instantiate(ZIZI_Parent, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
        ZIZIPAPA.transform.SetParent(SelectPanel.transform, false);

        Final_Skin = Instantiate(Selected_Skin, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Final_Skin.transform.SetParent(ZIZIPAPA.transform, false);

        // Set Final Skin Color
        Final_SkinColor = Selected_Color;
        Final_Skin.GetComponent<Image>().color = Final_SkinColor;

        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Final_Skin_2P = Final_Skin;
        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().DidYouSelect_Skin2 = true;
    }

    void Update()
    {

    }
}
