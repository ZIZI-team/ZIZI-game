using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGame : MonoBehaviour
{


    // SKIN_ZIZI 70 , 35
    // ColorPrefab 25



    // +++ SKIN +++ //

    // Skin Prefab : SkinName(Str) + SkinImg(Img) + Color(Script) + Anim(Script) + Skill(Script)
    private List<GameObject> MySkin = new List<GameObject>();
    public int MySkinIndex = 0;

    public GameObject SkinPalette;
    public GameObject newSkin;

    // : Script
    public string script_name = "SKIN_ZIZI";                            // 다시 생각해보기

    // : Color
    public int Color_List_Count = 0;
    public Color Selected_Color;
    
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

        // Start SKIN Prefab
        newSkin = Instantiate(MySkin[MySkinIndex], new Vector3(0, 0, 0), Quaternion.identity);
        newSkin.transform.SetParent(SkinPalette.transform, false);
        SearchSkin(MySkinIndex);     
    }

    void Update()
    {

    }

    public void SkinIndexUp()
    {
        Destroy(newSkin);
        if (MySkinIndex == Color_List_Count - 1){ MySkinIndex = 0; }
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
}
