using TMPro;
using UnityEngine;

/// <summary>
/// 이 타이머를 초과하면 상대방으로 턴이 넘어갑니다.
/// 기본적으로 60초입니다.
/// DataManager에 있는 gamedata.timertime에서 가져오며
/// Timer로 작동하게 될 Timer Text에 부착됩니다.
/// </summary>
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

        //timer가 0일시 턴 변경
        if (timer <= 0)
        {
            GameSystem.Instance.changeTurn();
        }

    }


}
