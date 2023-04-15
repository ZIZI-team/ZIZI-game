using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClick : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickItem()
    {
        Debug.Log(gameObject.name);

        GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSlotItem();
    }
  
    public void OnClickChiso()
    {
        GameObject.Find("Game").GetComponent<GameSceneSystem>().SkillChiso();
    }

}
