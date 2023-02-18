using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorPrefab : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // color Prefab Onclick
    public void ColorPrefab_Onclick()
    {
        GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Selected_Color = gameObject.GetComponent<Image>().color;
        GameObject.Find("ReadyGame").GetComponent<ReadyGame>().ChangeSkinColor();
        
        // Test
        Debug.Log(GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Selected_Color);
    }
}
