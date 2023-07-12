using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.Localization.Components;

public class LoginManager2 : MonoBehaviour
{
    public GameObject informationPopup; //게임정보 동의 팝업
    public GameObject loginPanel;   //로그인 판넬
    public GameObject statePanel;   //상태판넬 - 게임시작,닉네임,캐릭터설정
    public GameObject[] stateButton;    //각 상태 버튼
    public Sprite[] StartPanel_sprites; //로그인 상태일 경우, 사용하는 랜덤 스프라이트 종류

    public InputField id_Field; //아이디
    public InputField pw_Field; //비밀번호

    public GameObject pwFindPopup;  //비번찾기판넬
    public InputField pwFind_Field; //비번찾기
    public GameObject successPopup; //메일보내기 성공 팝업
    public GameObject noticPopup;   //알림팝업
    public Text noticText;  //알림텍스트
    public Text successText;

    public Toggle personalToggle;   //개인정보약관 동의 토글
    public GameObject personalPanel;    //개인정보팝업

    bool makeJoinState; //계정만들기 버튼 클릭 여부

    string loginState;
    string player_id, player_uid, player_pw;

    bool toggleIsOn;    //약관동의 여부
    string PASSWORD_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //임의의 비밀번호 생성하기 위한 문자들



    public GameObject[] backLogoImg;
    public GameObject fadeEffect;   //페이드 효과 스크립트
    public GameObject fadeEffect2;   //페이드 효과 스크립트
    FadeEffect fade_sripts;
    FadeEffect fade_sripts2;

    bool isEmailFormat; //이메일 포맷
    bool invalidEmailType;


    public LocalizeStringEvent noticeTextStringEvent;


    void Start()
    {
        //fade_sripts = fadeEffect.GetComponent<FadeEffect>();
        //fade_sripts2 = fadeEffect2.GetComponent<FadeEffect>();

        //if (fade_sripts.fadeStartState == false)
        //{
        //    fadeEffect.SetActive(true);
        //    //fadeEffect2.SetActive(false);
        //    backLogoImg[0].SetActive(true);
        //    backLogoImg[1].SetActive(true);
        //    StartCoroutine(FadeStart());
        //}


        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;
        //구글 플레이 활성화
        PlayGamesPlatform.Activate();

        //PlayerPrefs.SetString("Busan_Player_LoginState", "");
        //PlayerPrefs.SetFloat("ConnectTime", 0);

        loginState = PlayerPrefs.GetString("Busan_Player_LoginState");

        Debug.Log("loginState : " + loginState);
        // 네트워크 상태 체크.....

        if (makeJoinState.Equals(true))//loginState == "")
        {
            //첫 입장 - 개인정보 동의 팝업 활성화
            informationPopup.SetActive(true);
        }
        //다른 아이디로 로그인 시
        else if (loginState == "Again")
        {
            loginPanel.SetActive(true); //로그인 판넬 활성화
            statePanel.SetActive(false);    //상태 판넬 비활성화
        }
        //자동 구글로그인
        else if (loginState == "Google" || loginState == "GoogleNickName" || loginState == "GoogleCharacter")
        {
            loginPanel.SetActive(true); //로그인 판넬 활성화
            GoogleLoginButtonOn();
        }
        //자동 게이트웨이즈 로그인
        else if (loginState == "Gateways" | loginState == "GatewaysNickName" || loginState == "GatewaysCharacter")
        {
            loginPanel.SetActive(true); //로그인 판넬 활성화
            AutoGatewaysLogin();
        }

        //로그이 했을 때 로그인 화면 이미지 랜덤으로 적용하기
       statePanel.GetComponent<Image>().sprite = StartPanel_sprites[UnityEngine.Random.Range(0, StartPanel_sprites.Length)];
    }

    //IEnumerator FadeStart()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    fade_sripts.Fade(); //점점 어둡게 실행

    //    yield return new WaitForSeconds(1.5f);
    //    fadeEffect.SetActive(false);
    //    backLogoImg[0].SetActive(false);
    //    //backLogoImg.SetActive(false);
    //    StartCoroutine(FadeStart2());
    //}

    //IEnumerator FadeStart2()
    //{
    //    //yield return new WaitForSeconds(1.5f);
    //    //fadeEffect2.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    fade_sripts2.Fade();//점점 어둡게 실행

    //    yield return new WaitForSeconds(1.5f);
    //    fadeEffect2.SetActive(false);
    //    backLogoImg[1].SetActive(false);
    //}


    public void Logout()
    {
        //PlayGamesPlatform pl = (GooglePlayGames.PlayGamesPlatform)Social.Active;
        //pl.SignOut();

        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    public void Initialization()
    {

    }


    //개인정보약관 동의 이벤트
    public void Personal_InfoToggle()
    {
        if (personalToggle.isOn == true)
        {
            toggleIsOn = true;
        }
        else
        {
            toggleIsOn = false;
        }
    }
    //개인정보약관 완료 버튼 이벤트
    public void CompletionButtonOn()
    {
        if (makeJoinState.Equals(true))
        {
            if (toggleIsOn)  //동의했으면
            {
                personalPanel.SetActive(false);
                SceneManager.LoadScene("Join");
            }
            else
            {//동의안했으면
                personalPanel.SetActive(true);
            }
        }
        else
        {
            if (toggleIsOn)  //동의했으면
            {
                personalPanel.SetActive(false);
                loginPanel.SetActive(true); //로그인 판넬 활성화
            }
            else
            {//동의안했으면
                personalPanel.SetActive(true);
            }
        }
    }

    IEnumerator StatePanel_and_ButtonAction(float _time, bool _statePanel, bool _start, bool _nickkname, bool _character)
    {
        yield return new WaitForSeconds(_time);

        loginPanel.SetActive(false); //로그인 판넬 비활성화
        statePanel.SetActive(_statePanel);
        stateButton[0].SetActive(_start);
        stateButton[1].SetActive(_nickkname);
        stateButton[2].SetActive(_character);

        //로그이 했을 때 로그인 화면 이미지 랜덤으로 적용하기
        statePanel.GetComponent<Image>().sprite = StartPanel_sprites[UnityEngine.Random.Range(0, StartPanel_sprites.Length)];
    }

    void AutoGatewaysLogin()
    {
        player_id = PlayerPrefs.GetString("Busan_Player_ID");
        player_pw = PlayerPrefs.GetString("Busan_Player_PassWord");
        player_uid = PlayerPrefs.GetString("Busan_Player_UID");

        StartCoroutine(_AutoGatewaysLogin());
    }

    IEnumerator _AutoGatewaysLogin()
    {
        Debug.Log("로그인 상태 " + loginState);
        if (loginState == "Gateways")
        {
            ServerManager.Instance.Search_USER_Info(player_id);

            yield return new WaitUntil(() => ServerManager.Instance.isUserSearchCompleted);

            ServerManager.Instance.isUserSearchCompleted = false;
            //ServerMyInformation();  //서버에서 정보 들고오기
            StartCoroutine(_ServerMyInformation(player_id));

            //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
            StartCoroutine(StatePanel_and_ButtonAction(2f, true, true, false, false));
        }
        else if (loginState == "GatewaysNickName")
        {
            //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
            StartCoroutine(StatePanel_and_ButtonAction(2f, true, false, true, false));
        }
        else if (loginState == "GatewaysCharacter")
        {
            // 로그인 상태 GatewaysCharater 라면 서버에서 닉네임 가져오기 위함
            ServerManager.Instance.Search_USER_Info(player_id);

            yield return new WaitUntil(() => ServerManager.Instance.isUserSearchCompleted);

            ServerManager.Instance.isUserSearchCompleted = false;

            PlayerPrefs.SetString("Busan_Player_NickName", ServerManager.Instance.userInfo.player_NickName);    //서버에 저장된 닉네임 들고와서 저장
            Debug.Log("ServerManager " + ServerManager.Instance.userInfo.player_NickName);
            StartCoroutine(StatePanel_and_ButtonAction(2f, true, false, false, true));
        }
    }

    //게이트웨이즈로그인 버튼 클릭
    public void GatewaysLoginButtonOn()
    {
        // 서버에 아이디가 있는지 확인
        ServerManager.Instance.Search_USER_Info(id_Field.text);
        // 서버 로딩 딜레이 주기 밑에 다 코루틴안으로 이동
        StartCoroutine(_GatewaysLoginButtonOn());
    }

    IEnumerator _GatewaysLoginButtonOn()
    {
        yield return new WaitUntil(() => ServerManager.Instance.isUserSearchCompleted);

        ServerManager.Instance.isUserSearchCompleted = false;

        if (id_Field.text == ServerManager.Instance.userInfo.player_ID)
        {
            if (pw_Field.text == ServerManager.Instance.userInfo.player_PW)
            {
                PlayerPrefs.SetString("Busan_Player_ID", id_Field.text);  //아이디 저장
                PlayerPrefs.SetString("Busan_Player_PassWord", pw_Field.text);    //비번 저장
                PlayerPrefs.SetString("Busan_Player_UID", ServerManager.Instance.userInfo.player_UID);    //서버(player_uid)에 저장된 고유 아이디 들고 와서 저장
                PlayerPrefs.SetString("Busan_Player_LoginState", ServerManager.Instance.userInfo.player_LoginState);  //서버에 저장된 로그인 상태를 들고와서 저장
                loginState = PlayerPrefs.GetString("Busan_Player_LoginState");

                if (loginState == "Gateways")
                {
                    StartCoroutine(_ServerMyInformation(id_Field.text.ToString()));  //서버에서 정보 들고오기
                                                                                     //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                    StartCoroutine(StatePanel_and_ButtonAction(2f, true, true, false, false));
                }
                else if (loginState == "GatewaysNickName")
                {
                    //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                    StartCoroutine(StatePanel_and_ButtonAction(2f, true, false, true, false));
                }
                else if (loginState == "GatewaysCharater")
                {
                    PlayerPrefs.SetString("Busan_Player_NickName", ServerManager.Instance.userInfo.player_NickName);    //서버에 저장된 닉네임 들고와서 저장
                                                                                                                        //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                    StartCoroutine(StatePanel_and_ButtonAction(2f, false, false, false, true));
                }
            }
            else if (pw_Field.text != ServerManager.Instance.userInfo.player_PW)
            {
                Debug.Log("입력한 비밀번호가 맞지않습니다.");
                noticPopup.SetActive(true);
                //noticText.text = "입력한 비밀번호가 맞지 않습니다.";
                noticeTextStringEvent.StringReference.SetReference("StringLanguageTable", "text_nopassword_key");
                // 패널 넣기
            }
        }
        else if (id_Field.text != ServerManager.Instance.userInfo.player_ID)
        {
            Debug.Log("입력한 아이디가 존재하지 않습니다.");
            noticPopup.SetActive(true);
            //noticText.text = "입력한 아이디가 존재하지 않습니다.";
            noticeTextStringEvent.StringReference.SetReference("StringLanguageTable", "text_enterednotid_key");
            // 패널 넣기
        }
    }

    //구글로그인 버튼 클릭
    public void GoogleLoginButtonOn()
    {
        StartCoroutine(_GoogleLoginButtonOn());
    }

    IEnumerator _GoogleLoginButtonOn()
    {
        Debug.Log(" --- " + Social.localUser.userName);
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)    // 아이디, 비밀번호 맞게 쳤음
            {
                ServerManager.Instance.Search_USER_Info(Social.localUser.id);

                Invoke("Delay_StakUp_ID", 1f);
            }
            else
            {
                noticPopup.SetActive(true);
                noticeTextStringEvent.StringReference.SetReference("StringLanguageTable", "text_googleloginfail_key");
            }
        });

        yield return null;
    }

    private void Delay_StakUp_ID()
    {
        //아이디가 없으면 - 첫 로그인
        if (Social.localUser.id != ServerManager.Instance.userInfo.player_ID)   //서버(player_id)에 있는 사용자 id중에 같은게 있는지 검색
        {
            player_uid = uniqueID();    //고유 아이디 생성
            PlayerPrefs.SetString("Busan_Player_ID", Social.localUser.id);  //아이디 저장
            PlayerPrefs.SetString("Busan_Player_UID", player_uid);    //고유 아이디 저장
            PlayerPrefs.SetString("Busan_Player_LoginState", "GoogleNickName");    // 닉네임씬 상태 저장
            PlayerPrefs.SetString("Busan_Player_Password", "");    // PW

            // 서버에도 저장
            ServerManager.Instance.UserInfo_Reg();

            //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
            StartCoroutine(StatePanel_and_ButtonAction(2f, true, false, true, false));
        }
        else //아이디있으면 - 서버에서 구글아이디와 비교해서 가입한 기록이 있으면
        {
            PlayerPrefs.SetString("Busan_Player_ID", Social.localUser.id);  //아이디 저장
            PlayerPrefs.SetString("Busan_Player_UID", ServerManager.Instance.userInfo.player_UID);    //서버(player_uid)에 저장된 고유 아이디 들고 와서 저장
            PlayerPrefs.SetString("Busan_Player_LoginState", ServerManager.Instance.userInfo.player_LoginState); //서버에 저장된 로그인 상태를 들고와서 저장

            loginState = PlayerPrefs.GetString("Busan_Player_LoginState");

            if (loginState == "Google")
            {
                StartCoroutine(_ServerMyInformation(Social.localUser.id));  //서버에서 정보 들고오기
                                                                            //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                StartCoroutine(StatePanel_and_ButtonAction(2f, true, true, false, false));
            }
            else if (loginState == "GoogleNickName")
            {
                //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                StartCoroutine(StatePanel_and_ButtonAction(2f, true, false, true, false));
            }
            else if (loginState == "GoogleCharacter")
            {
                PlayerPrefs.SetString("Busan_Player_NickName", ServerManager.Instance.userInfo.player_NickName);    //서버에 저장된 닉네임 들고와서 저장
                                                                                                                    //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                StartCoroutine(StatePanel_and_ButtonAction(2f, false, false, false, true));
            }
        }
    }

    //계정 고유 UID 생성 함수
    string uniqueID()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "01" + z1;
        return uid;
    }


    //임시 비밀번호 생성 함수
    public string GeneratePaseeword(int _length)
    {
        var sb = new System.Text.StringBuilder(_length);
        var r = new System.Random();

        for (int i = 0; i < _length; i++)
        {
            int pos = r.Next(PASSWORD_CHARS.Length);
            char c = PASSWORD_CHARS[pos];
            sb.Append(c);
        }
        return sb.ToString();
    }
    //올바른 이메일인지 체크
    public bool Check_Id(string _id)
    {
        if (string.IsNullOrEmpty(_id))
            isEmailFormat = false;

        _id = Regex.Replace(_id, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);
        if (invalidEmailType)
            isEmailFormat = false;

        isEmailFormat = Regex.IsMatch(_id,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);

        //Debug.Log("이메일 체크 : " + isEmailFormat);
        return isEmailFormat;
    }
    //도메인으로 변경 함수
    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalidEmailType = true;
        }
        return match.Groups[1].Value + domainName;
    }


    //닉네임 씬 화면 이동
    public void NickNameSceneMove()
    {
        SceneManager.LoadScene("NickName");
    }
    //캐릭터선택 씬 화면 이동
    public void CharacterChoiceSceneMove()
    {
        SceneManager.LoadScene("CharacterChoice");
    }
    //로비 씬 화면 이동
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("Lobby");
    }
    //계정만들기 씬 화면 이동
    public void JoinSceneMove()
    {
        makeJoinState = true;   //계정만들기 버튼 클릭
        //SceneManager.LoadScene("Join");
    }

    //비밀번호찾기 버튼 클릭 이벤트
    public void PassWordFind()
    {
        pwFindPopup.SetActive(true);
    }

    //비밀번호 찾기 버튼 이벤트 - 회사에 가입된 이메일 검색
    public void EmailServerDataCompartison()
    {
        // 서버에 아이디가 있는지 확인
        ServerManager.Instance.Search_USER_Info(Social.localUser.id);
        // 서버 로딩 딜레이 주기 밑에 다 코루틴안으로 이동
        StartCoroutine(_EmailServerDataCompartison());
    }

    //메일 보내는 함수
    IEnumerator _EmailServerDataCompartison()
    {
        yield return new WaitUntil(() => ServerManager.Instance.isUserSearchCompleted);   // 서버에서 검색을 끝내면

        ServerManager.Instance.isUserSearchCompleted = false;

        string pw = GeneratePaseeword(7);   //임의의 비밀번호
        Check_Id(pwFind_Field.text);

        //이메일 주소가 올바르지 않다
        if(isEmailFormat.Equals(false))
        {
            if(pwFind_Field.text != "")
            {
                successPopup.SetActive(true);
                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    successText.text = "올바르지 않은 이메일입니다.\n다시 입력해주세요.";
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    successText.text = "Invalid email.\nPlease enter it again.";
            }
            else
            {
                successPopup.SetActive(true);
                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    successText.text = "이메일을 입력하지 않으셨습니다.\n이메일을 입력해주세요.";
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    successText.text = "You have not entered an email.\nPlease enter your email.";
            }
        }
        else
        {
            if (pwFind_Field.text == ServerManager.Instance.userInfo.player_ID) // 이메일 확인 체크
            {
                ServerManager.Instance.Get_CompanyEmail();
                yield return new WaitUntil(() => ServerManager.Instance.isCompanyEmailSearchCompleted);   // 서버에서 검색을 끝내면
                ServerManager.Instance.isCompanyEmailSearchCompleted = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ServerManager.Instance.company_Email.email); // 보내는사람
                mail.To.Add(pwFind_Field.text); // 받는 사람
                if(PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                {
                    mail.Subject = "[시티투어부산-gateways]비밀번호 재설정";
                    mail.Body = "임의의 비밀번호를 보내드립니다." + "\n" + "대괄호('[]')안에 있는 비밀번호로 게임에 접속해주세요." + "\n" +
                        "게임에 접속하신 후 계정설정에서 비밀번호를 꼭 변경해주세요." + "\n" + "\n" + "\n" +
                        "[ " + pw + " ]"; //임의의 비밀번호 넣기
                }
                else if(PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                {
                    mail.Subject = "[City Tour Busan - gateways]Reset Password";
                    mail.Body = "I'm sending you a random password." + "\n" + "Please access the game with the password in square brackets ('[]')." + "\n" +
                        "Please change your password in your account settings after accessing the game." + "\n" + "\n" + "\n" +
                        "[ " + pw + " ]"; //임의의 비밀번호 넣기
                }
                ServerManager.Instance.PasswordChange(pw);

                SmtpClient smtpServer = new SmtpClient("smtp.naver.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(ServerManager.Instance.company_Email.email, ServerManager.Instance.company_Email.email_PW) as ICredentialsByHost; // 보내는사람 주소 및 비밀번호 확인
                smtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

                { return true; };

                smtpServer.Send(mail);

                successPopup.SetActive(true);
                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    successText.text = "메일이 성공적으로 발송되었습니다." + "\n" + "메일을 확인해주시길 바랍니다.";
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    successText.text = "Your mail has been sent successfully." + "\n" + "Please check your mail.";
                pwFind_Field.text = "";
            }
            else
            {
                successPopup.SetActive(true);
                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    successText.text = "동일한 메일(아이디)이 없습니다." + "\n" + "다시 한번 확인 후 이용해주시길 바랍니다.";
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    successText.text = "I don't have the same email (ID)." + "\n" + "Please check it again and use it.";
                pwFind_Field.text = "";
            }
        }


        //if (pwFind_Field.text == ServerManager.Instance.userInfo.player_ID) // 이메일 확인 체크
        //{
        //    ServerManager.Instance.Get_CompanyEmail();

        //    yield return new WaitUntil(() => ServerManager.Instance.isCompanyEmailSearchCompleted);   // 서버에서 검색을 끝내면

        //    ServerManager.Instance.isCompanyEmailSearchCompleted = false;

        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(ServerManager.Instance.company_Email.email); // 보내는사람
        //    mail.To.Add(pwFind_Field.text); // 받는 사람
        //    mail.Subject = "[gateways]비밀번호 재설정";
        //    mail.Body = "임의의 비밀번호를 보내드립니다." + "\n" + "대괄호('[]')안에 있는 비밀번호로 게임에 접속해주세요." + "\n" +
        //        "게임에 접속하신 후 계정설정에서 비밀번호를 꼭 변경해주세요." + "\n" + "\n" + "\n" +
        //        "[ " + pw + " ]"; //임의의 비밀번호 넣기

        //    ServerManager.Instance.PasswordChange(pw);

        //    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        //    smtpServer.Port = 587;
        //    smtpServer.Credentials = new System.Net.NetworkCredential(ServerManager.Instance.company_Email.email, ServerManager.Instance.company_Email.email_PW) as ICredentialsByHost; // 보내는사람 주소 및 비밀번호 확인
        //    smtpServer.EnableSsl = true;
        //    ServicePointManager.ServerCertificateValidationCallback =
        //    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

        //    { return true; };

        //    smtpServer.Send(mail);

        //    Debug.Log("success!");
        //    successPopup.SetActive(true);
        //    noticText.text = "메일이 성공적으로 발송되었습니다." + "\n" + "메일을 확인해주시길 바랍니다.";
        //    pwFind_Field.text = "";
        //}
        //else
        //{
        //    Debug.Log("not exist e-mail!");
        //    successPopup.SetActive(true);
        //    noticText.text = "동일한 메일(아이디)이 없습니다." + "\n" + "다시 한번 확인 후 이용해주시길 바랍니다.";
        //    pwFind_Field.text = "";
        //    // 이메일 존재하지 않는다 패널 잠깐있다 사라짐
        //}
    }
    IEnumerator _ServerMyInformation(string _userID)
    {
        // 캐릭터 데이터 가져오기
        ServerManager.Instance.GetCharacterInfo(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterDataStackCompleted);
        ServerManager.Instance.isCharacterDataStackCompleted = false;

        // 계정의 출석체크 가져오기
        ServerManager.Instance.GetConnectedState(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isConnectedStateStackCompleted);
        ServerManager.Instance.isConnectedStateStackCompleted = false;

        // 일일 퀘스트 가져오기
        ServerManager.Instance.GetTodayQuest(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isTodayQuestStackCompleted);
        ServerManager.Instance.isTodayQuestStackCompleted = false;

        // 맵 퀘스트 가져오기
        ServerManager.Instance.GetMapQuest(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isMapQuestStackCompleted);
        ServerManager.Instance.isMapQuestStackCompleted = false;

        // 맵 퀘스트 보상 상태 가져오기
        ServerManager.Instance.GetQuestReward(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isQuestRewardStackCompleted);
        ServerManager.Instance.isQuestRewardStackCompleted = false;

        // 맵 진척도
        ServerManager.Instance.Get_MapProgress(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isGetMapProgressCompleted);
        ServerManager.Instance.isGetMapProgressCompleted = false;

        // 볼륨 설정.
        ServerManager.Instance.SettingInfo("SELECT", _userID);
        yield return new WaitUntil(() => ServerManager.Instance.isGetSettingCompleted);
        ServerManager.Instance.isGetSettingCompleted = false;


        //전부 서버에서 들고오기!!!! 전부 
        PlayerPrefs.SetString("Busan_Player_NickName", ServerManager.Instance.userInfo.player_NickName);
        PlayerPrefs.SetString("Busan_Player_LoginState", ServerManager.Instance.userInfo.player_LoginState);
        PlayerPrefs.SetInt("Busan_Player_Profile", ServerManager.Instance.userInfo.player_Profile);

        PlayerPrefs.SetString("Busan_Player_Sex", ServerManager.Instance.characterInfo.player_Sex);
        //PlayerPrefs.SetString("Busan_Player_Face", ServerManager.Instance.characterInfo.player_Face);
        //PlayerPrefs.SetString("Busan_Player_SkinColor", ServerManager.Instance.characterInfo.player_SkinColor);
        //PlayerPrefs.SetString("Busan_Player_Hair", ServerManager.Instance.characterInfo.player_Hair);
        //PlayerPrefs.SetString("Busan_Player_HairColor", ServerManager.Instance.characterInfo.player_HairColor);

        PlayerPrefs.SetInt("Busan_Player_Gold", ServerManager.Instance.level_And_golds.player_Gold); //, "서버에서 들고오세용"
        PlayerPrefs.SetInt("Busan_Player_Level", ServerManager.Instance.level_And_golds.player_Level); //, "서버에서 들고오세용"
        PlayerPrefs.SetInt("Busan_Player_CurrExp", ServerManager.Instance.level_And_golds.player_CurrEXP); //, "서버에서 들고오세용"
        PlayerPrefs.SetInt("Busan_Palyer_TakeExp", ServerManager.Instance.level_And_golds.player_TakeEXP); //, "서버에서 들고오세용"

        PlayerPrefs.SetFloat("Busan_Record_MaxSpeed", ServerManager.Instance.records.record_MaxSpeed);
        PlayerPrefs.SetFloat("Busan_Record_TodayKcal", ServerManager.Instance.records.record_TodayKal);
        PlayerPrefs.SetFloat("Busan_Record_TodayKm", ServerManager.Instance.records.record_TodayKm);
        PlayerPrefs.SetFloat("Busan_TodayMaxSpeed", ServerManager.Instance.records.record_TodayMaxSpeed); //서버!!!!
        PlayerPrefs.SetFloat("Busan_Record_TotalKm", ServerManager.Instance.records.record_TotalKm);

        PlayerPrefs.SetString("Busan_OpenMap_CourseName", ServerManager.Instance.mapProgress.mapProgress);    //현재 오픈되어있는 맵
        PlayerPrefs.SetFloat("Busan_BackBGMVol", ServerManager.Instance.setting.bgm);
        //PlayerPrefs.SetFloat("SFXVol", 0);

        //PlayerPrefs.SetString("Busan_Wear_HairKind", ServerManager.Instance.equipped_item_list.Wear_HairKind);
        //PlayerPrefs.SetString("Busan_Wear_GlovesKind", ServerManager.Instance.equipped_item_list.Wear_GlovesKind);
        //PlayerPrefs.SetString("Busan_Wear_HelmetKind", ServerManager.Instance.equipped_item_list.Wear_HelmetKind);
        //PlayerPrefs.SetString("Busan_Wear_JacketKind", ServerManager.Instance.equipped_item_list.Wear_JacketKind);
        //PlayerPrefs.SetString("Busan_Wear_PantsKind", ServerManager.Instance.equipped_item_list.Wear_PantsKind);
        //PlayerPrefs.SetString("Busan_Wear_ShoesKind", ServerManager.Instance.equipped_item_list.Wear_ShoesKind);
        //PlayerPrefs.SetString("Busan_Wear_BicycleKind", ServerManager.Instance.equipped_item_list.Wear_BicycleKind);

        //PlayerPrefs.SetString("Busan_Wear_HairStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_HairStyleName);
        //PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_GlovesStyleName);
        //PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_HelmetStyleName);
        //PlayerPrefs.SetString("Busan_Wear_JacketStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_JacketStyleName);
        //PlayerPrefs.SetString("Busan_Wear_PantsStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_PantsStyleName);
        //PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_ShoesStyleName);
        //PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", ServerManager.Instance.equipped_item_stylename_list.Wear_BicycleStyleName);

        //PlayerPrefs.SetInt("Busan_HairNumber", ServerManager.Instance.equipped_item_num.HairNumber);
        //PlayerPrefs.SetInt("Busan_BodyNumber", ServerManager.Instance.equipped_item_num.BodyNumber);
        //PlayerPrefs.SetInt("Busan_GlovesNumber", ServerManager.Instance.equipped_item_num.GlovesNumber);
        //PlayerPrefs.SetInt("Busan_HelmetNumber", ServerManager.Instance.equipped_item_num.HelmetNumber);
        //PlayerPrefs.SetInt("Busan_JacketNumber", ServerManager.Instance.equipped_item_num.JacketNumber);
        //PlayerPrefs.SetInt("Busan_PantsNumber", ServerManager.Instance.equipped_item_num.PantsNumber);
        //PlayerPrefs.SetInt("Busan_ShoesNumber", ServerManager.Instance.equipped_item_num.ShoesNumber);
        //PlayerPrefs.SetInt("Busan_BicycleNumber", ServerManager.Instance.equipped_item_num.BicycleNumber);

        ////출석체크 형식
        PlayerPrefs.SetString("Busan_OneCennectDay", ServerManager.Instance.connectedStateCheck.oneConnectDay);   //미션씬에서 체크
        PlayerPrefs.SetString("Busan_TwoCennedtDay", ServerManager.Instance.connectedStateCheck.twoConnectDay);
        PlayerPrefs.SetString("Busan_OneCheck", ServerManager.Instance.connectedStateCheck.oneCheck);    //로비에서 체크
        PlayerPrefs.SetString("Busan_TwoCheck", ServerManager.Instance.connectedStateCheck.twoCheck);
        PlayerPrefs.SetString("Busan_ConnectTime", ServerManager.Instance.connectedStateCheck.connectTime);


        //경기 기록---------------------------
        PlayerPrefs.SetString("Busan_Asia Normal 1Course", ServerManager.Instance.recordByAsiaCourse.asiaNormal_1C);
        PlayerPrefs.SetString("Busan_Asia Normal 2Course", ServerManager.Instance.recordByAsiaCourse.asiaNormal_2C);
        PlayerPrefs.SetString("Busan_Asia Normal 3Course", ServerManager.Instance.recordByAsiaCourse.asiaNormal_3C);
        PlayerPrefs.SetString("Busan_Asia Hard 1Course", ServerManager.Instance.recordByAsiaCourse.asiaHard_1C);
        PlayerPrefs.SetString("Busan_Asia Hard 2Course", ServerManager.Instance.recordByAsiaCourse.asiaHard_2C);
        PlayerPrefs.SetString("Busan_Asia Hard 3Course", ServerManager.Instance.recordByAsiaCourse.asiaHard_3C);


        ////----- 아이템 & 퀘스트---------
        ////아이템 갯수 > 로비로 갈 때 ItemDataBase.cs 에서 불러옴
        //PlayerPrefs.SetInt("ExpPlusAmount", );
        //PlayerPrefs.SetInt("ExpUpAmount", );
        //PlayerPrefs.SetInt("CoinUpAmount", );
        //PlayerPrefs.SetInt("SpeedUpAmount", );

        //일일퀘스트 실행 여부
        PlayerPrefs.SetInt("Busan_TodayQuest1", ServerManager.Instance.myTodayQuest[0].quest_Idx);    // 접속하기 myTodayQuest[0]
        PlayerPrefs.SetInt("Busan_TodayQuest2", ServerManager.Instance.myTodayQuest[1].quest_Idx);    // 방문하기
        PlayerPrefs.SetInt("Busan_TodayQuest3", ServerManager.Instance.myTodayQuest[2].quest_Idx);    // 게임미션
        PlayerPrefs.SetInt("Busan_TodayQuest4", ServerManager.Instance.myTodayQuest[3].quest_Idx);    // 칼로리소모
        PlayerPrefs.SetInt("Busan_TodayQuest5", ServerManager.Instance.myTodayQuest[4].quest_Idx);    // 최고속도

        //// 서버 퀘스트2의 Quest_Idx 값 하나를 제외한 나머지상태 No 
        //// 방문하기 퀘스트 ( 0 ~ 3 )
        for (int i = 0; i < 4; i++)
        {
            if (ServerManager.Instance.myTodayQuest[1].quest_Idx == i)
            {
                if (i == 0)
                {
                    PlayerPrefs.SetString("Busan_StoreVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
                }
                else if (i == 1)
                {
                    PlayerPrefs.SetString("Busan_MyInfoVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
                }
                else if (i == 2)
                {
                    PlayerPrefs.SetString("Busan_InventoryVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
                }
                else if (i == 3)
                {
                    PlayerPrefs.SetString("Busan_RankJonVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
                }
            }
            else
            {
                if (i == 0)
                {
                    PlayerPrefs.SetString("Busan_StoreVisit", "No");
                }
                else if (i == 1)
                {
                    PlayerPrefs.SetString("Busan_MyInfoVisit", "No");
                }
                else if (i == 2)
                {
                    PlayerPrefs.SetString("Busan_InventoryVisit", "No");
                }
                else if (i == 3)
                {
                    PlayerPrefs.SetString("Busan_RankJonVisit", "No");
                }
            }
        }

        //// 서버 퀘스트3의 Quest_Idx 값 하나를 제외한 나머지상태 No 
        //// 게임미션 퀘스트 ( 0 ~ 6 )
        for (int i = 0; i < 7; i++)
        {
            if (ServerManager.Instance.myTodayQuest[2].quest_Idx == i)
            {
                if (i == 0)
                {
                    PlayerPrefs.SetString("Busan_ProfileChange", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 1)
                {
                    PlayerPrefs.SetString("Busan_GameOnePlay", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 2)
                {
                    PlayerPrefs.SetString("Busan_RankUp", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 3)
                {
                    PlayerPrefs.SetString("Busan_CustomChange", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 4)
                {
                    PlayerPrefs.SetString("Busan_GoldUse", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 5)
                {
                    PlayerPrefs.SetString("Busan_ItemUse", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
                else if (i == 6)
                {
                    PlayerPrefs.SetString("Busan_ItemPurchase", ServerManager.Instance.myTodayQuest[2].quest_State);
                }
            }
            else
            {
                if (i == 0)
                {
                    PlayerPrefs.SetString("Busan_ProfileChange", "No");
                }
                else if (i == 1)
                {
                    PlayerPrefs.SetString("Busan_GameOnePlay", "No");
                }
                else if (i == 2)
                {
                    PlayerPrefs.SetString("Busan_RankUp", "No");
                }
                else if (i == 3)
                {
                    PlayerPrefs.SetString("Busan_CustomChange", "No");
                }
                else if (i == 4)
                {
                    PlayerPrefs.SetString("Busan_GoldUse", "No");
                }
                else if (i == 5)
                {
                    PlayerPrefs.SetString("Busan_ItemUse", "No");
                }
                else if (i == 6)
                {
                    PlayerPrefs.SetString("Busan_ItemPurchase", "No");
                }
            }
        }

        //// 칼로리소모 퀘스트 상태
        PlayerPrefs.SetString("Busan_KcalBurnUp", ServerManager.Instance.myTodayQuest[3].quest_State);

        //// 최고속도 퀘스트 상태
        PlayerPrefs.SetString("Busan_MaxSpeedToday", ServerManager.Instance.myTodayQuest[4].quest_State);

        //// 원래라면 서버에서 가져온 퀘스트 카테고리의 문자비교 후 넣어야함 > 하지만 일단 지금은 순서대로 넣어뒀음
        //// 맵 퀘스트 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormal1Finish", ServerManager.Instance.myMapQuest[0].quest_CompletedState);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("Busan_AsiaNormal2Finish", ServerManager.Instance.myMapQuest[1].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaNormal3Finish", ServerManager.Instance.myMapQuest[2].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHard1Finish", ServerManager.Instance.myMapQuest[3].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHard2Finish", ServerManager.Instance.myMapQuest[4].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHard3Finish", ServerManager.Instance.myMapQuest[5].quest_CompletedState);

        //// 맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("Busan_AsiaNormal1FinishAmount", ServerManager.Instance.myMapQuest[0].quest_Progress);
        PlayerPrefs.SetInt("Busan_AsiaNormal2FinishAmount", ServerManager.Instance.myMapQuest[1].quest_Progress);
        PlayerPrefs.SetInt("Busan_AsiaNormal3FinishAmount", ServerManager.Instance.myMapQuest[2].quest_Progress);
        PlayerPrefs.SetInt("Busan_AsiaHard1FinishAmount", ServerManager.Instance.myMapQuest[3].quest_Progress);
        PlayerPrefs.SetInt("Busan_AsiaHard2FinishAmount", ServerManager.Instance.myMapQuest[4].quest_Progress);
        PlayerPrefs.SetInt("Busan_AsiaHard3FinishAmount", ServerManager.Instance.myMapQuest[5].quest_Progress);

        //// 맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", ServerManager.Instance.myMapQuest[6].quest_CompletedState); // 0~13
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", ServerManager.Instance.myMapQuest[7].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish3", ServerManager.Instance.myMapQuest[8].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", ServerManager.Instance.myMapQuest[9].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", ServerManager.Instance.myMapQuest[10].quest_CompletedState);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish3", ServerManager.Instance.myMapQuest[11].quest_CompletedState);

        //// 원래라면 서버에서 가져온 퀘스트 카테고리의 문자비교 후 넣어야함 > 하지만 일단 지금은 순서대로 넣어뒀음
        //// 맵 퀘스트 - 커스텀 보상
        PlayerPrefs.SetString("Busan_AllOneFinish", ServerManager.Instance.questReward[0].reward_State);    //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_AllTenFinish", ServerManager.Instance.questReward[1].reward_State);
        PlayerPrefs.SetString("Busan_AllTwentyFinish", ServerManager.Instance.questReward[2].reward_State);
        PlayerPrefs.SetString("Busan_Distance500Km", ServerManager.Instance.questReward[3].reward_State);
        PlayerPrefs.SetString("Busan_Distance1000Km", ServerManager.Instance.questReward[4].reward_State);
        PlayerPrefs.SetString("Busan_Distance1500Km", ServerManager.Instance.questReward[5].reward_State);

        //Debug.Log("로그인 상태 ----- " + PlayerPrefs.GetString("Busan_Player_LoginState"));
    }
}
