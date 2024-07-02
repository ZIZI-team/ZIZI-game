using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Managers을 관리하는 Manager.
/// Hierachy창을 깔끔하게 하기 위함.
/// System에 부착되어있으며 자식들로 여러 Manager들을 가지게 됨.
/// </summary>
public class SystemManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
