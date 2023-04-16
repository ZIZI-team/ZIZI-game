using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    GameObject Game;

    void Start()
    {
        Game = GameObject.Find("Game");   
    }

    public void OnClickSlotItem_Func()
    {
        Game.GetComponent<GameSceneSystem>().OnClickSlotItem(gameObject);
    }
  
    public void SkillChiso_Func()
    {
        Game.GetComponent<GameSceneSystem>().SkillChiso(gameObject);
    }

}
