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
        GameObject.Find("Game").GetComponent<GameSceneSystem>().OnClickSkill(gameObject);
    }
}
