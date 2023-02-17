using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKIN_temp1 : MonoBehaviour
{
    // : Test
    // public Sprite Image;
    // public int num = 1;

    // : Color
    public GameObject ColorPalette;
    public GameObject ColorPrefab;

    public List<Color> Color_List = new List<Color>()
    {
        new Color(0f, 1F, 1f, 0.5f),     //Blue
        new Color(1f, 0f, 0f, 0.5f),     //Red
        new Color(1f, 1f, 0.2f, 0.5f)   //Yellow        
    };

    // : Anim
    public List<Animation> Animation_List = new List<Animation>();

    // : Skill
    public void Skill(){
        return;
    }
    
    void Start()
    {
        ColorPalette = GameObject.Find("Color");
        ColorPrefab = Resources.Load<GameObject>("ColorPrefab");

        // Make SKIN Color Palette
        for (int i = 0; i < Color_List.Count; i++)
        {
            GameObject newColor = Instantiate(ColorPrefab, new Vector3(i*200, 0, 0), Quaternion.identity);
            newColor.GetComponent<Image>().color = Color_List[i];
            newColor.transform.SetParent(ColorPalette.transform, false);
        }
    }

    void Update()
    {
        
    }
}
