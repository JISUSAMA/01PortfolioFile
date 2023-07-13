using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;




public class GoogleManager : MonoBehaviour
{
    public GameObject loginPanel;   //로그인 판넬
    public GameObject startPanel;   //게임시작 판넬

    public InputField id_Field; //아아디
    public InputField pw_Field; //비밀번호

    public Text logText;


    void Start()
    {
        logText.text = PlayerPrefs.GetString("Player_LoginState");
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        if (PlayerPrefs.GetString("Player_LoginState") == "")
        {
            StartCoroutine(LoginPanelShow(true, 0));  //로그인 판넬 비활성화
            StartCoroutine(GameStartShow(false, 0));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
        }
        else if(PlayerPrefs.GetString("Player_LoginState") == "Google")
        {
            StartCoroutine(LoginPanelShow(false, 0));  //로그인 판넬 비활성화
            AutoGoogleLogin();
            StartCoroutine(GameStartShow(true, 3));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
        }
        else if(PlayerPrefs.GetString("Player_LoginState") == "Gateways")
        {
            StartCoroutine(LoginPanelShow(false, 0));  //로그인 판넬 비활성화
            PlayerPrefs.SetString("Player_LoginState", "Gateways");
            PlayerPrefs.SetString("Player_ID", id_Field.text);
            PlayerPrefs.SetString("Player_PassWord", pw_Field.text);
            StartCoroutine(GameStartShow(true, 3));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
        }
            
    }

    //void GameStartView(bool _state)
    //{
    //    startPanel.SetActive(_state);
    //}

    //void LoginPanelView(bool _state)
    //{
    //    loginPanel.SetActive(_state);
    //}

    IEnumerator GameStartShow(bool _active, float _time)
    {
        yield return new WaitForSeconds(_time);

        startPanel.SetActive(_active);
    }

    IEnumerator LoginPanelShow(bool _active, float _time)
    {
        yield return new WaitForSeconds(_time);

        loginPanel.SetActive(_active);
    }



    //이용약관 및 개인정보방침 동의 버튼
    public void Personal_Information_Button()
    {
        StartCoroutine(LoginPanelShow(false, 0));  //로그인 판넬 비활성화
        GoogleLogin();
        StartCoroutine(GameStartShow(true, 3));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기

    }

    //구글로그인 버튼을 누르고 첫 구글로그인할때 사용하는 함수
    void GoogleLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                logText.text = Social.localUser.id + " \n" + Social.localUser.userName;
                PlayerPrefs.SetString("Player_LoginState", "Google");
                PlayerPrefs.SetString("Player_ID", Social.localUser.id);
                //Invoke("GameStartView", 3f);    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
            }
            else logText.text = "구글 로그인 실패";
        });
    }

    //구글로그인 후 자동로그인되는 함수
    public void AutoGoogleLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                logText.text = Social.localUser.id + " \n" + Social.localUser.userName;
                PlayerPrefs.SetString("Player_LoginState", "Google");
                PlayerPrefs.SetString("Player_ID", Social.localUser.id);
            }
            else logText.text = "구글 로그인 실패";
        });
    }

    //로그인 화면에 있는 로그인 버튼을 눌렀을 때 이벤트
    public void GatewaysLogin()
    {
        PlayerPrefs.SetString("Player_LoginState", "Gateways");
        PlayerPrefs.SetString("Player_ID", id_Field.text);
        PlayerPrefs.SetString("Player_PassWord", pw_Field.text);

        StartCoroutine(LoginPanelShow(false, 0));  //로그인 판넬 비활성화
        StartCoroutine(GameStartShow(true, 3));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
    }



    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        //logText.text = "구글 로그아웃";
        // 서버에 현재 계정에 관련된 값 저장
        // 접속 끊겼던 시간, 접속중이었던 시간
        // 또....
        ServerManager.Instance.UpdateConnectedState("ConnectTime", PlayerPrefs.GetString("ConnectTime"));
    }

    //계정만들기 버튼
    public void Join()
    {
        SceneManager.LoadScene("Join");
    }
}
