using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;


public class FirebaseAuthManager 
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new FirebaseAuthManager();
            }

            return instance;
        }
    }

    private FirebaseAuth auth;
    private FirebaseUser user;

    //public string UserId => user.UserId;

    //public Action<bool> LoginState;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        if(auth.CurrentUser != null)
        {
            Logout();
        }

        auth.StateChanged += OnChanged;
    }   
    
    private void OnChanged(object sender, EventArgs e)
    {
        if(auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if(!signed && user != null)
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

    public void SingUp(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => { 

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
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void Logout()
    {
        auth.SignOut();
    }



}
