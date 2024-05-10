using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    public IEnumerator ChangeSceneToA(string sceneName, int delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator goToOnlineGameScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOnline");
        yield return new WaitForSeconds(0.25f);
        GameManager.Instance.SetGame();
    }
}   
