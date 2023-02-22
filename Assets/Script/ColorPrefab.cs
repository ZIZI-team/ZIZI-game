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
        if ( gameObject.transform.parent.name == "1PColor")
        {
            GameObject.Find("ReadyGame").GetComponent<ReadyGame_Local1P>().SelectColor_prefab(this.gameObject);
        }
        else if ( gameObject.transform.parent.name == "2PColor")
        {
            GameObject.Find("ReadyGame").GetComponent<ReadyGame_Local2P>().SelectColor_prefab(this.gameObject);
        }
    }
}
