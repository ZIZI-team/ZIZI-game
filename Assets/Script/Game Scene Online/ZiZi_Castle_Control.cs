using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiZi_castle : MonoBehaviour
{

    public GameObject ziziCastle;
    public GameObject accessPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "online_zizi")
        {
            accessPanel.transform.position = new Vector3(0,0,0);
            
        }
    }
    
}
