using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    void Start()
    {
        // button1 = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject;
        // button2 = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).gameObject;
        // button3 = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(2).gameObject;

        pressedSprite1 = Resources.Load<Sprite>("Title Flag/1P");
        pressedSprite2 = Resources.Load<Sprite>("Title Flag/2P");
        pressedSprite3 = Resources.Load<Sprite>("Title Flag/online");

        disabledSprite1 = Resources.Load<Sprite>("Title Flag/1P L");
        disabledSprite2 = Resources.Load<Sprite>("Title Flag/2P L");
        disabledSprite3 = Resources.Load<Sprite>("Title Flag/online L");

        if (PlayerPrefs.GetInt("GameScene") == 1)
        {
            Select_StartGame();
        } 
    }

    public Animator controller;                            // Unity : Inspector
    public void Select_StartGame()
    {
        if (PlayerPrefs.GetInt("GameScene") == 0)
        {
            controller.SetBool("FadeOut", true);
        }

        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
    }

    public void BacktoTitle()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
    }

        // PlayerPrefs : PlayerMode
        // 1 : Single_1P
        // 2 : LocalM_2P
        // 3 : OnlineMulti

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;   
	
    public Sprite pressedSprite1;
    public Sprite pressedSprite2;
    public Sprite pressedSprite3;

    public Sprite disabledSprite1;
    public Sprite disabledSprite2;
    public Sprite disabledSprite3;

    
    #region PlayerMode1() // No use

        public void PlayerMode1() // Onclick Button 1P Local
        {
            // button1.GetComponent<Image>().sprite = pressedSprite1;
            button1.transform.GetChild(0).gameObject.SetActive(true);

            // button2.GetComponent<Image>().sprite = disabledSprite2;
            button2.transform.GetChild(0).gameObject.SetActive(false);

            // button3.GetComponent<Image>().sprite = disabledSprite3;
            button3.transform.GetChild(0).gameObject.SetActive(false);

            PlayerPrefs.SetInt("PlayerMode", 1); 
        }

    #endregion

    // Local
        public void Local2P() // Onclick Button 2P Local
        {
            // // button2.GetComponent<Image>().sprite = pressedSprite2;
            // button2.transform.GetChild(0).gameObject.SetActive(true);

            // // button1.GetComponent<Image>().sprite = disabledSprite1;
            // button1.transform.GetChild(0).gameObject.SetActive(false);

            // // button3.GetComponent<Image>().sprite = disabledSprite3;
            // button3.transform.GetChild(0).gameObject.SetActive(false);

            PlayerPrefs.SetInt("PlayerMode", 2); 
            SceneManager.LoadScene("GameScene"); 
        }

    // Online
        public void Online()  // Onclick Button Online
        {        
            // // button3.GetComponent<Image>().sprite = pressedSprite3;
            // button3.transform.GetChild(0).gameObject.SetActive(true);

            // // button2.GetComponent<Image>().sprite = disabledSprite2;
            // button2.transform.GetChild(0).gameObject.SetActive(false);

            // // button1.GetComponent<Image>().sprite = disabledSprite1;
            // button1.transform.GetChild(0).gameObject.SetActive(false);

            PlayerPrefs.SetInt("PlayerMode", 3); 
            SceneManager.LoadScene("Online"); 
        }

    public void StartFlag()
    {
        
    }

    public GameObject SettingPanel; // inspector

    public void Settings()
    {
        SettingPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingPanel.SetActive(false);
    }


    public void ShowTutorial()
    {
        PlayerPrefs.SetInt("ShowTutorial", 1);
        SceneManager.LoadScene("Tutorial");        
    }



    void Update()
    {
        
    }
}
