using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGame : MonoBehaviour
{
    // +++ SKIN +++ //

    // Skin Prefab : SkinName(Str) + SkinImg(Img) + Color(Script) + Anim(Script) + Skill(Script)
    private List<GameObject> MySkin = new List<GameObject>();
    public int MySkinIndex = 0;

    public GameObject SkinPalette;
    public GameObject newSkin;

    // : Color
    public int Color_List_Count = 0;
    public Color Selected_Color;
    
    // : Final Skin
    public GameObject Final_Skin;
    public Color Final_SkinColor;
    public bool SetFinalSkin = false;

    // +++ MAP +++ //

    // Map Prefab : MapName(Str) + MapImg(Img) + MapRule(Script)
    private List<GameObject> Map = new List<GameObject>();


    
    void Start()
    {
        // MY SKIN (Own)
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"ZIZI"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp1"));
        MySkin.Add(Resources.Load<GameObject>("SKIN_Prefab/"+"SKIN_Temp2"));

        // MAP Can Choose
        Map.Add(Resources.Load<GameObject>("MAP_Prefab/MAP1"));  

        // Set initiate SKIN Prefab
        newSkin = Instantiate(MySkin[0], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        SearchSkin(0); 
        ChangeSkinColor();    
    }

    void Update()
    {

    }

    public void SkinIndexUp()
    {
        Destroy(newSkin);
        if (MySkinIndex == MySkin.Count - 1){ MySkinIndex = 0; }
        else { MySkinIndex++; }

        newSkin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        SearchSkin(MySkinIndex);
    }

    public void SkinIndexDown()
    {
        Destroy(newSkin); 
        if (MySkinIndex == 0){ MySkinIndex = 2; }
        else { MySkinIndex--; }

        newSkin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        SearchSkin(MySkinIndex);        
    }

    public void SearchSkin (int MySkinIndex) 
    {
        // var SkinScript = MySkin[MySkinIndex].GetComponent<SKIN_ZIZI>();  

        // Test
        // Debug.Log(MySkinIndex);
        Debug.Log(Selected_Color);

        
        // Debug.Log(SkinScript.Color_List[0]); 
    }

    // Color Prefab Onclick
    public void ChangeSkinColor()
    {
        newSkin.GetComponent<Image>().color = Selected_Color;
        Debug.Log("ChangeColor : " + Selected_Color + "/ " + newSkin.name);
    }


    // Select Button Onclick
    public void SelectSkin()
    {
        SetFinalSkin = true;

        // Destroy Final Skin
        if (Final_Skin != null){ Destroy(Final_Skin); }

        // Select Object : Parent Object For Instantiate
        GameObject SelectPanel = GameObject.Find("Select");

        // Final Skin
        Final_Skin = Instantiate(newSkin, new Vector3(0, -300, 0), Quaternion.identity);
        Final_Skin.transform.SetParent(SelectPanel.transform, false);

        // Set Final Skin Color
        Final_SkinColor = Selected_Color;
        Final_Skin.GetComponent<Image>().color = Final_SkinColor;
    }
}
