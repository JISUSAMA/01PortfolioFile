using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI; 



public class GoogleLogin : MonoBehaviour
{
    public Text logText;



    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
                logText.text = Social.localUser.id + " \n" +
                Social.localUser.userName;
            else
                logText.text = "구글 로그인 실패";
        });
    }

    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        logText.text = "구글 로그아웃";
    }
}
