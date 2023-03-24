using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClick : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject clickSkill;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickItem()
    {
        GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSkill();
    }

    // public void OnClickSkill()
    // {
        
    //     if (isBlack == true )
    //     {
    //         foreach (GameObject item in P2_Item)
    //         {
    //             item.GetComponent<Button>().interactable = false;
    //         }
    //         foreach (GameObject item in P1_Item)
    //         {
    //             item.GetComponent<Button>().interactable = true;
    //         }
    //     }
    //     else
    //     {
    //         foreach (GameObject item in P1_Item)
    //         {
    //             item.GetComponent<Button>().interactable = false;
    //         }
    //         foreach (GameObject item in P2_Item)
    //         {
    //             item.GetComponent<Button>().interactable = true;
    //         }
    //     }
    
    // }
}
