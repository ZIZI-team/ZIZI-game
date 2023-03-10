using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ZIZIAnim : MonoBehaviour
{
    // Animation
    public GameObject ZIZI_Parent;
    public GameObject ZIZI;
    public Animator ZIZIcontroller;

    public RectTransform ZIZIParentRect;
    public RectTransform ZIZIRect;

    public string KEY;

    void Start()
    {
        // Animation
        ZIZI_Parent = GameObject.Find("ZIZI_Point");
        ZIZI = GameObject.Find("ZIZI");
        ZIZIcontroller = ZIZI.GetComponent<Animator>(); 
        ZIZIParentRect = ZIZI_Parent.GetComponent<RectTransform>();
        ZIZIRect = ZIZI.GetComponent<RectTransform>();
        KEY = "Start";
    }
    
    // Animation Operator
    public void Operator(GameObject button)
    {
        ZIZIcontroller.SetBool(button.name, true); 
        ZIZIcontroller.SetBool("Empty", true);
        KEY = button.name;
    }

    public void OffAnim() // End of L, R, U, D
    { 
        ZIZIRect.localPosition = new Vector3(0f, 0f, 0f);  
        ZIZIcontroller.SetBool("Empty", false);
        Debug.Log("OffAnim");
    }

    // End of L, R, U, D
    public void Go_UP() { ZIZIParentRect.localPosition += new Vector3(0f, 200f, 0f);  Debug.Log("Up"); }
    
    public void Go_Down() { ZIZIParentRect.localPosition += new Vector3(0f, -200f, 0f);  Debug.Log("Down"); }
    
    public void Go_Right() { ZIZIParentRect.localPosition += new Vector3(200f, 0f, 0f);  Debug.Log("Right"); }
    
    public void Go_Left() { ZIZIParentRect.localPosition += new Vector3(-200f, 0f, 0f);  Debug.Log("Left"); }


    public void Stop() // End of Stop
    {
        Debug.Log("Stop");
        ZIZIcontroller.SetBool(KEY, false);
    }

    public void Select()
    {

    }


    // StartCoroutine(WaitAnim());        
    // IEnumerator WaitAnim()
    // {
    //     yield return new WaitForSeconds(0.5f);
    // }

}
