using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using Firebase.Auth;

public class LogInSystem : MonoBehaviour
{
    [Header("Sign Up Input Field")]
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_InputField userid;
    public TMP_InputField username;

    [Header("Login Input Field")]
    public TMP_InputField loginId;
    public TMP_InputField loginPassword;

    [Header("Message Text")]
    public GameObject OutputPanel;
    public TMP_Text OutputText;


    void Start()
    {
        FirebaseAuthManager.Instance.Init();

        FirebaseDataBaseManager.Instane.Init();
    }

    public void Create()
    {

        AudioManager.Instance.SFX3();
        FirebaseAuthManager.Instance.SingUp(signupEmail.text, signupPassword.text);

        //name, ID save to FirebaseDataBase
        //�߰� �Ǿ�� �� �κ��� if������ ��� FirebaseUser user �� != �϶� ���ǹ��� �ֱ�
        FirebaseDataBaseManager.Instane.SaveUserDataFn(userid.text, username.text, signupEmail.text);

    }

    public void Login()
    {
        AudioManager.Instance.SFX3();
        StartCoroutine(FirebaseDataBaseManager.Instane.LoadUserDataCoroutine(loginId.text, "email", result => 
        {
            Debug.Log(result);
            if(result == null)
            {
                OutputPanel.SetActive(true);
                OutputText.text = "Faulted Login";
            }
            else{ FirebaseAuthManager.Instance.Login(result, loginPassword.text); }
        }   
        ));
    }

    public void Logout()
    {
        FirebaseAuthManager.Instance.Logout();
    }

    
}

