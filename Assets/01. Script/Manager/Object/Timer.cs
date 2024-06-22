using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer;

    void Start()
    {
        timer = DataManager.Instance.gamedata.timertime;
    }

    void Update()
    {
        if ((int)DataManager.Instance.gamedata.timertime == 60)
        {
            timer = DataManager.Instance.gamedata.timertime;
        }

        timer -= Time.deltaTime;
        GetComponent<TMP_Text>().text = timer.ToString("F0");

        DataManager.Instance.gamedata.timertime = timer;

        if (timer <= 0)
        {
            GameSystem.Instance.changeTurn();
        }

    }


}
