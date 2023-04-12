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

        GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSkill(gameObject);
    }
  
    public void OnClickChiso()
    {
        GameObject.Find("Game").GetComponent<GameSceneSystem>().SkillChiso();
    }


    // bool Skill_Flag = false;
    // public void OnClickItem()
    // {
    //     if (Skill_Flag == true){ return; }

    //     GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSkill(gameObject);

    //     Skill_Flag = true;
    //     StartCoroutine(DelayCoroutine());
    // }
    // IEnumerator DelayCoroutine()
    // {
    //     yield return new WaitForSeconds(0.5f);

    //     Skill_Flag = false;
    // }
}
