using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Online_Player_Control : MonoBehaviour
{
    public GameObject ziziCastle;
    public GameObject ziziStore;
    public GameObject accessPanel;

    public Text warningInfo;


    void Awake()
    {
        ziziCastle = GameObject.Find("Castle");
        ziziStore = GameObject.Find("zizi_store");
        accessPanel = GameObject.Find("Castle_panel");

        warningInfo = GameObject.Find("Warning_txt").GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        accessPanel.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "zizi_castle")
        {
            accessPanel.SetActive(true);
            warningInfo.text = "You are not ready master ZIZI";
        }
        else if (other.transform.tag == "online_dummy")
        {
            accessPanel.SetActive(true);
            warningInfo.text = "Do you know Jazz?";
        }
        else if (other.transform.tag == "store_zizi")
        {
            accessPanel.SetActive(true);
            warningInfo.text = "The store open at someday, but nor today";
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "zizi_castle")
        {
            accessPanel.SetActive(false);
        }
        else if (other.transform.tag == "online_dummy")
        {
            accessPanel.SetActive(false);
        }
        else if (other.transform.tag == "store_zizi")
        {
            accessPanel.SetActive(false);
        }
    }
}
