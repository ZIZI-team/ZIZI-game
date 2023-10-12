using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGameSceneSystem : MonoBehaviour
// IDragHandler, IPointerUpHandler,IPointerUpHandler
{
    [Header("GameObject")]
    public GameObject OnlineScene;
    public GameObject zizi_player;
    public GameObject zizi_dummy;

    public GameObject Background;
    public float speed = 5.0f;
    public GameObject Camera;

    public GameObject ziziCastle;
    public GameObject accessPanel;
    public GameObject onlinePlayer;


     
    // Start is called before the first frame update

    void Start()
    {

        zizi_player = Instantiate(Resources.Load("SKIN_Prefab_Online/zizi_Kingdom/" + "Online_Player"), new Vector3(0,0,-0.01f), this.transform.rotation) as GameObject;
        zizi_dummy = Instantiate(Resources.Load("SKIN_Prefab_Online/zizi_Kingdom/" + "Online_dummy"), new Vector3(0,0,-0.01f), this.transform.rotation) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Camera.transform.position = zizi_player.transform.position + new Vector3( 0, 0, -200f);
        Move();

        
    }
    void Move()
    {

        


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            zizi_player.transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            zizi_player.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            zizi_player.transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            zizi_player.transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }

}
