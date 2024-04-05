using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    private static TitleUIManager instance = null;

    public static TitleUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TitleUIManager();
            }
            return instance;
        }
    }

    public void ChangetUI(GameObject unActive, GameObject Active)
    {
        unActive.SetActive(false);
        Active.SetActive(true);
    }
}
