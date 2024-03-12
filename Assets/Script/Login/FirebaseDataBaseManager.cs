using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class UserData
{
    public string name;
    public string email;
    public string sginupdata;

    public UserData(string name, string email, string sginupdata)
    {
        this.name = name;
        this.email = email;
        this.sginupdata = sginupdata;
    }
}
public class FirebaseDataBaseManager
{
    DatabaseReference dbRef;
    public UserData ud;

    private static FirebaseDataBaseManager instane = null;
    public static FirebaseDataBaseManager Instane
    {
        get
        {
            if (instane == null)
            {
                instane = new FirebaseDataBaseManager();
            }
            return instane;
        }
    }

    

    public void Init()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveUserDataFn(string userid, string name, string email)
    {
        UserData userdata = new UserData(name, email, DateTime.Now.ToString("yyyy/MM/dd"));
        string json = JsonUtility.ToJson(userdata);

        dbRef.Child("users").Child(userid).SetRawJsonValueAsync(json);

    }

    public IEnumerator LoadUserDataCoroutine(string userid, string something, Action<string> callback)
    {
        DatabaseReference dbRef = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userid);
        dbRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Faulted Lead User Data");
                callback(null);
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                callback(null);
            }
            else if (task.IsCompleted)
            {
                if (task.Result.Exists)
                {
                    DataSnapshot snapshot = task.Result;
                    string userdata = snapshot.Child(something).Value.ToString();
                    callback(userdata);
                }
                else { callback(null); }
            }
            else { callback(null); }
        });

        yield return null; // 코루틴이 종료될 때까지 대기
    }

}
