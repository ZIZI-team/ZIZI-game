using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public List<GameObject> SceneList = new List<GameObject>();
    int ListIndex = 0;

    public GameObject Main;
    void Start()
    {
        Main = GameObject.Find("Main");
        
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(0).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(1).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(2).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(3).gameObject);
        SceneList.Add(Main.transform.GetChild(0).transform.GetChild(4).gameObject);
    }

    public void NextCut()
    {
        if (ListIndex == SceneList.Count - 1)
        {
            SkipCutScene();
            return;
        }
        SceneList[ListIndex++].SetActive(false);
        SceneList[ListIndex].SetActive(true);
    }

    public void SkipCutScene()
    {
        Main.transform.GetChild(0).gameObject.SetActive(false); // CutScene Canvas
        Main.transform.GetChild(1).gameObject.SetActive(true);  // Tutorial Canvas
    }

    void Update()
    {
        
    }
}