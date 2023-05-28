using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorPrefab : MonoBehaviour
{
    public GameObject AudioManagerSRC;

    void Start()
    {
        AudioManagerSRC = GameObject.FindWithTag("Music");
    }

    void Update()
    {
        
    }

    // color Prefab Onclick
    public void ColorPrefab_Onclick()
    {
        AudioManagerSRC.GetComponent<AudioManager>().SFX3();
        
        if ( gameObject.transform.parent.name == "1PColor")
        {
            GameObject.Find("1PScript").GetComponent<ReadyGame_Local1P>().SelectColor_prefab(this.gameObject);
        }
        else if ( gameObject.transform.parent.name == "2PColor")
        {
            GameObject.Find("2PScript").GetComponent<ReadyGame_Local2P>().SelectColor_prefab(this.gameObject);
        }
    }
}
