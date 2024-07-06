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
    public TMP_InputField signupEmail; // 회원가입 이메일 입력란
    public TMP_InputField signupPassword; // 회원가입 비밀번호 입력란
    public TMP_InputField userid; // 회원가입 사용자 ID 입력란
    public TMP_InputField username; // 회원가입 사용자 이름 입력란

    [Header("Login Input Field")]
    public TMP_InputField loginId; // 로그인 ID 입력란
    public TMP_InputField loginPassword; // 로그인 비밀번호 입력란

    [Header("Message Text")]
    public GameObject OutputPanel; // 메시지 출력 패널
    public TMP_Text OutputText; // 메시지 출력 텍스트

    private FirebaseAuth auth; // Firebase 인증 객체
    private FirebaseUser user; // 현재 로그인된 사용자 정보


    void Start()
    {
        // Firebase Database 매니저 초기화
        FirebaseDataBaseManager.Instane.Init();

        // Firebase 인증 객체 가져오기
        auth = FirebaseAuth.DefaultInstance;

        // 현재 로그인된 사용자가 있는 경우 로그아웃
        if (auth.CurrentUser != null)
        {
            Logout();
        }

        // 인증 상태 변경 이벤트 구독
        auth.StateChanged += OnChanged;
    }

    /// <summary>
    /// 인증 상태 변경 이벤트 핸들러
    /// </summary>
    /// <param name="sender">이벤트 발생 객체</param>
    /// <param name="e">이벤트 인자</param>
    private void OnChanged(object sender, EventArgs e)
    {
        // 현재 로그인된 사용자가 변경된 경우
        if (auth.CurrentUser != user)
        {
            // 로그인 상태 확인
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);

            // 로그아웃된 경우
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
            }

            // 현재 로그인된 사용자 정보 업데이트
            user = auth.CurrentUser;

            // 로그인된 경우
            if (signed)
            {
                Debug.Log("로그인");
            }
        }
    }


    /// <summary>
    /// 회원가입 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void SingUp()
    {
        // 오디오 효과 재생
        AudioManager.Instance.SFX3();

        // Firebase 인증을 통해 이메일과 비밀번호로 새 사용자 생성
        auth.CreateUserWithEmailAndPasswordAsync(signupEmail.text, signupPassword.text)
        .ContinueWith(task =>
        {
        // 작업이 취소된 경우
        if (task.IsCanceled)
            {
                Debug.Log("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
        // 작업이 실패한 경우
        if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

        // 회원가입 성공 시
        Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase was created successfully: {0}, ({1})",
                result.User.DisplayName, result.User.UserId);

        // Firebase Database에 사용자 데이터 저장
        FirebaseDataBaseManager.Instane.SaveUserDataFn(userid.text, username.text, signupEmail.text);
        });
    }


    /// <summary>
    /// 로그인 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void Login()
    {
        // 오디오 효과 재생
        AudioManager.Instance.SFX3();

        // Firebase Database에서 로그인 ID에 해당하는 이메일 데이터를 비동기로 로드
        StartCoroutine(FirebaseDataBaseManager.Instane.LoadUserDataCoroutine(loginId.text, "email", result =>
        {
            // 로드된 이메일 데이터 출력
            Debug.Log(result);

            // 이메일 데이터가 없는 경우
            if (result == null)
            {
                // 출력 패널 활성화 및 오류 메시지 출력
                OutputPanel.SetActive(true);
                OutputText.text = "Not Find user ID: " + loginId.text;
            }
            // 이메일 데이터가 있는 경우
            else
            {
                // Firebase 인증을 통해 이메일과 비밀번호로 로그인
                auth.SignInWithEmailAndPasswordAsync(result, loginPassword.text)
                .ContinueWith(task =>
                {
                // 작업이 취소된 경우
                if (task.IsCanceled)
                    {
                        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                        return;
                    }
                // 작업이 실패한 경우
                if (task.IsFaulted)
                    {
                        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        return;
                    }

                // 로그인 성공 시
                Firebase.Auth.AuthResult authResult = task.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})",
                        authResult.User.DisplayName, authResult.User.UserId);
                });
            }
        }));
    }

    /// <summary>
    /// 로그아웃 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void Logout()
    {
        auth.SignOut();
    }

}
