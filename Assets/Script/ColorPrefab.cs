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
        Debug.Log(GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Selected_Color);

        // GameObject.Find("ReadyGame").GetComponent<ReadyGame>().Selected_ColorPrefab = gameObject;
    }
}
