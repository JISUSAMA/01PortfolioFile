using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Login_UIManager : MonoBehaviour
{
    public Toggle personalToggle;   //개인정보약관 동의 토글
    public GameObject personalPanel;    //개인정보약관 팝업
    public GameObject personalPopupOb;
    public GameObject[] personalPanelText;    //개인정보약관보기
    bool PersonInfo_bool = false;
    bool TermsConditions_bool = false;
    public GameObject changePanel;  //씬전환판넬
                                    //  public GameObject[] stateButton;    //씬전환 버튼들(시작,닉네임,캐릭터설정씬)
    public InputField id_Field;
    public InputField pw_Field;
    public GameObject pwFindPopup;  //비번찾기판넬
    public InputField pwFind_Field; //비번찾기
    public GameObject AnnouncementPopup; //메일 보내기 팝업
    public Text AnnouncementText; //메일 보내기 성공/실패 텍스트
    bool AnnouncementState = false;
    //public Text logText;

    public GameObject errer_ob;
    public Text errer_Text; //알림말 텍스트

    bool toggleIsOn;    //약관동의 여부
    string loginState;  //로그인 상태
    string player_id, player_uid, player_pw;

    string PASSWORD_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //임의의 비밀번호 생성하기 위한 문자들

    void Start()
    {
        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;
        //구글 플레이 활성화
        PlayGamesPlatform.Activate();

        Initialization();
    }
    private void Update()
    {
        //개인정보약관 판넬
        if (personalPanel.activeSelf.Equals(true))
        {
            if (PersonInfo_bool.Equals(true) && TermsConditions_bool.Equals(true))
                personalToggle.interactable = true;
            else { }
                //personalToggle.interactable = false;
        }

    }
    void Initialization()
    {
        //PlayerPrefs.SetString("Player_LoginState", "");
        loginState = PlayerPrefs.GetString("Player_LoginState");
        //logText.text = loginState;

        if (loginState == "")
        {
            personalPanel.SetActive(true);
        }
        else if(loginState == "TermsJoinAgree")
        {
            personalPanel.SetActive(false);
        }
        else if (loginState == "Again")
        {

        }
        else if (loginState == "Google" || loginState == "GoogleNickName")
        {
            GoogleLoginButtonOn();
        }
        else if (loginState == "Gateways")
        {
            GatewaysLoginButtonOn();
        }
    }

    //게이트웨이즈로그인 버튼 클릭
    public void GatewaysLoginButtonOn()
    {
        // 서버에 아이디가 있는지 확인후 있다면 > UID, ID, PW, NICKNAME, LOGINSTATE 저장
        ServerManager.Instance.Search_USER_Info(id_Field.text);

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
                PlayerPrefs.SetString("Player_ID", id_Field.text);  //아이디 저장
                PlayerPrefs.SetString("Player_Password", pw_Field.text);    //비번 저장
                PlayerPrefs.SetString("Player_UID", ServerManager.Instance.userInfo.player_UID);    //서버(player_uid)에 저장된 고유 아이디 들고 와서 저장
                PlayerPrefs.SetString("Player_LoginState", ServerManager.Instance.userInfo.player_LoginState); //서버에 저장된 로그인 상태를 들고와서 저장
                loginState = PlayerPrefs.GetString("Player_LoginState");

                if (loginState == "Gateways")
                {
                    //Debug.Log("id_Field.text : " + id_Field.text);
                    StartCoroutine(_ServerMyInformation(id_Field.text)); //서버에서 정보 들고오기

                    //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                    //StartCoroutine(SceneChangePanelAction(2f, true, true, false));
                }
                else if (loginState == "GatewaysNickName")
                {
                    //초, 상태판넬, 게임시작, 닉네임씬, 캐릭터선택씬
                    //StartCoroutine(SceneChangePanelAction(2f, true, false, true));
                    SceneManager.LoadScene("NickName");
                }
                else if (loginState == "Again")
                {
                    StartCoroutine(_ServerMyInformation(id_Field.text)); //서버에서 정보 들고오기
                }
            }
            else if (pw_Field.text != ServerManager.Instance.userInfo.player_PW)
            {
                Debug.Log("입력한 비밀번호가 맞지않습니다.");
                SoundFunction.Instance.Warning_Sound();
                errer_ob.SetActive(true);
                errer_Text.text = "입력한 비밀번호가 맞지않습니다.";
            }
        }
        else if (id_Field.text != ServerManager.Instance.userInfo.player_ID)
        {
            Debug.Log("입력한 아이디가 존재하지 않습니다.");
            SoundFunction.Instance.Warning_Sound();
            errer_ob.SetActive(true);
            errer_Text.text = "입력한 아이디가 존재하지 않습니다.";
        }
    }

    //구글로그인 버튼 클릭
    public void GoogleLoginButtonOn()
    {
        // 서버 로딩 딜레이 주기 밑에 다 코루틴안으로 이동
        StartCoroutine(_GoogleLoginButtonOn());
    }

    //구글로그인
    IEnumerator _GoogleLoginButtonOn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                //logText.text = Social.localUser.id + " \n" + Social.localUser.userName;
                // 서버에 구글 아이디가 있는지 확인
                ServerManager.Instance.Search_USER_Info(Social.localUser.id);
                Invoke("Delay_StakUp_ID", 1f);
            }
            else
            {
                //logText.text = "로그인 실패!!!!빠밤";
                SoundFunction.Instance.Warning_Sound();
            }
                
        });

        yield return null;
    }

    private void Delay_StakUp_ID()
    {
        //서버에 아이디가 없으면 - 첫 로그인
        if (Social.localUser.id != ServerManager.Instance.userInfo.player_ID)   //서버에서 아이디 검색 비교
        {
            player_uid = UniqueID();    //고유아이디 저장
            PlayerPrefs.SetString("Player_ID", Social.localUser.id);    //아이디 저장
            PlayerPrefs.SetString("Player_UID", player_uid);    //고유아이디 저장
            PlayerPrefs.SetString("Player_LoginState", "GoogleNickName");    // 닉네임씬 상태 저장
            PlayerPrefs.SetString("Player_Password", "");    // PW

            // 서버에도 저장
            ServerManager.Instance.UserInfo_Reg();

            //판넬 띄워주기
            //StartCoroutine(SceneChangePanelAction(2f, true, false, true));   //2초 후 판넬생성 후 닉네임 씬버튼 활성화
            SceneManager.LoadScene("Nickname");// 닉네임 씬버튼 활성화
        }
        else//아이디있으면 - 서버에서 비교해서 기록이 있으면
        {
            PlayerPrefs.SetString("Player_ID", Social.localUser.id);  //아이디 저장
            PlayerPrefs.SetString("Player_UID", ServerManager.Instance.userInfo.player_UID);    //서버(player_uid)에 저장된 고유 아이디 들고 와서 저장
            PlayerPrefs.SetString("Player_LoginState", ServerManager.Instance.userInfo.player_LoginState); //서버에 저장된 로그인 상태를 들고와서 저장
            loginState = PlayerPrefs.GetString("Player_LoginState");

            if (loginState == "Google")
            {
                //ServerMyInformation();  //서버에서 정보 들고오기
                StartCoroutine(_ServerMyInformation(Social.localUser.id));
                //판넬 띄워주기
                StartCoroutine(SceneChangePanelAction(2f, true, true, false));   //2초 후 판넬생성 후 로비 씬버튼 활성화
                Debug.Log("여기1");
            }
            else if (loginState == "GoogleNickName")
            {
                //판넬 띄워주기
                StartCoroutine(SceneChangePanelAction(2f, true, true, false));   //2초 후 판넬생성 후 로비 씬버튼 활성화
                Debug.Log("여기2");
            }
        }
    }

    //씬전환 버튼 비활성화/활성화 시켜주는 함수
    IEnumerator SceneChangePanelAction(float _time, bool _panel, bool _start, bool _nickname)
    {
        yield return new WaitForSeconds(_time);

        changePanel.SetActive(_panel);
        //  stateButton[0].SetActive(_start);
        //  stateButton[1].SetActive(_nickname);
    }

    //고유아이디 만드는 함수
    string UniqueID()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "02" + z1;
        return uid;
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
        if (toggleIsOn)  //동의했으면
        {
            personalPanel.SetActive(false);
            PlayerPrefs.SetString("Player_LoginState", "TermsJoinAgree");
        }
        else
        {//동의안했으면
            personalPanel.SetActive(true);
        }
    }

    //계정만들기 버튼 클릭 이벤트
    public void AccountButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        SceneManager.LoadScene("Join");
    }

    //비밀번호찾기 버튼 클릭 이벤트
    public void PassWordFind()
    {
        SoundFunction.Instance.ButtonClick_Sound();
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

    /////////////////// 서버 안에 있는 아이디 중에 입력받은 아이디가 존재하는지 확인필요 ///////////////////// 
    //비밀번호 찾기 버튼 이벤트
    IEnumerator _EmailServerDataCompartison()
    {
        yield return new WaitUntil(() => ServerManager.Instance.isUserSearchCompleted);   // 서버에서 검색을 끝내면

        ServerManager.Instance.isUserSearchCompleted = false;

        string pw = GeneratePaseeword(7);   //임의의 비밀번호
        Debug.Log("player_ID" + ServerManager.Instance.userInfo.player_ID);
        Debug.Log("pwFind_Field.text" + pwFind_Field.text);
        if (pwFind_Field.text == ServerManager.Instance.userInfo.player_ID)
        {
            ServerManager.Instance.Get_CompanyEmail();

            yield return new WaitUntil(() => ServerManager.Instance.isCompanyEmailSearchCompleted);   // 서버에서 검색을 끝내면

            ServerManager.Instance.isCompanyEmailSearchCompleted = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("gateways202001@gmail.com"); // 보내는사람
            mail.To.Add(pwFind_Field.text); // 받는 사람
            mail.Subject = "[gateways]비밀번호 재설정";
            mail.Body = "임의의 비밀번호를 보내드립니다." + "\n" + "대괄호('[]')안에 있는 비밀번호로 게임에 접속해주세요." + "\n" +
                "게임에 접속하신 후 계정설정에서 비밀번호를 꼭 변경해주세요." + "\n" + "\n" + "\n" +
                "[ " + pw + " ]"; //임의의 비밀번호 넣기

            ServerManager.Instance.PasswordChange(pw);

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(ServerManager.Instance.company_Info.email, ServerManager.Instance.company_Info.email_PW) as ICredentialsByHost; // 보내는사람 주소 및 비밀번호 확인
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

            { return true; };

            smtpServer.Send(mail);

            Debug.Log("success");
            AnnouncementPopup.SetActive(true);
            AnnouncementState = true;
            AnnouncementText.text = "해당 메일로 비밀번호가 발송되었습니다.";
            pwFind_Field.text = "";
        }
        else
        {
            SoundFunction.Instance.Warning_Sound();
            AnnouncementPopup.SetActive(true);
            AnnouncementState = false;
            AnnouncementText.text = "일치하는 메일이 없습니다.\n다시한번 확인 해주세요";
            pwFind_Field.text = "";
        }

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

    public void OnClick_AnnouncementPopup()
    {
        //비밀번호 변경 성공, 로그인 화면으로 
        if (AnnouncementState.Equals(true))
            pwFindPopup.SetActive(false);
        //비밀번호 변경 실패 , 알림 팝업 닫기
        else { AnnouncementPopup.SetActive(false); SoundFunction.Instance.Warning_Sound(); }
           
    }
    /////////////////////  이용약관 0, 개인정보약관 1 ///////////////////////// 
    //이용약관 선택
    public void OnClcik_TermsConditions()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        personalPopupOb.SetActive(true);
        personalPanelText[0].SetActive(true);
    }
    //개인정보약관 선택 
    public void OnClick_PersonInfoBtn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        personalPopupOb.SetActive(true);
        personalPanelText[1].SetActive(true);
    }
    public void OnClick_PersonInfoBtnOkBtn()
    {
        if (personalPanelText[0].activeSelf.Equals(true))
        {
            personalPanelText[0].SetActive(false);
            PersonInfo_bool = true;
        }
        else if (personalPanelText[1].activeSelf.Equals(true))
        {
            personalPanelText[1].SetActive(false);
            TermsConditions_bool = true;
        }
        personalPopupOb.SetActive(false);
    }
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    ////////////////////////////////////////////////////////////////////////
    IEnumerator _ServerMyInformation(string _userID)
    {
        Debug.Log("서버인포");
        // 가져오는 데이터 //

        // 접속일 / 코인

        // 닉네임 / 로그인 상태
        // 현재구간
        // 월별 걸은거리 / 걸음수 / 걸음시간 / 소모칼로리
        // 총 걸은거리 / 걸음수 / 걸은시간 / 소모칼로리
        // 달까지 남은 거리
        // 소비 아이템 : 작은공기통, 큰공기통, 에너지드링크

        // 서버에서 계정 데이터 가져오기
        ServerManager.Instance.Get_MyProfileData(_userID);

        yield return new WaitUntil(() => ServerManager.Instance.isGetMyProfileDataCompleted);

        ServerManager.Instance.isGetMyProfileDataCompleted = false;

        PlayerPrefs.SetString("Player_NickName", ServerManager.Instance.userInfo.player_NickName);
        PlayerPrefs.SetString("Player_LoginState", ServerManager.Instance.userInfo.player_LoginState);

        PlayerPrefs.SetInt("Player_Coin", ServerManager.Instance.lobbyInfo.player_Coin);
        PlayerPrefs.SetInt("Player_ConnectDay", ServerManager.Instance.lobbyInfo.player_ConnectDate);
        PlayerPrefs.SetInt("Current_Section", ServerManager.Instance.lobbyInfo.player_Current_Section);

        PlayerPrefs.SetFloat("Total_Distance", ServerManager.Instance.totalData_To_Date.total_Distance);
        PlayerPrefs.SetString("Total_StepTime", ServerManager.Instance.totalData_To_Date.total_StepTime);
        PlayerPrefs.SetFloat("Total_Kcal", ServerManager.Instance.totalData_To_Date.total_Kcal);
        PlayerPrefs.SetInt("Total_StepCount", ServerManager.Instance.totalData_To_Date.total_StepCount);

        PlayerPrefs.SetInt("Item_SmallAirTank", ServerManager.Instance.consumable_Item.Item_SmallAirTank);
        PlayerPrefs.SetInt("Item_BigAirTank", ServerManager.Instance.consumable_Item.Item_BigAirTank);
        PlayerPrefs.SetInt("Item_EnergyDrink", ServerManager.Instance.consumable_Item.Item_EnergyDrink);

        PlayerPrefs.SetInt("MoonPieceCount", ServerManager.Instance.moonPiece.count);   // 달의 조각 갯수

        PlayerPrefs.SetFloat("Moon_Distance", ServerManager.Instance.totalData_To_Date.moon_dist);   //달까지 남은 거리

        //--------------------------------------------------------------------------------------------------

        // 서버에서 레코드(그래프) 데이터 가져오기
        // 오늘 날짜 확인 후 없으면 기본값 프리팹에 저장 있으면 가져오기 ( 일, 월, 년 )
        string[] todayArr = new string[3];
        todayArr[0] = DateTime.Now.ToString("yyyy");
        todayArr[1] = DateTime.Now.ToString("MM");
        todayArr[2] = DateTime.Now.ToString("dd");

        // 오늘 날짜 있든 없든 가져옴
        // 없으면 없는거 확인
        //ServerManager.Instance.Get_DateData(int.Parse(todayArr[2]), int.Parse(todayArr[2]), int.Parse(todayArr[1]), int.Parse(todayArr[0]));
        ServerManager.Instance.Get_DateData(int.Parse(todayArr[0]), int.Parse(todayArr[1]), int.Parse(todayArr[2]), int.Parse(todayArr[2]));
        yield return new WaitUntil(() => ServerManager.Instance.isGetDateDataCompleted);
        ServerManager.Instance.isGetDateDataCompleted = false;

        ServerManager.Instance.Get_MonthData(int.Parse(todayArr[0]), int.Parse(todayArr[1]), int.Parse(todayArr[1]));
        yield return new WaitUntil(() => ServerManager.Instance.isGetMonthDataCompleted);
        ServerManager.Instance.isGetMonthDataCompleted = false;

        PlayerPrefs.SetFloat("Today_Distance", ServerManager.Instance.dataByDate[0].today_Distance);  //일별 걸은거리
        PlayerPrefs.SetInt("Today_StepCount", ServerManager.Instance.dataByDate[0].today_StepCount);   //일별 걸음수
        PlayerPrefs.SetString("Today_StepTime", ServerManager.Instance.dataByDate[0].today_StepTime);    //일별 걸은시간
        PlayerPrefs.SetFloat("Today_Kcal", ServerManager.Instance.dataByDate[0].today_Kcal);  //일별 칼로리

        PlayerPrefs.SetFloat("Month_Distance", ServerManager.Instance.dataByMonth[0].month_Distance);  //월별 걸은거리
        PlayerPrefs.SetInt("Month_StepCount", ServerManager.Instance.dataByMonth[0].month_StepCount);   //월별 걸음수
        PlayerPrefs.SetString("Month_StepTime", ServerManager.Instance.dataByMonth[0].month_StepTime);    //월별 걸은시간
        PlayerPrefs.SetFloat("Month_Kcal", ServerManager.Instance.dataByMonth[0].month_Kcal);  //월별 칼로리

        PlayerPrefs.SetFloat("BGM",20);  //BGM 소리
        PlayerPrefs.SetFloat("SFX", 20);  //SFX 소리
        SceneManager.LoadScene("Lobby");
    }
}