using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class SKIN : MonoBehaviour
{
    // PlayerPrefs : GameMode
    // 1 : Original
    // 2 : Classic
    // 3 : Dynamic

// ------------------------------------------------------------------------ //

    // +++ Color +++ //
    public List<Color> Color_List = new List<Color>();


    // +++ Animation +++ //
    // public List<Animator> Animation_List = new List<Animator>();

    // public Animator controller;         // Start : GetComponent


    // +++ SKILL +++ //    
    public void Skill()
    {
        return;
    }
    
    void Start()
    {
        // // Default Animation
        // if (PlayerPrefs.GetInt("GameMode") == 1 || PlayerPrefs.GetInt("GameMode") == 3)     // Original, Dynamic
        // {
        //     controller = gameObject.GetComponent<Animator>();
        //     controller.runtimeAnimatorController = Resources.Load("SKIN_Anim/"+"Controller_Hop1") as RuntimeAnimatorController; 
        // }
    }

    void Update()
    {
        
    }


    // Animation을 리스트로 관리하는 것을 만들고 싶음


}
