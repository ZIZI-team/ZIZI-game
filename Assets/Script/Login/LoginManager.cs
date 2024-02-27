using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    
    
    void Start()
    {   
        //not login
        PlayerPrefs.SetInt("IsLogin",0);
        //login
        PlayerPrefs.SetInt("IsLogin", 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
