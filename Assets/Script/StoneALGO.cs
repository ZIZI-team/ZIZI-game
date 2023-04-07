// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class StoneALGGO : MonoBehaviour
// {
//     public GameObject Line;

//     //public GameSystem GameSystem;

//     float[] x_dir = new float[3];
//     float[] y_dir = new float[3];

//     public GameSystem GameSystem;


//     int x, y;
//     int BoardX, BoardY;

//     int color_code;

//     public int[,] stoneBoard = new int[11,11];

//     void Start()
//     {
//         if(this.gameObject.name.Contains("Player1"))
//         {
//             color_code = 1;
//         }
//         else if(this.gameObject.name.Contains("Player2"))
//         {
//             color_code = 2;
//         }
//         else
//         {

//         }

//         Debug.Log(color_code);

//         GameSystem = GameObject.Find("EventSystem").GetComponent<GameSceneSystem>();

//         for (int i = 0; i < stoneBoard.Length; i++)
//         {
//             for (int j = 0; j < stoneBoard.Length; j++)
//             {
//                 stoneBoard[i][j] = GameSystem.ZIZIBoard[i + 6][j + 6];
//             }
//         }

//         x = (int) Math.Truncate(this.transform.position.x / 3.5) + GameSystem.Correction(this.transform.position.x);
//         y = (int) Math.Truncate(this.transform.position.y / 3.5) + GameSystem.Correction(this.transform.position.y);

//         BoardX = x + 7;
//         BoardY = -y + 7;

//         Debug.Log($"In stome Action {BoardX}, {BoardY}");

//         threecheck();

//     }

       


//     public void threecheck()
//     {

//         if(this.gameObject.name.Contains("Player1"))
//         {
//             color_code = 1;
//         }
//         else if(this.gameObject.name.Contains("Player2"))
//         {
//             color_code = 2;
//         }
//         else
//         {

//         }



//         if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY + 1][BoardX] == color_code && game_board[BoardY + 1][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY - 1][BoardX] == color_code && game_board[BoardY - 2][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY - 1][BoardX + 1] == color_code && game_board[BoardY - 2][BoardX + 2] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY + 1][BoardX] == color_code && game_board[BoardY + 1][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY + 1][BoardX + 1] == color_code && game_board[BoardY + 2][BoardX + 2] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY - 1][BoardX - 1] == color_code && game_board[BoardY - 2][BoardX - 2] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code && game_board[BoardY + 1][BoardX - 1] == color_code && game_board[BoardY + 2][BoardX - 2] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX - 1] == color_code && game_board[BoardY + 1][BoardX] == color_code && game_board[BoardY + 2][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX - 1] == color_code && game_board[BoardY - 1][BoardX] == color_code && game_board[BoardY - 2][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX - 1] == color_code && game_board[BoardY - 1][BoardX] == color_code && game_board[BoardY + 1][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY][BoardX - 1] == color_code && game_board[BoardY][BoardX - 2] == color_code && game_board[BoardY + 1][BoardX] == color_code && game_board[BoardY - 1][BoardX] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//         else if(game_board[BoardY][BoardX] == color_code && game_board[BoardY - 1][BoardX] == color_code && game_board[BoardY + 1][BoardX] == color_code && game_board[BoardY][BoardX + 1] == color_code && game_board[BoardY][BoardX + 2] == color_code)
//         {
//             Destroy(this.gameObject);
//             game_board[BoardY][BoardX] = 0;
//             GameSystem.PlayerChange();
//         }
//     }

//     void threethreechecker()
//     {

//     }
// }