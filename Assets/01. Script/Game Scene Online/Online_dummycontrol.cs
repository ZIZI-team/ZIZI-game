using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Online_dummycontrol : MonoBehaviour
{

    Vector3[] destination = new Vector3[3];
    bool isMoving = false;
    int next = 0;
    float speed = 1.0f;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //randomPos();

        targetPos = new Vector3(Random.Range( -2.5f, 2.0f), Random.Range(-20.0f, 3.5f), -0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        dummyMove();

        Debug.Log(targetPos);
    }
    

    // void randomPos()
    // {
    //     for (int i = 0; i < 3; i++)
    //     {
    //         destination[i] = new Vector3(Random.Range( -2.5f, 2.0f, 0f), Random.Range(-20.0f, 3.5f, 0f), 0f);
    //     }
    //     isMoving = true;
    // }

    void dummyMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        if (Vector2.Distance((Vector2)transform.position, (Vector2)targetPos) == 0)
        {
            targetPos = new Vector3(Random.Range( -2.5f, 2.0f), Random.Range(-20.0f, 3.5f), -0.01f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "zizi_castle")
        {
            targetPos = new Vector3(Random.Range( -2.5f, 2.0f), Random.Range(-20.0f, 3.5f), -0.01f);
        }
        
        else if (other.transform.tag == "store_zizi")
        {
            targetPos = new Vector3(Random.Range( -2.5f, 2.0f), Random.Range(-20.0f, 3.5f), -0.01f);
        }

    }
}
