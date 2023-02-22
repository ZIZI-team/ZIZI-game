using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameReadyHub : MonoBehaviour
{    
    void Start()
    {
        // PlayerPrefs : PlayerMode
        // 1 : Single_1P
        // 2 : LocalM_2P
        // 3 : OnlineMulti

        // PlayerPrefs : GameMode
        // 1 : Original
        // 2 : Classic
        // 3 : Dynamic
        
        switch(PlayerPrefs.GetInt("PlayerMode")){
            case 1 :
                Destroy(GameObject.Find("ReadyGame").GetComponent<ReadyGame_Local2P>());
                break;
            case 2 :
                // Destroy(GameObject.Find("ReadyGame").GetComponent<ReadyGame_Local1P>());
                break;
            case 3 :
                // Add Online script 
                break;
        }

    }


    public GameObject Final_Skin_1P;                       // ReadyGame_Local1P
    public GameObject Final_Skin_2P;                       // ReadyGame_Local2P


    void Update()
    {

    }
}
