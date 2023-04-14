using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniviaLogo : MonoBehaviour
{
    void Start()
    {
        // Remove Juseok before Build
        PlayerPrefs.SetInt("TutorialPlayed", 0); 
    }

    void ZIZI()
    {
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Univia").SetActive(false);
    }

    void StartGame()
    {
        if (PlayerPrefs.GetInt("TutorialPlayed", 0) == 0){ SceneManager.LoadScene("Tutorial"); }
        else if (PlayerPrefs.GetInt("TutorialPlayed", 0) == 1){ SceneManager.LoadScene("TitleScene"); }
    }
}
