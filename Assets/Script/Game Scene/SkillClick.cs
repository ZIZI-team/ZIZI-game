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

    bool Skill_Flag = false;
    public void OnClickItem()
    {
        if (Skill_Flag == true){ return; }

        GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSkill(gameObject);

        Skill_Flag = true;
        StartCoroutine(DelayCoroutine());
    }
    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Skill_Flag = false;
    }
}
