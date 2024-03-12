using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using Firebase.Auth;

public class AuthManager : MonoBehaviour
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

    private FirebaseAuth auth;
    private FirebaseUser user;
    

    void Start()
    {
        FirebaseDataBaseManager.Instane.Init();

        auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser != null)
        {
            Logout();
        }

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
            }

            user = auth.CurrentUser;

            if (signed)
            {
                Debug.Log("로그인");
            }
        }
    }

    public void SingUp()
    {
        AudioManager.Instance.SFX3();
        auth.CreateUserWithEmailAndPasswordAsync(signupEmail.text, signupPassword.text).ContinueWith(task => {

            if (task.IsCanceled)
            {
                Debug.Log("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase was created successfully: {0}, ({1})",
                result.User.DisplayName, result.User.UserId);
            FirebaseDataBaseManager.Instane.SaveUserDataFn(userid.text, username.text, signupEmail.text);
        });
    }

    public void Login()
    {
        AudioManager.Instance.SFX3();
        StartCoroutine(FirebaseDataBaseManager.Instane.LoadUserDataCoroutine(loginId.text, "email", result =>
        {
            Debug.Log(result);
            if (result == null)
            {
                OutputPanel.SetActive(true);
                OutputText.text = "Not Find user ID: " + loginId.text;
            }
            else
            {
                auth.SignInWithEmailAndPasswordAsync(result, loginPassword.text)
            .ContinueWith(task => {
                // 작업이 취소되었는지 확인
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                // 작업이 실패했는지 확인
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // 로그인 성공한 경우
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });
            }
        }));
    }

    public void Logout()
    {
        auth.SignOut();
    }

}
