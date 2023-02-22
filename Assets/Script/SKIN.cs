using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class SKIN : MonoBehaviour
{
    // : Test
    // public Sprite Image;
    // public int num = 1;

    // : Color
    // public GameObject ColorPalette;
    // public GameObject ColorPrefab;


    public List<Color> Color_List = new List<Color>();

    // : Anim
    public List<Animation> Animation_List = new List<Animation>();

    // : Skill
    public void Skill()
    {
        return;
    }
    
    void Start()
    {

        // Send Color List information to GameReady Hub
        // GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Color_List_Hub = new List<Color>();
        // GameObject.Find("ReadyGame").GetComponent<GameReadyHub>().Color_List_Hub = Color_List.ToList();


    }

    void Update()
    {
        
    }
}
