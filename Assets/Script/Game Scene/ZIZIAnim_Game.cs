using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Script Located in skin prefab

// Normal Animation
public class ZIZIAnim_Game : MonoBehaviour
{
    public Animator controller;      
    string KEY = "Anim1";

    void Start()
    {
        // Default Animation : Anim1
        controller = gameObject.GetComponent<Animator>();
        controller.runtimeAnimatorController = Resources.Load("ZIZI_Anim/"+"ZIZI_Anim") as RuntimeAnimatorController; 

        
        // if (PlayerPrefs.GetInt("GameMode") == 1 || PlayerPrefs.GetInt("GameMode") == 3)     // Original, Dynamic
        // {
        //     // controller = gameObject.GetComponent<Animator>();
        //     controller.SetBool("Anim1", true);
        // }
    }

    public void AnimT(string AnimName)
    {
        // AnimName
            // Anim1 : Hop
            // Anim2 : Nervous
            // Anim3 : Dance
            // Anim4 : Win

        controller.SetBool("Change", true);
        controller.SetBool(KEY, false);
        controller.SetBool(AnimName, true);
        controller.SetBool("Change", false);
        KEY = AnimName;
    }

    void Update()
    {
        
    }

    




}
