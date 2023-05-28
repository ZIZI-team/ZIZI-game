using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public GameObject AudioManagerSRC;

    public List<GameObject> SceneList = new List<GameObject>();
    int ListIndex = 0;

    public GameObject Main;
    void Start()
    {
        AudioManagerSRC = GameObject.FindWithTag("Music");

        Main = GameObject.Find("Main");

        // Cut scene 임시 보류 : 축제용
        SkipCutScene();

        if (PlayerPrefs.GetInt("ShowTutorial") == 1)
        {
            SkipCutScene();
        }
        
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(0).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(1).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(2).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(3).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(4).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(5).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(6).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(7).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(8).gameObject);
    }

    public Animator controller;                            // Unity : Inspector
    public void Fade()
    {
        controller.SetBool("Fade", true);
        controller.SetBool("Fade", false);
    }

    public void NextCut()
    {
        if (ListIndex == SceneList.Count - 1)
        {
            // controller.SetBool("Fade", false);
            SkipCutScene();
            return;
        }
        SceneList[ListIndex++].SetActive(false);
        SceneList[ListIndex].SetActive(true);
    }

    public void SkipCutScene()
    {
        AudioManagerSRC.GetComponent<AudioManager>().SFX3();

        controller.SetBool("Fade", true);
        Main.transform.GetChild(0).gameObject.SetActive(false); // CutScene Canvas
        Main.transform.GetChild(1).gameObject.SetActive(true);  // Tutorial Canvas
    }

    void Update()
    {
        
    }
}
