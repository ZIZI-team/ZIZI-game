using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

/// <summary>
/// 사용자 데이터를 저장하기 위한 클래스
/// </summary>
public class UserData
{
    public string name; // 사용자 이름
    public string email; // 사용자 이메일
    public string sginupdata; // 가입 날짜

    /// <summary>
    /// 사용자 데이터를 초기화하는 생성자
    /// </summary>
    /// <param name="name">사용자 이름</param>
    /// <param name="email">사용자 이메일</param>
    /// <param name="sginupdata">가입 날짜</param>
    public UserData(string name, string email, string sginupdata)
    {
        this.name = name;
        this.email = email;
        this.sginupdata = sginupdata;
    }
}

/// <summary>
/// Firebase Realtime Database를 관리하는 매니저 클래스
/// </summary>
public class FirebaseDataBaseManager
{
    private DatabaseReference dbRef; // Firebase Realtime Database 참조

    // 싱글톤 패턴을 사용하여 FirebaseDataBaseManager 인스턴스 관리
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

    /// <summary>
    /// Firebase Realtime Database 초기화
    /// </summary>
    public void Init()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// 사용자 데이터를 Firebase Realtime Database에 저장하는 함수
    /// </summary>
    /// <param name="userid">사용자 ID</param>
    /// <param name="name">사용자 이름</param>
    /// <param name="email">사용자 이메일</param>
    public void SaveUserDataFn(string userid, string name, string email)
    {
        // UserData 객체 생성
        UserData userdata = new UserData(name, email, DateTime.Now.ToString("yyyy/MM/dd"));
        // UserData를 JSON 형식으로 변환
        string json = JsonUtility.ToJson(userdata);

        // Firebase Realtime Database에 사용자 데이터 저장
        dbRef.Child("users").Child(userid).SetRawJsonValueAsync(json);
    }

    /// <summary>
    /// Firebase Realtime Database에서 사용자 데이터를 로드하는 코루틴 함수
    /// </summary>
    /// <param name="userid">사용자 ID</param>
    /// <param name="something">데이터베이스에서 가져올 데이터의 키</param>
    /// <param name="callback">데이터 로드 완료 시 호출할 콜백 함수</param>
    /// <returns>코루틴 실행 대기</returns>
    public IEnumerator LoadUserDataCoroutine(string userid, string something, Action<string> callback)
    {
        // Firebase Realtime Database의 "users" 경로에 사용자 ID 경로 생성
        DatabaseReference dbRef = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userid);

        // 데이터베이스에서 값을 비동기로 가져오기
        dbRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // 작업이 실패한 경우
            if (task.IsFaulted)
            {
                Debug.Log("Faulted Lead User Data");
                callback(null);
            }
            // 작업이 취소된 경우
            else if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                callback(null);
            }
            // 작업이 완료된 경우
            else if (task.IsCompleted)
            {
                // 데이터가 존재하는 경우
                if (task.Result.Exists)
                {
                    DataSnapshot snapshot = task.Result;
                    // 지정한 키의 값을 문자열로 가져와 콜백 함수에 전달
                    string userdata = snapshot.Child(something).Value.ToString();
                    callback(userdata);
                }
                else // 데이터가 존재하지 않는 경우
                {
                    callback(null);
                }
            }
            else // 그 외의 경우
            {
                callback(null);
            }
        });

        yield return null; // 코루틴이 종료될 때까지 대기
    }

}
