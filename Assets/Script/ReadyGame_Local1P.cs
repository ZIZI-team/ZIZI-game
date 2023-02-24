using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class ReadyGame_Local1P : MonoBehaviour
{
    // +++ SKIN +++ //
    // Skin Prefab : SkinName(Str) + SkinImg(Img) + Color(Script) + Anim(Script) + Skill(Script)
    private List<GameObject> MySkin = new List<GameObject>();   // Start : Add
    public int MySkinIndex = 0;

    public GameObject SkinPalette;                      // Start : Find
    public GameObject newSkin;                          // Start : Instantiate

    // : Color
    public List<GameObject> ColorPalette_Object;        // ShowColorPalette() : Instantiate
    public List<Color> Skincolor_list;                  // GetSKINColor() : GetComponent

    public GameObject ColorPalette;                     // Start : Find
    public GameObject ColorPrefab;                      // Start : Find

    public Color Selected_Color;                        // GetSKINColor() : initiate, SelectColor_prefab()
    
    // : Final Skin
    public GameObject Final_Skin;                       // SelectSkin() : newSkin
    public Color Final_SkinColor;                       // SelectSkin() : Selected_Color         


    void Start()
    {
        // +++ SKIN +++ //

        // declare Skin Palette (Parent)
        SkinPalette = GameObject.Find("1P/1PSkin");

        // declare Color Palette (Parent)
        ColorPalette = GameObject.Find("1P/1PColor");

        // declare Color (Child)
        ColorPrefab = Resources.Load<GameObject>("ColorPrefab");

        // MY SKIN (Own)
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"ZIZI"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp1"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp2"));

        //if(GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().StartSKIN == true){ Initiate_SKIN(); }
        Initiate_SKIN();
    }

    public void Initiate_SKIN()
    {
        // Set initiate SKIN Prefab
        newSkin = Instantiate(MySkin[0], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        GetSKINColor();
    }

    public void GetSKINColor()
    {
        Skincolor_list = new List<Color>();
        Skincolor_list = MySkin[MySkinIndex].GetComponent<SKIN>().Color_List.ToList();
        Selected_Color = Skincolor_list[0];
        newSkin.GetComponent<Image>().color = Selected_Color;
        ShowColorPalette();
    }

    // Unity Prefab : Color Prefab Onclick
    public void SelectColor_prefab(GameObject colorPrefab)
    {
        Selected_Color = colorPrefab.GetComponent<Image>().color;             // ??

        // Change Selected Color
        newSkin.GetComponent<Image>().color = Selected_Color;
        Debug.Log("ChangeColor : " + Selected_Color + "/ " + newSkin.name);
    }


    public void ShowColorPalette()
    {
        // Reset Color Palette (Child List)
        if(ColorPalette_Object.Count > 0){
            Transform[] childList = ColorPalette.GetComponentsInChildren<Transform>();
            for (int i = 0; i < ColorPalette_Object.Count; i++){ Destroy(childList[i+2].gameObject); }
        }

        ColorPalette_Object = new List<GameObject>();

        // Make new Color Palette
        for (int i = 0; i < Skincolor_list.Count; i++)
        {
            ColorPalette_Object.Add(Instantiate(ColorPrefab, new Vector3(i*200, 0, 0), Quaternion.identity));
            ColorPalette_Object[i].GetComponent<Image>().color = Skincolor_list[i];
            ColorPalette_Object[i].transform.SetParent(ColorPalette.transform, false);
        }
    }


    void Update()
    {

    }


    // Unity : UPButton 1P Onclick
    public void SkinIndexUp()
    {
        Destroy(newSkin);
        if (MySkinIndex == MySkin.Count - 1){ MySkinIndex = 0; }
        else { MySkinIndex++; }

        newSkin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        GetSKINColor();
    }


    // Unity : DownButton 1P Onclick
    public void SkinIndexDown()
    {
        Destroy(newSkin); 
        if (MySkinIndex == 0){ MySkinIndex = 2; }
        else { MySkinIndex--; }

        newSkin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        GetSKINColor();
    }


    // Unity : Select Button 1P Onclick
    public void SelectSkin()
    {
        // Destroy Final Skin
        if (Final_Skin != null){ Destroy(Final_Skin); }

        // Select Object : Parent Object For Instantiate
        GameObject SelectPanel = GameObject.Find("1P/1PSelect");

        // Final Skin
        Final_Skin = Instantiate(newSkin, new Vector3(-200, -300, 0), Quaternion.identity);
        Final_Skin.transform.SetParent(SelectPanel.transform, false);

        // Set Final Skin Color
        Final_SkinColor = Selected_Color;
        Final_Skin.GetComponent<Image>().color = Final_SkinColor;

        GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Final_Skin_1P = Final_Skin;
    }
}
