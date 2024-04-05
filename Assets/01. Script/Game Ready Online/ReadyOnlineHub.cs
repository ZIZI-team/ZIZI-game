using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyOnlineHub : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Back2Title()
    {
        PlayerPrefs.SetInt("GameScene", 1);
        SceneManager.LoadScene("TitleScene");
    }

    public GameObject CashMarket;
    public void OpenCashMarket()
    {
        CashMarket.SetActive(true);
    }

    public void CloseCashMarket()
    {
        CashMarket.SetActive(false);
    }

    public GameObject SkinInventory;
    public void OpenSkinInventory()
    {
        SkinInventory.SetActive(true);
    }

    public void CloseSkinInventory()
    {
        SkinInventory.SetActive(false);
    }

    public GameObject ItemInventory;
    public void OpenItemInventory()
    {
        ItemInventory.SetActive(true);
    }

    public void CloseItemInventory()
    {
        ItemInventory.SetActive(false);
    }

    public void GoMap_ZIZICastle()
    {
        SceneManager.LoadScene("Map ZIZICastle");    
    }

    public void GoMap_Sea()
    {
        SceneManager.LoadScene("Map Sea");    
    }

}
