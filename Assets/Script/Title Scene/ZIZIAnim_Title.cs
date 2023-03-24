using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ZIZIAnim_Title : MonoBehaviour
{
    // Animation
    public RectTransform Land;
    public GameObject ZIZILand;
    
    public Animator ZIZIcontroller;
    public RectTransform ZIZIParentRect;
    
    public string KEY;
    public List<string> AnimList = new List<string>();

    public RectTransform Collider1P;
    public RectTransform Collider2P;
    public RectTransform ColliderOnline;
    public RectTransform ColliderGameStart;
 
    

    void Start()
    {
        // Animation
        Land = GameObject.Find("Title2").GetComponent<RectTransform>();
        ZIZILand = GameObject.Find("Charactor");

        ZIZIcontroller = GameObject.Find("ZIZI").GetComponent<Animator>(); 
        ZIZIParentRect = GameObject.Find("ZIZI_Point").GetComponent<RectTransform>();
        KEY = "Start";

        Collider1P = GameObject.Find("1P").GetComponent<RectTransform>();
        Collider2P = GameObject.Find("2P").GetComponent<RectTransform>();
        ColliderOnline = GameObject.Find("Online").GetComponent<RectTransform>();
        ColliderGameStart = GameObject.Find("StartGame").GetComponent<RectTransform>();

        StartCoroutine(WaitAnim());           
    }
    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(1f);
        
        AnimList.Add("Up");                             // ZIZI Animation
        ZIZILand.transform.SetSiblingIndex(6);          // Change Layor of ZIZI
        StartCoroutine(WaitMapAnim());                  // Map Animation
    }
    IEnumerator WaitMapAnim()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 30; i++){ yield return new WaitForSeconds(0.02f); Land.localPosition -= new Vector3(0f, 200f / 10, 0f); }
    }

    void Update()
    {
        Check_SelectState(Collider1P);
        Check_SelectState(Collider2P);
        Check_SelectState(ColliderOnline);
        Check_SelectState(ColliderGameStart);

        if (AnimList.Count != 0)
        {
            ZIZIcontroller.SetBool(AnimList[0], true); 
            ZIZIcontroller.SetBool("Empty", true);
            KEY = AnimList[0];
        }
    }

    bool DidYouSelect_PlayerMode = false;
    public void Check_SelectState(RectTransform colRect)
    {
        if(ZIZIParentRect.position.x <= colRect.position.x + 20f 
        && ZIZIParentRect.position.x >= colRect.position.x - 20f 
        && ZIZIParentRect.position.y <= colRect.position.y + 20f  
        && ZIZIParentRect.position.y >= colRect.position.y - 20f)
        {

    // PlayerPrefs : PlayerMode
    // 1 : Single_1P
    // 2 : LocalM_2P
    // 3 : OnlineMulti

            if(colRect.gameObject.name == "1P")
            {
                PlayerPrefs.SetInt("PlayerMode", 1); 
                Debug.Log("1P"); 
                DidYouSelect_PlayerMode = true;
            }          
            else if(colRect.gameObject.name == "2P")
            {
                PlayerPrefs.SetInt("PlayerMode", 2); 
                Debug.Log("2P"); 
                DidYouSelect_PlayerMode = true;
            }
            else if(colRect.gameObject.name == "Online")
            {
                PlayerPrefs.SetInt("PlayerMode", 3); 
                Debug.Log("Online"); 
                DidYouSelect_PlayerMode = true;
            }
            else if(colRect.gameObject.name == "StartGame")
            {
                if(DidYouSelect_PlayerMode == true)
                { 
                    SceneManager.LoadScene("GameScene"); 
                    Debug.Log("SG"); 
                } 
            }
        }
    }


// Animation

    // Animation Operator
    public void Operator(GameObject button)
    {
        if(AnimList.Count < 1)
        {
            //prevent deleayed move
            AnimList.Add(button.name);              
        }
        else
        {

        }
      
    }

    // Animation ++++ End of L, R, U, D
    int index_UpPos = 0;    
    List<float> UpPos = new List<float>(){ 0f, 0.58997f, -25.606f, -43.743f, -49.062f, 3.7431f, 115.57f, 137f, 226.1f, 219f, 192.72f, 176.89f, 165.58f, 157f, 200f };
    public void Go_UP() { 
        Land.localPosition -= new Vector3(0f, 200f / UpPos.Count , 0f); 
        ZIZIParentRect.localPosition += new Vector3(0f, UpPos[index_UpPos + 1] - UpPos[index_UpPos] , 0f); 

        index_UpPos++; 
        if (index_UpPos >= UpPos.Count - 1){ index_UpPos = 0; }
    }   

    int index_DownPos = 0;    
    List<float> DownPos = new List<float>(){ 0f, -35.421f, -25.192f, -23.11f, -23.396f, 0f, 11.7f, -63f, -84.434f, -210.9f, -224f, -222f, -204.4f, -199.41f, -200f };
    public void Go_Down() { 
        Land.localPosition += new Vector3(0f, 200f / DownPos.Count , 0f); 
        ZIZIParentRect.localPosition += new Vector3(0f, DownPos[index_DownPos + 1] - DownPos[index_DownPos] , 0f); 

        index_DownPos++; 
        if (index_DownPos >= DownPos.Count - 1){ index_DownPos = 0; }
        //Debug.Log(DownPos.Count );
    } 

    int index_RightPosX = 0;  
    int index_RightPosY = 0;  
    List<float> RightPosX = new List<float>(){ 0f, -1f, 81.2f, 161.5f, 213.3f, 200f };
    List<float> RightPosY = new List<float>(){ 0f, -5.722f, 61.3f, 64.3f, -3.8744f, 0f };

    public void Go_Right() { 
        Land.localPosition -= new Vector3(200f / RightPosX.Count, 0f, 0f); 
        ZIZIParentRect.localPosition += new Vector3(RightPosX[index_RightPosX + 1] - RightPosX[index_RightPosX], RightPosY[index_RightPosY + 1] - RightPosY[index_RightPosY], 0f); 
        
        index_RightPosX++; 
        index_RightPosY++; 
        if (index_RightPosX >= RightPosX.Count - 1){ index_RightPosX = 0; index_RightPosY = 0; }
        //Debug.Log(RightPosX.Count );
        //Debug.Log(RightPosY.Count );
        
        //Debug.Log("R : " + ZIZIParentRect.localPosition + " " + index_RightPosX + " " + index_RightPosY);
    } 

    int index_LeftPosX = 0;    
    int index_LeftPosY = 0;
    List<float> LeftPosX = new List<float>(){ 0f, -1f, -81.2f, -161.5f, -213.3f, -200f };
    List<float> LeftPosY = new List<float>(){ 0f, -5.722f, 61.3f, 64.3f, -3.8744f, 0f };

    public void Go_Left() { 
        Land.localPosition += new Vector3(200f / LeftPosX.Count, 0f, 0f); 
        ZIZIParentRect.localPosition += new Vector3(LeftPosX[index_LeftPosX + 1] - LeftPosX[index_LeftPosX], LeftPosY[index_LeftPosY + 1] - LeftPosY[index_LeftPosY], 0f); 
        
        index_LeftPosX++; 
        index_LeftPosY++; 
        if (index_LeftPosX >= LeftPosX.Count - 1){ index_LeftPosX = 0; index_LeftPosY = 0; }
        //Debug.Log("L : " + ZIZIParentRect.localPosition + " " + index_LeftPosX + " " + index_LeftPosY);
    } 
    
    public void Stop() // End of Stop
    {
        ZIZIcontroller.SetBool(KEY, false);
        AnimList.RemoveAt(0);
    }
}
