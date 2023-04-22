using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackOnlineHub : MonoBehaviour
{
    public void GoBackHub()
    {
        SceneManager.LoadScene("Online");
    }
}
