using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKIN_ZIZI : MonoBehaviour
{
    // : Test
    // public Sprite Image;
    // public int num = 1;

    // : Color
    public GameObject ColorPalette;
    public GameObject ColorPrefab;
    public List<GameObject> ColorPalette_Object = new List<GameObject>();

    public List<Color> Color_List = new List<Color>()
    {
        new Color(1f, 0f, 0f, 0.5f),     //Red
        new Color(1f, 1f, 0.2f, 0.5f),   //Yellow
        new Color(0f, 1f, 0f, 0.5f),     
        new Color(1f, 0f, 1f, 0.5f)  
    };

    // : Anim
    public List<Animation> Animation_List = new List<Animation>();

    // : Skill
    public void Skill()
    {
        return;
    }
    
    void Start()
    {
        if (GameObject.Find("ReadyGame").GetComponent<ReadyGame>().SetFinalSkin == false)
        {
            ColorPalette = GameObject.Find("Color");
            ColorPrefab = Resources.Load<GameObject>("ColorPrefab");

            int ColorMaxIndex = GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Color_List_Count;

            // Delete Color
            if (ColorMaxIndex != 0)
            {
                Transform[] childList = ColorPalette.GetComponentsInChildren<Transform>();
                for (int i = 0; i < ColorMaxIndex; i++){ Destroy(childList[i+2].gameObject); }
            }

            GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Color_List_Count = Color_List.Count;
            
            // Make SKIN Color Palette
            for (int i = 0; i < Color_List.Count; i++)
            {
                ColorPalette_Object.Add(Instantiate(ColorPrefab, new Vector3(i*200, 0, 0), Quaternion.identity));
                ColorPalette_Object[i].GetComponent<Image>().color = Color_List[i];
                ColorPalette_Object[i].transform.SetParent(ColorPalette.transform, false);
            }

            // Set initiate Color of Skin
            GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Selected_Color = Color_List[0];
            GameObject.Find("ReadyGame").GetComponent<ReadyGame>().ChangeSkinColor();
        }

        GameObject.Find("ReadyGame").GetComponent<ReadyGame>().SetFinalSkin = false;
    }

    void Update()
    {
        
    }
}