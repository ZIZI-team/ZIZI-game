using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class LogInSystem : MonoBehaviour
{
    [Header("Input Field")]
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;

    public TMP_InputField loginId;
    public TMP_InputField loginPassword;

    public TMP_InputField userid;
    public TMP_InputField username;

    void Start()
    {
        FirebaseAuthManager.Instance.Init();

        FirebaseDataBaseManager.Instane.Init();
    }

    public void Create()
    {
        
        FirebaseAuthManager.Instance.SingUp(signupEmail.text, signupPassword.text);

        //name, ID save to FirebaseDataBase
        FirebaseDataBaseManager.Instane.SaveUserDataFn(userid.text, username.text, signupEmail.text);
    }

    public void Login()
    {
        StartCoroutine(FirebaseDataBaseManager.Instane.LoadUserDataCoroutine(loginId.text, "email", (result) => 
        {
            FirebaseAuthManager.Instance.Login(result, loginPassword.text);
        }
        ));
    }


    public void Logout()
    {
        FirebaseAuthManager.Instance.Logout();
    }


}
