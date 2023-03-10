using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBushSpawnSystem : MonoBehaviour
{

    public GameObject GameSystem;
    int yAxis = GameSystem.Getcomponent<GameSceneSystem>().mapGridNum_y + 8;
    int xAxis = GameSystem.Getcomponent<GameSceneSystem>().mapGridNum_y + 8;
    public int[,] BushBoard = new int[15+8, 15+8];

    void bushVersionOne()
    {

        for (int i = 0; i < (yAxis / 2); i++)
        {
            for (int j = 0; j < (xAxis / 2); j++)
            {
                BushBoard[i, j] = 5;
            }
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
                for (int i = 0; i < yAxis; i++)
        {
            for (int j = 0; j < xAxis; j++)
            {
                BushBoard[i, j] = 0;
            }
        }
        bushVersionOne();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
